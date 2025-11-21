using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    //movement
    public float moveSpeed = 5f;

    //Ground Check
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    //Rigid Body
    private Rigidbody2D rb;
    private float currentHorizontalInput = 0f;
    private FacingDirection facing = FacingDirection.right;

    //Jump Settings
    public float apexHeight = 3.5f;
    public float apexTime = 0.5f;

    //Gravity
    private float gravity;
    private float jumpVelocity;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVelocity = (2 * apexHeight) / apexTime;
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        //Vector2 playerInput = new Vector2();

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetButtonDown("Jump") ? 1 : 0);
        MovementUpdate(playerInput);
    }

    public void MovementUpdate(Vector2 playerInput)
    {
        currentHorizontalInput = playerInput.x;


        rb.linearVelocity = new Vector2(currentHorizontalInput * moveSpeed, rb.linearVelocity.y);

        if (currentHorizontalInput > 0)
            facing = FacingDirection.right;
        else if (currentHorizontalInput < 0)
            facing = FacingDirection.left;

        JumpUpdate(playerInput.y);
    }

    private void JumpUpdate (float jumpPressed)
    {
        bool grounded = IsGrounded();

        if (grounded && jumpPressed == 1 && !isJumping)
        {
            isJumping = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }

        if (isJumping)
        {
            rb.linearVelocity += new Vector2(0, gravity * Time.deltaTime);

            if (rb.linearVelocity.y <= 0)
            {
                rb.gravityScale = 1f;
                isJumping = false;
            }
        }
    }
    public bool IsWalking()
    {
        return Mathf.Abs(currentHorizontalInput) > 0.01f;
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    public FacingDirection GetFacingDirection()
    {
        return facing; ;
    }
}
