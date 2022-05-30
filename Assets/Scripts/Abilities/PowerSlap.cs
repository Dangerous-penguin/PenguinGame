using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/PowerSlap")]
public class PowerSlap : AbilityBase
{
    public override    AttackType Ability       => AttackType.StrongAttack;
    protected override bool       CooldownOnHit => true;
    
    protected override void Perform(PlayerCombatController playerCombatController)
    {
        Debug.Log(Ability);
    }
    
}

}