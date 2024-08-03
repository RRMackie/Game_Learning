using UnityEngine;

public class KnowledgePickup : MonoBehaviour
{
    /*
    The knowledge pickup facilitiates the basic hidden collectible and puzzle element in the application.
    
    Functionally it works like the health pickup but triggers a dialogue box when the player interacts with it.

    Similarly to an NPC dialogue trigger, the visual cue and Text JSON file can be set in the Unity Inspector, so there
    is potential expansion for this pickup to cover other puzzle elements.
    */

    // Get the Audio Source Component
    AudioSource pickupSource;

    // Set up the game object acting as the visual cue
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // Set up the ink JSON file Text Asset to be used for the dialogue
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Bobbing parameters
    [Header("Floating Parameters")]
    // The height of the floating movement
    [SerializeField] private float floatingHeight = 0.5f;
    // The speed of the bobbing movement
    [SerializeField] private float floatingSpeed = 1.0f;

    private bool playerInRange;
    private Vector3 startPosition;
    private float floatingOffset;

    /*
     Called when component exists in scene
     AudioSource for attached audio values
     Player in Range set to false by default
     VisualCue is set to in inactive by default
     StartPosition sets the intial transform values for the floating motion 
     */
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        playerInRange = false;
        visualCue.SetActive(false);
        startPosition = transform.position; // Store the initial position of the object
    }

    //Called on every frame
    private void Update()
    {
        // Enable the float effect
        FloatEffect();

        //Check if the player is within the collider range and that dialogue is not already playing
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // If within range the visual cue is shown above the game object.
            visualCue.SetActive(true);
            // Check for key press
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else
        {
            //Set the Visual cue to inactive when the player moves out of range
            visualCue.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        // Enter dialogue mode with default parameters for choices and trigger
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, -1, null);
        Debug.Log(inkJSON.text);
    }

    //Create a floating effect for the game object by moving between set transform values
    private void FloatEffect()
    {
        // Calculate floating offset
        floatingOffset = Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        // Apply bobbing offset to the object's position
        transform.position = startPosition + new Vector3(0, floatingOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;

        }
    }
}
