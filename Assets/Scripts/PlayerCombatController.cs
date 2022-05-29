using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Abilities;
using DangerousPenguin.Input;
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

    [SerializeField] private List<AbilityBase> abilities;
    [SerializeField] private NavMeshAgent      navMeshAgent;
    [SerializeField] private Animator          animator;
    [SerializeField] private PlayerController  player;

    [Header("Demonic parts")]
    [SerializeField] private GameObject horns;
    [SerializeField] private GameObject fieryEyes;
    

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
        int abilityIndex = -1;
        if (obj.action.name == _input.Game.UseSkill1.name)
        {
            abilityIndex = 2;
        }
        else if (obj.action.name == _input.Game.UseSkill2.name)
        {
            abilityIndex = 3;
        }
        else if (obj.action.name == _input.Game.UseSkill3.name)
        {
            abilityIndex = 4;
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
            // var attackTime = Time.time - _attackTimeStart;
            // if (_isStrong)
            // {
            //     if (attackTime > strongAttackTime)
            //     {
            //         //Debug.Log("Strong attack!");
            //         _attacking = false;
            //     }
            // }
            // else
            // {
            //     if (attackTime > fastAttackTime)
            //     {
            //         Debug.Log("Fast attack!");
            //         _attacking = false;
            //     }
            // }
            //
            //
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

    private bool  _attacking = false;
    private bool  _isStrong  = false;
    private float _attackTimeStart;

    [SerializeField] private float strongAttackTime = 1.0f;
    [SerializeField] private float fastAttackTime   = 0.5f;

    private void DoPrimaryAttack()
    {
        abilities[1].Execute(this);
        
        // if (_attacking) return;
        // Debug.Log("Starting attack!");
        //
        // animator.SetTrigger(AttackTrigger);
        // _attacking       = true;
        // _isStrong        = true;
        // _attackTimeStart = Time.time;
    }

    private void DoFastAttack()
    {
        abilities[0].Execute(this);
        // animator.SetTrigger(FastAttackTrigger);
        // _isStrong = false;
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

        //navMeshAgent.enabled = false; // allow moving without collisions
        _attacking           = true;
        _attackTimeStart     = Time.time;
        _currentAbility      = ability;
        player.BlockMovement();
        player.RotateTowardsCursor();
        
        switch (attack)
        {
            case AttackType.FastAttack:
                _isStrong = false;
                animator.SetTrigger(FastAttackTrigger);
                break;
            case AttackType.StrongAttack:
                _isStrong = true;
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

    public void OnAttackAnimationEnd()
    {
        OnAttackEnd();
    }

    public void OnAttackHit(AbilityBase ability)
    {
        
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