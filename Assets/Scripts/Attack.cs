using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

//Trigger the attack sequence and attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the Game Object can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {
            Vector2 knockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //Hit the target Game Object
            bool gotHit = damageable.Hit(attackDamage, knockbackDirection);
            
            if(gotHit)
            Debug.Log(collision.name + "hit for" + attackDamage);

        }
    }
}
