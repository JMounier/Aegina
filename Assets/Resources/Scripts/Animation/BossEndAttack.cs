using UnityEngine;
using System.Collections;

public class BossEndAttack : StateMachineBehaviour
{

    bool done;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.done = false;
    }
       
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!this.done)
        {
            animator.gameObject.GetComponent<SyncBoss>().Cible = new Vector3(0, animator.gameObject.transform.position.y, 0);
            this.done = true;
        }
    } 
}
