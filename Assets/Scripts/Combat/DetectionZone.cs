using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    /*
    The detection zone script is used to trigger other conditions when a game object with a collider
    enters the specificed zone. This is used primarily for enemy NPC objects to detect the player when
    entering a hitbox.

    This script is not attached to game object directly, but to child collision zone.
    */
    public UnityEvent ColliderCooldown;

    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (detectedColliders.Count <= 0)
        {
            ColliderCooldown.Invoke();
        }
    }
}
