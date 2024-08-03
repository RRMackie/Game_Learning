using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    /*
    The UIManager maintains and manages all the parts within the UI for the application, 
    such as the gamecanvase and the Health related text instances.

    The main purpose of the script is to create the instances of the health text depending on
    character related events.

    The UI also has its own input system to allow the player to exit the application when in a 
    developmental build or the unity editor itself:
    Escape - Exit

    This could be expanded upon to allow for menu navigation and start screens.
    */

    // Instance of damage text
    public GameObject damageTextPrefab;
    // Instance of Health text
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;

    /*
    Called when component exists in scene:
    gameCanvas gets the coordinates of the canvas and the text layer
    */
    public void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterDamage;
        CharacterEvents.characterHealed += CharacterHeal;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterDamage;
        CharacterEvents.characterDamaged -= CharacterHeal;
    }

    //Character takes damage on hit, health lost is equal to damage taken.
    public void CharacterDamage(GameObject character, int damageTaken)
    {
        // Create appropriate Damage Text object at character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // Create the UI element and instatiate it onto the canvas layer
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        //Cast the integer to string to show value in damage text
        tmpText.text = damageTaken.ToString();
    }

    //Character restores health on object pickup, health recovered is equal to heal.
    public void CharacterHeal(GameObject character, int healthRecovered)
    {
        // Create appropriate Health Text object at character heal
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        //Cast the integer to string to show value in Health text
        tmpText.text = healthRecovered.ToString();
    }

    //Allows the user to exit the game when pressing the specified input key.
    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false; //If playing the build in the unity editor
#elif (UNITY_STANDALONE)
                   Application.Quit(); //If playing in a standalone build of the software
#elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene"); //If playing a browser based version of the software
#endif
        }
    }
}
