using UnityEngine;
using System.Collections;

public class AnimalStep : StateMachineBehaviour
{
    float cd;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cd += Time.deltaTime;
        if (cd > .5f)
        {
            AudioSource soundAudio = animator.gameObject.GetComponent<AudioSource>();
            soundAudio.PlayOneShot(Resources.Load("Sounds/Player/Walk1") as AudioClip, 2);
            cd = 0;
        }     
    }
}
