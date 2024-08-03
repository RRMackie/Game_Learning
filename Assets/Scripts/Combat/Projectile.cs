using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
    Sets behaviour for any spawned projectiles within the game.
    Values can be set within the Unity Inspector to allow for different projectiles.

    The projectile has a set lifespan so as to avoid never being deleted if not hitting a target.
    */

    //Damage the projectile will cause on hit
    public int damage = 10;

    // Speed the projectile will move at.
    public Vector2 moveSpeed = new Vector2(5f, 0);

    // Can add knockback to projectile, similar to ground attack.
    public Vector2 knockback = new Vector2(0, 0);

    // Lifetime of the projectile before it is destroyed (in seconds)
    [Header("Lifetime Settings")]
    [SerializeField] public float lifetime = 5f;

    // Number of hits before the projectile is destroyed
    [Header("Collision Settings")]
    [SerializeField] public int maxHits = 1;

    Rigidbody2D rb;
    Animator animator;
    public int hitCount = 0;

    /* 
    Called when component exists in scene
    Rigidbody for physics values.
    Animator to trigger animations
    */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    public void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        StartCoroutine(DestroyAfterLifetime());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 knockbackDirection = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            //Hit the target Game Object
            bool gotHit = damageable.Hit(damage, knockbackDirection);
            hitCount++;

            if (gotHit)
            {
                Debug.Log(collision.name + "hit for" + damage);
                hitCount++;

                // Destroy the projectile if it has hit the maximum number of targets
                if (hitCount >= maxHits)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    private IEnumerator DestroyAfterLifetime()
    {
        // Wait for the lifetime duration before destroying the projectile
        yield return new WaitForSeconds(lifetime);
        if (hitCount < maxHits)
        {
            Destroy(gameObject);
        }
    }
}
