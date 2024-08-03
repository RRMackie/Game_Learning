using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ProjectilePlayModeTests
{
    /*
    A unit test script that checks the the expected behaviour of the projectile game
    object during play mode.
    
    The primary methods tested as the checking the object initalises with default values, the object is
    deleted after a period of time and that it moves at the set speed. As the object is instanced using
    local scale, these tests where to make sure there was no errors related to scale changes.

    Damage tests and projectile colliding with damagable game objects will be assessed using User
    Acceptance testing.
    */
    private GameObject projectileObject;
    private Projectile projectile;
    private Rigidbody2D rb;

    //Create the projectile with default values and components.
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create a new GameObject with necessary components for the projectile
        projectileObject = new GameObject("Projectile");
        rb = projectileObject.AddComponent<Rigidbody2D>();
        projectile = projectileObject.AddComponent<Projectile>();
        // Configure the Projectile with default values
        projectile.moveSpeed = new Vector2(5f, 0); // Move speed to the right
        projectile.lifetime = 0.1f; // Short lifetime for quick testing
        projectile.maxHits = 1; // Destroy after 1 hit
        // Set a size and shape for testing
        var collider = projectileObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        yield return null;
    }

    //Destroy the created projectile after tests are completed.
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(projectileObject);
        yield return null;
    }

    //Check that the projectile is destroyed after a period of time.
    [UnityTest]
    public IEnumerator Projectile_DestroyedAfterTime()
    {
        // Ensure the projectile is not destroyed before the test runs
        Assert.NotNull(projectileObject, "Projectile should be created.");
        // Wait for the projectile's lifetime to elapse
        yield return new WaitForSeconds(projectile.lifetime + 0.1f); // Add a small buffer time
        // Assert that the projectile has been destroyed
        Assert.IsNull(GameObject.Find("Projectile"), "Projectile should be destroyed after its lifetime.");
    }

    //Check the object is moving at the default speed when instanced.
    [UnityTest]
    public IEnumerator Projectile_MovesToSetSpeed()
    {
        // Create a new GameObject with necessary components for the projectile
        projectileObject = new GameObject("Projectile");
        var rb = projectileObject.AddComponent<Rigidbody2D>();
        var projectile = projectileObject.AddComponent<Projectile>();
        // Configure the Projectile with default values
        projectile.moveSpeed = new Vector2(5f, 0); // Move speed to the right
        projectile.lifetime = 0.1f; // Short lifetime for quick testing
        projectile.maxHits = 1; // Destroy after 1 hit
        // Set a size and shape for testing
        var collider = projectileObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        // Set initial position
        projectileObject.transform.position = new Vector3(0f, 0f, 0f);
        // Wait for a short time to allow movement
        yield return new WaitForSeconds(0.1f); // Allow some time for movement
        // Check the position after movement
        Assert.IsNotNull(projectileObject, "Projectile object should not be destroyed.");
        Assert.Greater(projectileObject.transform.position.x, 0f, "Projectile should have moved to the right.");
    }
}