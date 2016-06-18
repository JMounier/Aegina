using UnityEngine;
using System.Collections;

public class SmashAttack : StateMachineBehaviour
{      
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime - Mathf.Floor(stateInfo.normalizedTime) > 0.85 )
            animator.transform.GetComponentInChildren<ParticleSystem>().Play();
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<SyncBoss>().ShockWave();
        animator.gameObject.GetComponent<SyncBoss>().Damage = 0;
    }
}