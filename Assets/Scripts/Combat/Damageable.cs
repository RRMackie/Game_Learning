using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    /*
    The damagable script adds health values to a game object to allow them to be attacked by
    other game objects, with a death state when health reaches 0.

    If you do not want a game object to be destuctible then  do not add this as a component.

    Health and Max health can be directly set for a specific game object within the Unity Inspector.

    The damageable script is referenced in any combat related scripts or animations.
    */
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            //If health drops to 0 or below the value, the character enters the death state
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    //Set a characters alive state
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsALive set" + value);
        }
    }

    //Locks the players velocity constricting movement for specific states.
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    // Called when component exists in scene
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        //Create a timer to remove invincibilty state after a character is hit
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                //Remove the invincibility frames after a charcter is hit
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    //Remove health when a character is hit and give them invincibility frames
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //Notify other components that the damagable Game Object was hit to apply the relevant damage and knockback
            // The "?" checks that any updated componenets are not null to avoid errors/crashes
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        else
        {
            //Game object was unable to be hit
            return false;
        }
    }

    //Allow the character to restore health
    public bool Heal(int healthRestore)
    {
        // Debug log
        Debug.Log($"Heal called with: {healthRestore}");

        // Check if the character is alive and add the restored health to the characters total health
        // Health restored is calculated through the characters max health value and their current health value,
        // with the true health restored based on the difference and capped at max health.
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int trueHeal = Mathf.Min(maxHeal, healthRestore);
            Health += trueHeal;
            //Trigger the event and create the heath text UI object reflecting the actual value of health restored.
            if (CharacterEvents.characterHealed != null)
            {
                CharacterEvents.characterHealed.Invoke(gameObject, trueHeal);
            }
            return true;
        }

        return false;

    }
}
