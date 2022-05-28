using DangerousPenguin.Abilities.AbilityPowers;
using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/Fireball")]
public class Fireball : AbilityBase
{
    [SerializeField] private GameObject   projectile;
    public override          AttackType Ability => AttackType.FireBall;
    protected override void Perform(PlayerCombatController player)
    {
        Instantiate(projectile, player.transform.position, player.transform.rotation);
    }
}

}