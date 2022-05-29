using DangerousPenguin.Abilities.AbilityPowers;
using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/FireDash")]
public class FireDash : Dash
{
    [SerializeField] private GameObject groundEffect;


    
    public override AttackType Ability => AttackType.FireDash;
    protected override void Perform(PlayerCombatController player)
    {
        base.Perform(player);
        var effect = Instantiate(groundEffect, player.transform.position, player.transform.rotation).GetComponent<Blaze>();
        effect.player = player.transform;
    }
    

}

}