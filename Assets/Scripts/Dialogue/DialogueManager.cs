using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    /*
    The Dialogue manager handles all dialogue related information on the UI.
    Dialogue itself is created using the Ink editor and is intergated with a Unity Package 
    between the two softwares.

    The ink editor allows for writing dialogue/narrative scripts, while adding in scripted events
    such as choices or conditions and writes it to a JSON files. The plugin keeps the integrity of this dialogue script
    and will display the correct text in the dialogue panel.

    The dialogue manager deals with choices made by players through the button addons in the Unity UI,
    and will cycle through scripted dialogue in the JSON file.

    The choice options and the dialogue cooldown time, are set in the Unity Inspector.

    */
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Set up the choices UI Element for the UI
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    //Specific type tied to the INK plugin
    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }
    public bool canStartDialogue { get; private set; } = true;

    private static DialogueManager instance;

    [Header("Dialogue Cooldown")]
    [SerializeField] private float cooldownTime = 2.0f;  // Cooldown time in seconds

    private int correctAnswer;

    private DialogueTrigger currentDialogueTrigger;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get all of the choices text and set up the array to hold them
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index ++;
        }
    }

    private void Update()
    {
        // Return 
        if (!dialogueIsPlaying)
        {
            return;
        }

        // Continue to next line when submit is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, int correctAnswer, DialogueTrigger dialogueTrigger)
    {

          if (!canStartDialogue)
        {
            Debug.Log("Dialogue is on cooldown.");
            return;
        }
        
        Debug.Log("Entering dialogue mode.");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        this.correctAnswer = correctAnswer;
        this.currentDialogueTrigger = dialogueTrigger;

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        Debug.Log("Exiting dialogue mode.");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        StartCoroutine(DialogueCooldown());
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //Set the dialogue text for the current line
            dialogueText.text = currentStory.Continue();
            //Display any choices for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Debug check if number of choices exceeds the number that the UI can support
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
            + currentChoices.Count);
        }

        int index = 0;
        // Enable choices to be viewable and active on the UI dialogue menu
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index ++;
        }
        // Check any other remaining choices the UI supports and hide them
        for (int i = index; i <choices.Length; i ++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event system requires options to be cleared first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        
        //Check if the correct choice is made
        if (choiceIndex == correctAnswer)
        {
            currentDialogueTrigger.ActivatePlatform();
        }
    }

     private IEnumerator DialogueCooldown()
    {
        canStartDialogue = false;
        yield return new WaitForSeconds(cooldownTime);
        canStartDialogue = true;
    }
}
