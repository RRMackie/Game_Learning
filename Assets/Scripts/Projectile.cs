using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //Damage the projectile will cause on hit
    public int damage = 10;

    // Speed the projectile will move at (Can have Y value to be extended to other projectile objects)
    public Vector2 moveSpeed = new Vector2(5f, 0);

    // Can add knockback to projectile, similar to ground attack.
    public Vector2 knockback = new Vector2(0,0);

    Rigidbody2D rb;
    Animator animator;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {
             Vector2 knockbackDirection = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
           
            //Hit the target Game Object
            bool gotHit = damageable.Hit(damage, knockbackDirection);
            
            if(gotHit)
            Debug.Log(collision.name + "hit for" + damage);
            
            //Destroy the projectile on hit.
            Destroy(gameObject);
        }
    }

}
