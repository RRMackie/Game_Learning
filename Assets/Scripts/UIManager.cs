using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Instance of damage text
    public GameObject damageTextPrefab;
    // Instance of Health text
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;

    //Called when the component exists in scene
    public void Awake()
    {
        // Find the first object of type canvas and get its values
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
}
