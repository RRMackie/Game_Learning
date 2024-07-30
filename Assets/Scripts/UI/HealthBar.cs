using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Get the Health Slider from the Unity Game Canvas
    public Slider healthSlider;

    // Get the Health Bar Text from the Unity Game Canvas
    public TMP_Text healthBarText;

    // Get the damagable componenet 
    Damageable playerDamageable;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(player == null)
        {
            Debug.Log("No 'Player' tagged Object found in the current scene.");
        }
        
        playerDamageable = player.GetComponent<Damageable>();

    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerHealthChanged(int newHealth, int MaxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, MaxHealth);
        healthBarText.text = "HP " + newHealth + " / " + MaxHealth;
    }
}
