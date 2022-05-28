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

    private float _lastTimeUsed = float.MinValue;

    private void OnEnable()
    {
        _lastTimeUsed = float.MinValue;
    }

    public void Execute(PlayerCombatController player)
    {
        if (CooldownRemaining <= 0)
        {
            Debug.Log($"Doing the {GetType().Name} attack");
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
        //do nothing
    }
}

}