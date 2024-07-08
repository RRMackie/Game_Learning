using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //Get the relevant values and game object to be removed after entering the death state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //The gameobject has its alpha decreased over time to fade and then destroyed to remove the game object entirely
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        
        float newAlpha = startColor.a *(1 - timeElapsed/fadeTime);

        spriteRenderer.color = new Color(startColor.r, startColor.g,startColor.b, newAlpha);

        if(timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }

 
}
