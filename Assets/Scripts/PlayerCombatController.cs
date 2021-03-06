using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Abilities;
using DangerousPenguin.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DangerousPenguin
{

public class PlayerCombatController : MonoBehaviour
{
    public static PlayerCombatController Instance { get; private set; }

    private static readonly int FastAttackTrigger = Animator.StringToHash("FastAttack");
    private static readonly int AttackTrigger     = Animator.StringToHash("Attack");
    private static readonly int MagicAttack       = Animator.StringToHash("MagicAttack");
    private static readonly int DashAttack        = Animator.StringToHash("Dash");

    private GameControls _input;
    private AbilityBase  _currentAbility;
    private bool         _attacking = false;


    [SerializeField] private List<AbilityBase> abilities;
    [SerializeField] private NavMeshAgent      navMeshAgent;
    [SerializeField] private Animator          animator;
    [SerializeField] private PlayerController  player;

    [Header("Combat")]
    [SerializeField] private float baseDamage = 10.0f;

    [SerializeField] private float     strongDamage = 25.0f;
    [SerializeField] private float     meleeRadius  = 2.0f;
    [SerializeField] private LayerMask enemyLayerMask;

    [Header("Demonic parts")]
    [SerializeField] private GameObject horns;
    [SerializeField] private EyeSwapper eyeSwapper;


    private void Awake()
    {
        Instance = this;

        _input = new GameControls();
        _input.Game.Enable();
    }

    private void OnEnable()
    {
        _input.Game.PrimaryAttack.started   += PrimaryAttackInputStarted;
        _input.Game.PrimaryAttack.performed += PrimaryAttackInputPerformed;

        _input.Game.UseSkill1.performed += UseSkillInputPerformed;
        _input.Game.UseSkill2.performed += UseSkillInputPerformed;
        _input.Game.UseSkill3.performed += UseSkillInputPerformed;
    }

    private void OnDisable()
    {
        _input.Game.PrimaryAttack.started   -= PrimaryAttackInputStarted;
        _input.Game.PrimaryAttack.performed -= PrimaryAttackInputPerformed;

        _input.Game.UseSkill1.performed -= UseSkillInputPerformed;
        _input.Game.UseSkill2.performed -= UseSkillInputPerformed;
        _input.Game.UseSkill3.performed -= UseSkillInputPerformed;
    }

    private void UseSkillInputPerformed(InputAction.CallbackContext obj)
    {
        if (_attacking) return;
        int abilityIndex = -1;
        if (obj.action.name == _input.Game.UseSkill1.name)
        {
            //roll or fire-roll
            abilityIndex = abilities.FindIndex(a=> a.Ability == (fireDashEnabled ? AttackType.FireDash : AttackType.Dash));
        }
        else if (obj.action.name == _input.Game.UseSkill2.name)
        {
            //fireball
            if(fireBallEnabled)
                abilityIndex = abilities.FindIndex(a=>a.Ability == AttackType.FireBall);
        }

        if (abilityIndex >= 0)
        {
            abilities[abilityIndex].Execute(this);
        }
    }

    private void Update()
    {
        if (_attacking)
        {
            _currentAbility.OnUpdate(this);
        }
    }

    private void PrimaryAttackInputStarted(InputAction.CallbackContext obj)
    {
        if (obj.interaction is HoldInteraction) DoPrimaryAttack();
    }

    private void PrimaryAttackInputPerformed(InputAction.CallbackContext obj)
    {
        if (obj.interaction is TapInteraction) DoFastAttack();
    }


    private void DoPrimaryAttack()
    {
        if (_attacking) return;
        abilities[1].Execute(this);
    }

    private void DoFastAttack()
    {
        if (_attacking && (!_currentAbility || _currentAbility.Ability != AttackType.StrongAttack)) return;
        abilities[0].Execute(this);
    }

    public bool OnAttackStart(AttackType attack, AbilityBase ability)
    {
        if (_attacking)
        {
            // allow transition from strong to fast attack
            if (_currentAbility.Ability != AttackType.StrongAttack || ability.Ability != AttackType.FastAttack)
            {
                Debug.LogWarning("Player is already attacking!");
                return false;
            }
        }

        //navMeshAgent.enabled = false; // allow moving without collisions?
        _attacking      = true;
        _currentAbility = ability;
        player.BlockMovement();
        player.RotateTowardsCursor();

        switch (attack)
        {
            case AttackType.FastAttack:
                animator.SetTrigger(FastAttackTrigger);
                break;
            case AttackType.StrongAttack:
                animator.SetTrigger(AttackTrigger);
                break;
            case AttackType.Dash:
                animator.SetTrigger(DashAttack);
                break;
            case AttackType.FireDash:
                animator.SetTrigger(DashAttack);
                break;
            case AttackType.FireBall:
                animator.SetTrigger(MagicAttack);
                break;
            default:
                Debug.LogWarning($"Unknown attack type {attack}");
                break;
        }

        return true;
    }

    public void OnAttackEnd()
    {
        _attacking           = false;
        navMeshAgent.enabled = true;
        player.ResumeMovement();
    }

    public void MoveForward(float moveSpeed)
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void OnAttackAnimationEnd(bool powerAttack)
    {
        if (_currentAbility.Ability == AttackType.FastAttack && powerAttack) return;
        if (_currentAbility.Ability == AttackType.StrongAttack && !powerAttack) return;
        
        OnAttackEnd();
    }

    public void OnAttackHit(AbilityBase ability)
    {
        var damage = 0.0f;
        switch (ability.Ability)
        {
            case AttackType.FastAttack:
                damage = baseDamage;
                break;
            case AttackType.StrongAttack:
                damage = strongDamage;
                break;
            default:
                //other skills are handled by special prefabs
                return;
        }

        var hits = Physics.OverlapSphere(transform.position, meleeRadius, enemyLayerMask);
        foreach (var hit in hits)
        {
            if (Vector3.Dot(hit.transform.position - transform.position, transform.forward) < 0) continue;
            var health = hit.GetComponent<Health>();
            if (health == null) continue;
            health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
        Gizmos.DrawRay(transform.position, transform.forward);
    }

    private bool fireBallEnabled=false;
    private bool fireDashEnabled=false;
    public void ActivateFireBall()
    {
        fireBallEnabled = true;
        horns.SetActive(true);
    }

    public void ActivateFireDash()
    {
        fireDashEnabled = true;
        eyeSwapper.GlowThemEyes();
    }
}

public enum AttackType
{
    FastAttack,
    StrongAttack,
    Dash,
    FireDash,
    FireBall,
}

}