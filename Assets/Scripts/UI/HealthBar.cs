using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /*
    The health bar is used to give the player a visual and numerical representation of 
    the Player characters current health. The Health bar is only usable by the game object tagged 
    with the player tag in the Unity Inspector.
    
    The Health bar is made up of user interface elements within the unity game canvas object,
    the script updates the healthbar depending on the Unity events concerned with health changes.
    */

    // Get the Health Slider from the Unity Game Canvas.
    public Slider healthSlider;

    // Get the Health Bar Text from the Unity Game Canvas.
    public TMP_Text healthBarText;

    // Get the damagable component. 
    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No 'Player' tagged Object found in the current scene.");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update.
    //Update the Health bar Object.
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    // Called on the health change event to calculate the players current health points.
    private float CalculateSliderPercentage(float currentHealth, float MaxHealth)
    {
        return currentHealth / MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    //Called when the player health changes, and updates the Health Bar with the relevant information.
    private void OnPlayerHealthChanged(int newHealth, int MaxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, MaxHealth);
        healthBarText.text = "HP " + newHealth + " / " + MaxHealth;
    }
}
