using UnityEngine;

public class PlaySingleUseBehaviour : StateMachineBehaviour
{
    /*
    This script allows for single use components to be executed as set times.
    In this application it is used for audio, mostly sfx tied to specific actions within the application.

    The script creates the audio object when required and deletes it as soon as it is completed.

    The Audio, volume and a delay timer can be set within the Unity inspector to tailor to the application
    requirements.
    */

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

    //Called when the animation state is entered
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
        timeElapsed = 0f;
        delayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !delayedSoundPlayed)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                delayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }
}
