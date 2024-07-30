using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Check if the platform has been activated
    public bool platformActive = false;
    
    //Set the flying speed for the Game Object.
    public float moveSpeed = 3f;

    // Set the distance the between the waypoint and the game object.
    public float waypointReachedDistance = 0.1f;


    // Create a list of waypoints for the Game Object to follow
    // Waypoints are set within the Unity Inspector from Objects within the heirarchy
    public List<Transform> waypoints = new List<Transform>();

    // Components required by the entity.
    Rigidbody2D rb;
    Transform nextWaypoint;

    // Default number of waypoints within the list
    // Adjusted for each instance within the Unity Inpspector
    int waypointNum = 0;


    // Called when component exists in scene
    // Rigidbody for physics values.
    // Animator for values tied to animatons.
    // Damageable for health and hit values.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }


    private void FixedUpdate()
    {
        if (platformActive)
        {

            Move();

        }
        else

        rb.velocity = Vector3.zero;

    }

    // Set the movement of the Game Object through using a series of waypoints for the object to move between
    // The number of waypoints is adjusted in the Unity inspector, and the Game Object will move to each in sequence
    // before looping back to the inital waypoint.
    private void Move()
    {
        // Fly to next waypoint.
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        // Check if the Game Object has already reached the waypoint.
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * moveSpeed;

        // See if the game object needs to switch to the the next waypoint.
        if (distance <= waypointReachedDistance)
        {
            //Change Waypoint
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                //Loop back to inital waypoint
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];

        }
    }

    public void ActivatePlatform()
    {
        platformActive = true;
    }

}
