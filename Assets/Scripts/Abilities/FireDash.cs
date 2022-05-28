using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/FireDash")]
public class FireDash : AbilityBase
{
    public override AttackType Ability => AttackType.FireDash;
    protected override void Perform(PlayerCombatController playerCombatController)
    {
        Debug.Log(Ability);
    }
}

}