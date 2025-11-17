using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    public float moveSpeed = 5f;

    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float currentHorizontalInput = 0f;
    private FacingDirection facing = FacingDirection.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        //Vector2 playerInput = new Vector2();

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
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
