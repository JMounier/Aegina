using UnityEngine;
using System.Collections;

public class AnimalAttack : StateMachineBehaviour
{
    float cd;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cd += Time.deltaTime;
        if (cd > .666f)
        {
            string mobName = animator.gameObject.name.Replace("(Clone)", "");
            if (mobName.Contains("Slime"))
                mobName = "Slime";
            animator.gameObject.GetComponentInParent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Mob/" + mobName + "/Agressif"), 4);
            cd = 0;
        }
    }
}

