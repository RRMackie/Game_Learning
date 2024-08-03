using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    /*
    This class sets up game objects that use the dialogue system, creating parameters to hold specific JSON txts files,
    before initilaing the dialogue system through the dialogue manager.

    The visual cue is a small symbol that appears above the interactable NPC, notifying the player that they can interact or
    that this game object has dialogue atttached.

    The visual cue and Text can be set using the Unity Inspector allowing for specific instances of interactable NPCs

    If creating a NPC with choices involved, these can also be set using the unity inspector.
    If the NPC is tied to level progression, the correct choice can also be set in the inspector,
    as well as the moving platform object to be activated.
    */

    // Set up the game object acting as the visual cue
    [Header("Visual Cue")]
    [SerializeField] public GameObject visualCue;

    // Set up the ink JSON file Text Asset to be used for the dialogue
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Correct Answer Index")]
    [SerializeField] private int correctAnswer; // Single correct choice index

    [Header("Platform Reference")]
    [SerializeField] private MovingPlatform movingPlatform;

    public bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public void Update()
    {
        //Check if the player is in range of the NPC collider and if there is not dialogue playing already
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // If within range the visual cue is shown above the game object.
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) // Check for key press
            {
                //Start the dialogue manager instance using the text asset
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, correctAnswer, this);
                Debug.Log(inkJSON.text);
            }
        }
        else
        {
            // If not nearby the visual cue does not show
            visualCue.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    // Call the Activate Platfrom method in the moving platform script
    public void ActivatePlatform()
    {
        if (movingPlatform != null)
        {
            movingPlatform.ActivatePlatform();
        }
    }

}
