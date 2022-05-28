using System;
using UnityEngine;

namespace DangerousPenguin.Abilities
{

public abstract class AbilityBase : ScriptableObject
{
    public abstract AttackType Ability      { get; }

    public float Cooldown          => cooldown;
    public float CooldownRemaining => cooldown - (Time.time - _lastTimeUsed);
    
    [SerializeField] private float cooldown;
    [SerializeField] private float timeToHit;

    private float _lastTimeUsed = float.MinValue;
    private bool  _hit           = false;

    private void OnEnable()
    {
        _lastTimeUsed = float.MinValue;
    }

    public void Execute(PlayerCombatController player)
    {
        if (CooldownRemaining <= 0)
        {
            Debug.Log($"Doing the {GetType().Name} attack");
            _hit           = false;
            _lastTimeUsed = Time.time;
            if(player.OnAttackStart(Ability, this))
                Perform(player);
        }
        else
        {
            Debug.Log($"{GetType().Name} attack on cooldown! Remaining: {CooldownRemaining:F2}");
        }
    }

    protected abstract void Perform(PlayerCombatController player);

    public virtual void OnUpdate(PlayerCombatController player)
    {
        if (!_hit && (timeToHit - (Time.time - _lastTimeUsed) <= 0))
        {
            player.OnAttackHit(this);
            OnHit(player);
            _hit = true;
        }
        //do nothing
    }

    protected virtual void OnHit(PlayerCombatController player){}
}

}