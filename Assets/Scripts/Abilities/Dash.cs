using UnityEngine;

namespace DangerousPenguin.Abilities
{

[CreateAssetMenu(menuName = "Ability/Dash")]
public class Dash : AbilityBase
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private AnimationCurve MoveSpeedCurve;

    private float _timeElapsed;

    public override AttackType Ability => AttackType.Dash;
    protected override void Perform(PlayerCombatController playerCombatController)
    {
        Debug.Log(Ability);
        _timeElapsed = 0;
        
    }

    public override void OnUpdate(PlayerCombatController player)
    {
        _timeElapsed += Time.deltaTime;
        var speed = MoveSpeedCurve.Evaluate(_timeElapsed)*MoveSpeed;
        //Debug.Log(speed);
        player.MoveForward(speed);
    }
}

}