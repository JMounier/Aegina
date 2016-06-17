using UnityEngine;
using System.Collections;

public class AnimalIdle : StateMachineBehaviour {

    private float cd;
    [SerializeField]
    private float probability;
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!(cd > 0))
        {
            if (Random.Range(0f, 1f) < probability)
            {
                string mobName = animator.gameObject.name.Replace("(Clone)", "");
                animator.gameObject.GetComponentInParent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Mob/" + mobName + "/Idle"), 1);
            }
            cd = Random.Range(1f, 2f);
        }
        else
            cd -= Time.deltaTime;
    }

}
