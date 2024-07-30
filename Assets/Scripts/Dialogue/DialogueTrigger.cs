using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Set up the game object acting as the visual cue
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // Set up the ink JSON file Text Asset to be used for the dialogue
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Correct Answer Index")]
    [SerializeField] private int correctAnswer; // Single correct choice index

    [Header("Platform Reference")]
    [SerializeField] private MovingPlatform movingPlatform;

  
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) // Check for key press
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, correctAnswer, this);
                Debug.Log(inkJSON.text);
           }
        }
        else
        {
            visualCue.SetActive(false);
        }
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

        public void ActivatePlatform()
    {
        if (movingPlatform != null)
        {
            movingPlatform.ActivatePlatform();
        }
    }

}
