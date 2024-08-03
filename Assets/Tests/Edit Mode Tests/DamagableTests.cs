using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class DamageableTests
{
    /*
    A unit test for methods within the damagable script.
    Although Unity allows for testing in Play mode or Edit mode, this is to check methods
    before playing the application to make sure the default values and behaviours are working as intended. 
    */
    private GameObject testObject;
    private Damageable damageable;
    private Animator animator;

    //Creates a mock game object.
    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the Damageable component.
        testObject = new GameObject();
        damageable = testObject.AddComponent<Damageable>();

        // Add required components
        animator = testObject.AddComponent<Animator>();

        // Mock the Animator component
        damageable.Awake();
    }
    //Destroyes the mock game object after the test is completed.
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObject);
    }

    //Check that the Object with the component initilases as expected.
    [Test]
    public void TestInitialization()
    {
        Assert.AreEqual(100, damageable.MaxHealth);
        Assert.AreEqual(100, damageable.Health);
        Assert.IsTrue(damageable.IsAlive);
        Assert.IsFalse(damageable.isInvincible);
        Assert.AreEqual(0.25f, damageable.invincibilityTime);
    }

    //Check that the health set changes the default value.
    [Test]
    public void Test_HealthSet()
    {
        damageable.Health = 50;
        Assert.AreEqual(50, damageable.Health);
    }

    //Check that the isAlive is false when the health is equal to 0.
    [Test]
    public void Test_Death()
    {
        damageable.Health = 0;
        Assert.IsFalse(damageable.IsAlive);
    }

    //Check that the default state of isAlive can be changed.
    [Test]
    public void Test_IsAliveSetter()
    {
        damageable.IsAlive = false;
        Assert.IsFalse(damageable.IsAlive);
    }

    //Check that an object cannot go beyod max health.
    [Test]
    public void Test_HealWhenFull()
    {
        bool result = damageable.Heal(20);
        Assert.IsFalse(result);
    }

    //Check that the HeealthChanged event works as intended
    [Test]
    public void Test_HealthChangeEvent()
    {
        bool eventInvoked = false;
        damageable.healthChanged = new UnityEvent<int, int>();
        damageable.healthChanged.AddListener((health, maxHealth) => eventInvoked = true);
        damageable.Health = 80;
        Assert.IsTrue(eventInvoked);
    }

}
