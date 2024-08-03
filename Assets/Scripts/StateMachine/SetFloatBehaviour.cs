using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    /*
    This script is designed to set parameter or conditonal float values when entering or exiting
    animations states.
    When using this state machine within the Unity Animation Panel, the behaviour can be set on entering
    or exiting the state where required, and can be tied to specific animation sets.
    The primary use is to create movement constraints for Game Objects such setting movement speed
    during an attack knockback. 
    These constraints are primarily for game balance or  character immersion.
    */

    public string floatName;
    public bool updateOnStateEnter, updateOnStateExit;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    public float valueOnEnter, valueOnExit;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
