
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Skeleton : MonoBehaviour
{
    //Set the speed the Game Object moves at.
    public float accelerationSpeed = 30f;
    
    //Set the max speed of the Game Object.
    public float maxSpeed = 3f;

    //Set the detection distance of wall colliders to stop at.
    public float walkStopRate = 0.06f;

    //Designate attack detection zone
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    //Check the direction the Game Object is facing.
    public enum FacingDirection {Right, Left}

    //Set the inital walking direction to the right.
    public Vector2 WalkDirectionVector = Vector2.right;

   
    private FacingDirection _walkDirection;

     //Check the current facing direction and move the Game Object. 
    //If the Game Object collides with walls (other colliders), turn in the other direction and continue walking
    public FacingDirection WalkDirection
    {
        get { return _walkDirection;}
        set {
            if(_walkDirection != value)
            {
                //Flip the facing direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x *-1, gameObject.transform.localScale.y);

                if(value == FacingDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                } else if(value == FacingDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            
            
            _walkDirection = value;}
    }

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

    //Check the Attack Cooldown value in the animation parameters and set it
    // to the appropriate value after the Game Object has used an attack.
    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);

        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));

        }
    }

    // Called when component exists in scene
    // Rigidbody for physics values.
    // Animator for values tied to animatons.
    // Touching Directions for movement and direction orientation.
    // Damageable for health and hit values.
    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

  // Update is called once per frame
  // If the other Game Object enters the detection zone, then this Game Object sets the value to has target to true.
  // If the Game Object has attacked another Game Object, the attack cooldown timer will decrease until zero and able to attack again.
    void Update()
    {
       HasTarget = attackZone.detectedColliders.Count > 0;
       
       if(AttackCooldown > 0)
       {
        AttackCooldown -= Time.deltaTime;
       }
       
    }
     //When this Game Object is on the ground and touching a wall, it will turn the opposite direction by calling the function.
     //If the Game Object is not damaged and can move, it will move in the facing direction at the set walk speed.
    void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
             if(CanMove && touchingDirections.IsGrounded)
             //Character will start to accellerate towards max Speed
             // The clamp value limits the speed depending on facing direction
            rb.velocity = new Vector2(
                 Mathf.Clamp(rb.velocity.x + (accelerationSpeed *WalkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
        else
           rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate),rb.velocity.y);
        }
       
    }

    // The Game Object will walk in the direction its facing, and set a new facing direction if colliding with another object/collider.
    // If the Game Object does not have an inital direction an error message will be printed to the console.
    private void FlipDirection()
    {
        if(WalkDirection == FacingDirection.Right)
        {
            WalkDirection = FacingDirection.Left;
        }else if(WalkDirection == FacingDirection.Left)
        {
            WalkDirection = FacingDirection.Right;
        } else
        {
            Debug.Log("Current facing direction is not set to right or left");
        }
    }

//Applies damage and knockback to the Game Obeject when detecting a hit.
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetection()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
  
}
