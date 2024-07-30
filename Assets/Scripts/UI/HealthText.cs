using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
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

 

    // Called when component exists in scene.
    private void Awake()
    {
        // Gets the current transform coordinates of the health text in the canvas layer
        textTransform = GetComponent<RectTransform>();
        // Gets the values for the health text from the component
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Get the starting colour values for the health text object
        initalColor = textMeshPro.color;
    } 



   
    private void Update()
    {
        //Apply a percentage of the move speed to the text layer at a consistent amount
        textTransform.position += moveSpeed * Time.deltaTime;

        //Increment time each frame
        timePassed += Time.deltaTime;
        
        //Change health text object alpha colour to 0 over time to fade the object out
        if(timePassed < fadeTime)
        {
            float alphaFade = initalColor.a * (1 -(timePassed/fadeTime));
            textMeshPro.color = new Color(initalColor.r, initalColor.g, initalColor.b, alphaFade);
        }else
        {
            Destroy(gameObject);
        }
    }
}
