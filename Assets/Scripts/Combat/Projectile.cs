using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //Damage the projectile will cause on hit
    public int damage = 10;

    // Speed the projectile will move at.
    public Vector2 moveSpeed = new Vector2(5f, 0);

    // Can add knockback to projectile, similar to ground attack.
    public Vector2 knockback = new Vector2(0, 0);

    // Lifetime of the projectile before it is destroyed (in seconds)
    [Header("Lifetime Settings")]
    [SerializeField] private float lifetime = 5f;

    // Number of hits before the projectile is destroyed
    [Header("Collision Settings")]
    [SerializeField] private int maxHits = 1;

    Rigidbody2D rb;
    Animator animator;
    private int hitCount = 0;

    // Called when component exists in scene
    // Rigidbody for physics values.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        StartCoroutine(DestroyAfterLifetime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
