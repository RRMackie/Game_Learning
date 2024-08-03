using UnityEngine;

//
public class FadeRemove : StateMachineBehaviour
{
    /*
    The Fade Remove script handles fading out a game object when triggered by an animation state.
    
    Most commonly this is used by enemy NPC death states, the game object sprite is faded out by decreasing
    the alpha value over time until reaching 0. After this fade the game object is destroyed and removed from the application.

    The fade time value can be set in the Unity Inspector to allow for varied instances to suit the application requiremnts.
    A delay timer can be set to slightly delay the behaviour depending on requirements
    */

    // Set the default time for the Game Object to fade away
    public float fadeTime = 0.5f;

    // Set a delay to the fade timer
    public float delayFade = 0.0f;

    // The current amount of time passed.
    private float timeElapsed = 0f;

    // The current delay time passed.
    private float delayElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state.
    // Get the relevant values and game object to be removed after entering the death state/
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks/
    //The gameobject has its alpha decreased over time to fade and then destroyed to remove the game object entirely.
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (delayFade > delayElapsed)
        {
            delayElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = startColor.a * (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if (timeElapsed > fadeTime)
            {
                Destroy(objToRemove);
            }
        }
    }
}
