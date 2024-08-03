using NUnit.Framework;
using UnityEngine;

public class HealthPickupEditModeTests
{
    /*
   Unit tests to confirm that the Health pickup works as intended before entering
   play mode of the application in the unity editor.

   Tests focus on initalizing the object wtih default values, and health restore methods.

   when healing the player character, testing the health restored and its
   requirement to be destroyed after use.

   Please note that due to this unit test, the health pickup object will only destroy itself
   when running a build of the application and not in the Unity editor.
   */
    private GameObject pickupObject;
    private HealthPickup healthPickup;
    private GameObject damageableObject;
    private Damageable damageable;

    //Create a mock of the health pickup game object using default values and components, as well as a damgaged game object to be healed.
    [SetUp]
    public void SetUp()
    {
        // Create and set up the HealthPickup object.
        pickupObject = new GameObject("HealthPickup");
        healthPickup = pickupObject.AddComponent<HealthPickup>();
        healthPickup.healthRestore = 20;
        healthPickup.rotationSpeed = new Vector3(0, 150, 0);

        // Create and set up the Damageable object.
        damageableObject = new GameObject("Damageable");
        damageable = damageableObject.AddComponent<Damageable>();
        damageable.MaxHealth = 100;
        damageable.Health = 50;
        // Add colliders.
        var pickupCollider = pickupObject.AddComponent<BoxCollider2D>();
        pickupCollider.isTrigger = true;
        //Damage the mock game object.
        var damageableCollider = damageableObject.AddComponent<BoxCollider2D>();
        damageableCollider.isTrigger = true;
        // Ensure objects are placed at the same position for collision.
        damageableObject.transform.position = new Vector3(0f, 0f, 0f);
        pickupObject.transform.position = new Vector3(0f, 0f, 0f);
    }
    //Destroy both mock objects after tests are complete.
    [TearDown]
    public void TearDown()
    {
        // Clean up any remaining objects
        if (pickupObject != null)
        {
            Object.DestroyImmediate(pickupObject);
        }
        if (damageableObject != null)
        {
            Object.DestroyImmediate(damageableObject);
        }
    }

    //Check the health pickup restores the health of the damaged object.
    [Test]
    public void Pickup_RestoreHealth()
    {
        // Check initial health
        Assert.AreEqual(50, damageable.Health, "Initial health should be 50.");
        // Directly call OnTriggerEnter2D to simulate a pickup
        var damageableCollider = damageableObject.GetComponent<Collider2D>();
        Assert.IsNotNull(damageableCollider, "Damageable object should have a Collider2D.");
        // Simulate trigger event
        healthPickup.OnTriggerEnter2D(damageableCollider);
        // Check health restoration
        Assert.AreEqual(70, damageable.Health, "Health should be restored by the pickup amount.");
    }
}
