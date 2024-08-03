using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TouchingDirectionsPlayModeTests
{
    /*
    A unit test to ensure that the game object movements as intented depending on the
    touching directions script.
    This is primarily focused around ground, wall and ceiling detection working as expected.

    The tests use the player character animator controller to check the parameters constraining movement,
    */
    private GameObject playerObject;
    private TouchingDirections touchingDirections;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private RuntimeAnimatorController animatorController;

    //Create a game object with the expected components and default values.
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create a new GameObject with necessary components
        playerObject = new GameObject("Player");
        rb = playerObject.AddComponent<Rigidbody2D>();
        capsuleCollider = playerObject.AddComponent<CapsuleCollider2D>();
        animator = playerObject.AddComponent<Animator>();
        // Load the Animator Controller from Resources
        animatorController = Resources.Load<RuntimeAnimatorController>("Characters/Player/AC_Player");
        if (animatorController == null)
        {
            Assert.Fail("AnimatorController could not be loaded from Resources.");
        }
        animator.runtimeAnimatorController = animatorController;
        touchingDirections = playerObject.AddComponent<TouchingDirections>();
        // Configure the Collider and Animator as needed for the tests
        capsuleCollider.size = new Vector2(1, 2); // Set a size to simulate the player's collider
        capsuleCollider.offset = new Vector2(0, 0); // Set the collider offset
        // Set default values for distance parameters
        touchingDirections.groundDistance = 0.1f;
        touchingDirections.wallDistance = 0.1f;
        touchingDirections.ceilingDistance = 0.1f;
        yield return null;
    }

    //Destroy the created game object after tests are completed.
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(playerObject);
        yield return null;
    }

    //Check if the created game object is colliding with the ground and the boolean value is as expected.
    [UnityTest]
    public IEnumerator Test_IsGrounded()
    {
        // Move the player object to simulate being grounded.
        playerObject.transform.position = new Vector3(0, -0.9f, 0); // Adjust position to simulate ground contact

        // Set up ground collider
        var groundObject = new GameObject("Ground");
        var groundCollider = groundObject.AddComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(10, 1); // Large enough to cover test area
        groundObject.transform.position = new Vector3(0, -1, 0); // Place below the player
        yield return new WaitForFixedUpdate(); // Wait for FixedUpdate to run
        Assert.IsTrue(touchingDirections.IsGrounded, "Player should be grounded.");
    }
    
    //Check if the created player object is colliding with the wall and the boolean value is expected.
    [UnityTest]
    public IEnumerator Test_IsOnWall()
    {
        // Create a wall object next to the player
        var wallObject = new GameObject("Wall");
        var wallCollider = wallObject.AddComponent<BoxCollider2D>();
        wallCollider.size = new Vector2(1, 5); // Tall wall
        wallObject.transform.position = new Vector3(1.1f, 0, 0); // Position to the right of the player
        // Move the player object close to the wall
        playerObject.transform.position = new Vector3(1, 0, 0); // Adjust position to simulate wall contact
        yield return new WaitForFixedUpdate(); // Wait for FixedUpdate to run
        Assert.IsTrue(touchingDirections.IsOnWall, "Player should be on a wall.");
    }

    //Check if the created player object is colliding with the ceiling and the boolean value is as expected.
    [UnityTest]
    public IEnumerator Test_IsOnCeiling()
    {
        // Create a ceiling object above the player
        var ceilingObject = new GameObject("Ceiling");
        var ceilingCollider = ceilingObject.AddComponent<BoxCollider2D>();
        ceilingCollider.size = new Vector2(10, 1); // Large enough to cover test area
        ceilingObject.transform.position = new Vector3(0, 1.1f, 0); // Position above the player
        // Move the player object close to the ceiling
        playerObject.transform.position = new Vector3(0, 0.9f, 0); // Adjust position to simulate ceiling contact
        yield return new WaitForFixedUpdate(); // Wait for FixedUpdate to run
        Assert.IsTrue(touchingDirections.IsOnCeiling, "Player should be on the ceiling.");
    }
}
