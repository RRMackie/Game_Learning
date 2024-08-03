using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    /*
    This script is designed to instantiate visual numbers reflecting health changes for the game object.
    The text is made from user interface text mesh objects and is created at the point of the damage, 
    for both player character and NPC alike.

    There are two health text prefabs depending on the cause of the health change:
    Red - Health Damage
    Green - Health Restore
    */

    // The speed the text object moves at (Pixels per second).
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    // Set the time for the health text to fade away after appearing.
    public float fadeTime = 1f;
    // Time passed since the health text has appeared.
    private float timePassed = 0f;
    // Get the starting colour values (r,g,b,a) of the health text object.
    private Color initalColor;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    /*
   Called when component exists in scene:
   TextTransform gets the coordinates of the canvas and the text layer
   TextMeshPro gets the values for the health text from the component
   IntitalColour gets the starting colour values for the health text object
   (The spelling of color is due to many of the Unity built in sytems using the American
   spelling, so variable names are the same for consistency)
   */
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        initalColor = textMeshPro.color;
    }

    private void Update()
    {
        //Apply a percentage of the move speed to the text layer at a consistent amount
        textTransform.position += moveSpeed * Time.deltaTime;
        //Increment time each frame
        timePassed += Time.deltaTime;
        //Change health text object alpha colour to 0 over time to fade the object out
        if (timePassed < fadeTime)
        {
            float alphaFade = initalColor.a * (1 - (timePassed / fadeTime));
            textMeshPro.color = new Color(initalColor.r, initalColor.g, initalColor.b, alphaFade);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
