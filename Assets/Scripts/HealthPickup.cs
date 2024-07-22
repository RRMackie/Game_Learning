using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    // The amount of health that is restored by the pickup.
    public int healthRestore = 20;

    // The rotation speed of the health pickup object.
    public Vector3 rotationSpeed = new Vector3 (0, 150, 0);


    // Start is called before the first frame update
    void Start()
    {
        
    }

//Touching the pickup will restore the health of the Game Object 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        // Check if the Game Object has the damagable component, restore health and then destroy the health pickup object
        if(damageable)
        {
           bool canHeal = damageable.Heal(healthRestore);
           
           if(canHeal)
               Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Add a spin animation to the pickup object
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }


}
