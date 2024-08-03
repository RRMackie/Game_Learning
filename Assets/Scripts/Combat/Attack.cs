using UnityEngine;

public class Attack : MonoBehaviour
{
    /*
    The Attack script will activate the attack sequence and attack animation of the game object.
    The attack damage value and knockback strength values cab be changed in the Unity Inspector
    */
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    //Trigger the attack sequence and attack
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the Game Object can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 knockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            //Hit the target Game Object
            bool gotHit = damageable.Hit(attackDamage, knockbackDirection);

            if (gotHit)
                Debug.Log(collision.name + "hit for" + attackDamage);
        }
    }
}
