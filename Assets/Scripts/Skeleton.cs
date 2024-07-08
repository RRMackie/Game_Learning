
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Skeleton : MonoBehaviour
{
    //Set the speed the Enemy Skeleton walks at
    public float walkSpeed = 5f;
    public float walkStopRate = 0.06f;
    public DetectionZone attackZone;
    

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    //Check the direction the skeleton is facing
    public enum FacingDirection {Right, Left}
    public Vector2 WalkDirectionVector = Vector2.right;

    private FacingDirection _walkDirection;

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

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

  // Update is called once per frame
    void Update()
    {
       HasTarget = attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
             if(CanMove)
            rb.velocity = new Vector2(walkSpeed * WalkDirectionVector.x, rb.velocity.y);
        else
           rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate),rb.velocity.y);
        }
       
    }

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

//Applies damage and knockback when Game Object is hit.
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
  
}
