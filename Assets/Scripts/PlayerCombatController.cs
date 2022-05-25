using System;
using System.Collections;
using System.Collections.Generic;
using DangerousPenguin.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DangerousPenguin
{

public class PlayerCombatController : MonoBehaviour
{
    private static readonly int FastAttackTrigger = Animator.StringToHash("FastAttack");
    private static readonly int AttackTrigger     = Animator.StringToHash("Attack");

    private GameControls _input;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _input = new GameControls();
        _input.Game.Enable();
    }

    private void OnEnable()
    {
        _input.Game.PrimaryAttack.started   += PrimaryAttackInputStarted;
        _input.Game.PrimaryAttack.performed += PrimaryAttackInputPerformed;
        _input.Game.PrimaryAttack.canceled  += PrimaryAttackInputCanceled;
    }

    private void OnDisable()
    {
        _input.Game.PrimaryAttack.started   -= PrimaryAttackInputStarted;
        _input.Game.PrimaryAttack.performed -= PrimaryAttackInputPerformed;
        _input.Game.PrimaryAttack.canceled  -= PrimaryAttackInputCanceled;
    }

    private void Update()
    {
        if (_attacking)
        {
            var attackTime = Time.time - _attackTimeStart;
            if (_isStrong)
            {
                if (attackTime > strongAttackTime)
                {
                    //Debug.Log("Strong attack!");
                    _attacking = false;
                }
            }
            else
            {
                if (attackTime > fastAttackTime)
                {
                    Debug.Log("Fast attack!");
                    _attacking = false;
                }
            }
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

    private void PrimaryAttackInputCanceled(InputAction.CallbackContext obj)
    {
        // do nothing?
    }

    private bool  _attacking = false;
    private bool  _isStrong  = false;
    private float _attackTimeStart;

    
    [SerializeField] private float strongAttackTime = 1.0f;
    [SerializeField] private float fastAttackTime = 0.5f;

    private void DoPrimaryAttack()
    {
        if (_attacking) return;

        _animator.SetTrigger(AttackTrigger);
        _attacking       = true;
        _isStrong        = true;
        _attackTimeStart = Time.time;
    }

    private void DoFastAttack()
    {
        _animator.SetTrigger(FastAttackTrigger);
        _isStrong = false;
    }
}

}