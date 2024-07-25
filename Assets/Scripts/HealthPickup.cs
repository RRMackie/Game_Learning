using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    // The amount of health that is restored by the pickup.
    public int healthRestore = 20;

    // The rotation speed of the health pickup object. Animates a spiral motion to the object in the game
    public Vector3 rotationSpeed = new Vector3 (0, 150, 0);

    // The healed Audio Source Component
    AudioSource pickupSource;

    // Called when component exists in scene
    // Audio Source for any Audio components attached to the Game Object
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

//Touching the pickup will restore the health of the Game Object 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        // Check if the Game Object has the damagable component, restore health and then destroy the health pickup object
        if(damageable && damageable.Health < damageable.MaxHealth)
        {
           // Checks if the game object can be healed
           bool canHeal = damageable.Heal(healthRestore);

            if (canHeal)
            {
                if (pickupSource)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

                Destroy(gameObject);

            }
                
        }
    }

    private void Update()
    {
        // Add a spin animation to the pickup object
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }


}
