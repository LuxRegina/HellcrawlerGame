using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 2f;
    public float jumpingPower = 6f;
    private bool isFacingRight = true;
    private int doubleJump = 0;
    private bool Paused = true;

    public GameObject GameOver;

    [Header("Movement variables")]
    [SerializeField] private Rigidbody2D rb; // Handles physics of player
    [SerializeField] private Transform groundCheck; // checks if player is standing on ground to be able to jump
    [SerializeField] private LayerMask groundLayer;  // Gives the player a ground to stand on

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 0;
    }

    void Update()  // Update runs constantly and makes it possible for unity to instantly feel when a button is pressed.
    {
        horizontal = 0f;
       

        //// This handles the animation of the character to switch between "running left/running right".
        if (Input.GetButtonDown(" ") || Input.GetButtonDown(" "))
        {
            anim.Play("RunningAnim");
        }


        //// This Unpauses the game at start when the player moves and presses spacebar.
        if (Input.GetButtonDown(" ") && Paused == true)
        {
            Time.timeScale = 1;
            Paused = false;
        }


        Flip();

    }

    private void FixedUpdate()
    {
        // This handles the physics of our player. How fast we fall, and in what direction.
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // This checks if the player is standing on ground, to know if jumping is possible.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() // This makes sure the character is facing the direction it is moving at all times.
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}