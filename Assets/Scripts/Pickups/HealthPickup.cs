using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    /*
    Simulate a health pickup object within the game. When the character collides with the Health
    pickup and is damaged, they will restore health and the pickup will be destroyed.

    This class is set up to allow for varying pickups that could be used in the game world, so 
    values such as the amount of health restored can be set using the Unity Inspector.

    The pickup also has an audio cue and rotation to animate it.
    */

    // The amount of health that is restored by the pickup.
    public int healthRestore = 20;

    // The rotation speed of the health pickup object. Animates a spinning motion to the object.
    public Vector3 rotationSpeed = new Vector3(0, 150, 0);

    // The healed Audio Source Component
    AudioSource pickupSource;

    /*
    Called when component exists in scene.
    Audio Source for any Audio components attached to the Game Object.
    */
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    //Touching the pickup will restore the health of the Game Object 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        // Check if the Game Object has the damagable component, restore health and then destroy the health pickup object
        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            // Checks if the game object can be healed
            bool canHeal = damageable.Heal(healthRestore);

            if (canHeal)
            {
                if (pickupSource)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                /*
                USed for debugging the health pickup restore value. 
                Destroy Immediatly is only used for Edit Mode Unit Testing.
                The Health Pickup only destroys itself when running in a build and not in the Unity Editor!
                */

#if UNITY_EDITOR
                DestroyImmediate(gameObject);
                Debug.Log("The Health Pickup only destroys itself when running in a build");
#else
                Destroy(gameObject);
#endif
            }
        }
    }

    public void Update()
    {
        // Add a spin animation to the pickup object
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }
}
