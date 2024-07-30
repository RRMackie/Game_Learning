using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDemon : MonoBehaviour
{
    //Set the flying speed for the Game Object.
    public float flightSpeed = 3f;

    // Set the distance the between the waypoint and the game object.
    public float waypointReachedDistance = 0.1f;

    // The detection zone for the Game Object to detect others.
    public DetectionZone biteDetectionZone;

    // Create a list of waypoints for the Game Object to follow
    // Waypoints are set within the Unity Inspector from Objects within the heirarchy
    public List<Transform> waypoints = new List<Transform>();

    // Components required by the entity.
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Transform nextWaypoint;

    // Default number of waypoints within the list
    // Adjusted for each instance within the Unity Inpspector
    int waypointNum = 0;


    public bool _hasTarget = false;

    //Get the animation parameters if the game object has another specified Game Object within the detection zone and set appropriate value.
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    //Check if the game object has met the conditions to allow movement and set the appropriate value within the animation parameter.
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    // Called when component exists in scene
    // Rigidbody for physics values.
    // Animator for values tied to animatons.
    // Damageable for health and hit values.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }



    // Update is called once per frame
    private void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    
    }

    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            // The flying Game Object falls straight to the ground
            rb.gravityScale = 2f;
            rb.velocity = new Vector2 (0,rb.velocity.y);
        }
    }

    // Set the flying movement of the Game Object through using a series of waypoints for the object to move between
    // The number of waypoints is adjusted in the Unity inspector, and the Game Object will fly to each in sequence
    // before looping back to the inital waypoint.
    private void Flight()
    {
        // Fly to next waypoint.
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        // Check if the Game Object has already reached the waypoint.
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

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

    private void UpdateDirection()
    {
        //Objects current local scale values.
        Vector3 objScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            // Facing the right direction
            if(rb.velocity.x < 0)
            {
                //Flip the facing direction
                transform.localScale = new Vector3(-1 * objScale.x, objScale.y, objScale.z);
            }

        }
            
        else
        {
            //Facing the left direction
            if(rb.velocity.x > 0)
            {
                //Flip the current facing direction
                transform.localScale = new Vector3(-1 * objScale.x, objScale.y, objScale.z);
            }


        }
    }
}
