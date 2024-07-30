using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgePickup : MonoBehaviour
{
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
    [SerializeField] private float floatingHeight = 0.5f; // The height of the floating movement
    [SerializeField] private float floatingSpeed = 1.0f; // The speed of the bobbing movement

    private bool playerInRange;
    private Vector3 startPosition;
    private float floatingOffset;

    // Called when component exists in scene
    // Audio Source for any Audio components attached to the Game Object
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        playerInRange = false;
        visualCue.SetActive(false);
        startPosition = transform.position; // Store the initial position of the object
    }

    private void Update()
    {
        FloatEffect();

        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) // Check for key press
            {
              StartDialogue();
           }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

      private void StartDialogue()
    {
        // Enter dialogue mode with default parameters for choices and trigger
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, -1, null);
        Debug.Log(inkJSON.text);
    }

     private void FloatEffect()
    {
        // Calculate floating offset
        floatingOffset = Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        // Apply bobbing offset to the object's position
        transform.position = startPosition + new Vector3(0, floatingOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;


        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
         if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            
        }
    }
}
