using UnityEngine;

namespace DangerousPenguin
{

public class AnimationEndNotifier : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log($"State exit to Movement: {animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash == Animator.StringToHash("Movement")}");
        //if(animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash == Animator.StringToHash("Movement"))return;
        bool powerAttack = stateInfo.IsName("PowerAttack");
        PlayerCombatController.Instance.OnAttackAnimationEnd(powerAttack);
    }

}

}