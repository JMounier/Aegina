using UnityEngine;
using System.Collections;

public class SummonAttack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<SyncBoss>().Summon();
    }
}
