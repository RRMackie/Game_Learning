using UnityEngine;
using UnityEngine.InputSystem;

//Cannot add the PlayerController script to a game object without the required component
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float airSpeed = 3f;
    public float jumpForce = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;


    //Set the players move speed
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                //Allow the player to move if not touching a wall
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    //Check if player is on the ground or in air state before using a set speed
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            //Use the run speed for the player
                            return runSpeed;
                        }
                        else
                        {
                            //Use the set walk speed for the player
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        //In the air the player moves at a different speed
                        return airSpeed;
                    }
                }
                else
                {
                    //Idle speed is 0
                    return 0;
                }

            }
            else
            {
                //Movement locked due to current player state
                return 0;
            }

        }
    }



    //Check if Player is moving to cycle between idle, walking and running animations
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
         private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    //Check if the player is currently running and update the character animations (Tied to a key press in game)
    [SerializeField]
    private bool _isRunning = false;

     public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
         private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    // Check the direction the player is facing and change movements/character animations as required
    public bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; } private set{
    if(_isFacingRight != value)
    {
        //Flip the local scale to make the player face the opposite direction
        transform.localScale *= new Vector2(-1, 1);
    }
        _isFacingRight = value;
        
    } }

    //Check if the player is able to move during specific actions (Set in a animation behaviours through boolean conditions)
    public bool CanMove { get
    {
        return animator.GetBool(AnimationStrings.canMove);
    }

    }

    //Check if the player is in the Alive State
    public bool IsAlive {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

  

    Rigidbody2D rb;
    Animator animator;

    // Called when component exists in scene
    private void Awake()
    {
        //Get Rigidbody and Animator componenets on Awake and set it
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();

    }

//Update the player characters current movement speed
    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        //Update yVelocity to switch animations between player falling and player rising
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }

//Check the direction the player is facing and turn the sprite around
    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //Face the right
            IsFacingRight = true;

        }else if(moveInput.x < 0 && IsFacingRight)
        {
            //Face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    //Allow player to jump if conditions are met

    public void OnJump(InputAction.CallbackContext context)
    {
        //Check if the player can jump depending on current state
       if(context.started && touchingDirections.IsGrounded && CanMove)
       {
        animator.SetTrigger(AnimationStrings.jumpTrigger);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

       }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    //Apply the damage and knockback to player when hit
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
