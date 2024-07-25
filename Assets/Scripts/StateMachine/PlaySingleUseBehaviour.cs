using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySingleUseBehaviour : StateMachineBehaviour
{
    // The Audio Clip to play
    public AudioClip soundToPlay;

    // The volume the audio clip will play at, default is max
    public float volume = 1.5f;

    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    // Delayed sound timer.
    public float playDelay = 0.25f;

    // Time passed since entering the state.
    private float timeElapsed = 0;

    //Check if the delayed sound has played.
    private bool delayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

        timeElapsed = 0f;
        delayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !delayedSoundPlayed)
        {
            timeElapsed += Time.deltaTime;

            if(timeElapsed > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                delayedSoundPlayed = true;
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }
}
