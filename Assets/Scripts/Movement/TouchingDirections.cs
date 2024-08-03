using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    /*
    This class deals with checking and setting the required facing direction for game objects,
    as sprites, animations and attacks are tied to local scale values. This script detects the 
    appropriate direction and sets the appropriate valuse.

    This script is also referenced by moving game objects to detect collision with walls and ceilings, to 
    provide a smoother platforming experience. It's main purpose it to stop the player character from getting
    stuck in ground geomtry and allow NPCs to know when to turn.
    */
    public ContactFilter2D castFilter;

    // Set the value for the minimum contact distance the object has with the ground.
    public float groundDistance = 0.05f;
    // Set the value for the minimum contact distance the object has with the walls.
    public float wallDistance = 0.2f;
    // Set the value for the minimum contact distance the object has with the ceiling.
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Rigidbody2D rb;

    [SerializeField]
    private bool _isGrounded = true;
    //Check if the game object touching the ground.
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall = true;
    //Check if the player character is colliding with a wall.
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling = true;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    //Check if the player character is colliding with the ceiling.
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    /*
     Called when component exists in scene
     Rigidbody for physics values.
     touchingCol for a game objects collider values.
     Animator for values tied to animatons.
     */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    //
    //Check to see if the Player is touching the ground each frame.
    public void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
