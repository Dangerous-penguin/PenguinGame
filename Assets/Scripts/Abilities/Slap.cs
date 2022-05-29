using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/Slap")]
public class Slap : AbilityBase
{
    public override AttackType Ability => AttackType.FastAttack;

    protected override void Perform(PlayerCombatController playerCombatController)
    {
        Debug.Log(Ability);
    }


}

}