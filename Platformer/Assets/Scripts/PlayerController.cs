using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float deceleration;
    public float maxSpeed;
    public float acceleration;
    public float jumpHeight;

    [SerializeField] private LayerMask platform;

    //Variables to define how many seconds before landing on the floor you can press jump and still jump
    //and how many seconds after leaving the floor you can press jump and still jump.
    //Improves game-feel.
    public float earlyJumpLeniency;
    public float lateJumpLeniency;

    //Variables for countdown since space was pressed and since the player was last on the floor.
    //If either is 0, the player cannot jump.
    float jumpQueue;
    float timeSinceGrounded = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Count down for the two jump timers
        timeSinceGrounded -= Time.deltaTime;
        jumpQueue -= Time.deltaTime;

        if (IsGrounded())
        {
            Debug.Log("Grounded");
            timeSinceGrounded = lateJumpLeniency;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime, 0));
        }
        else
        {
            rb.AddForce(new Vector2(rb.velocity.x * -deceleration * Time.deltaTime, 0));
        }
        if(rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpQueue = earlyJumpLeniency;
        }

        Debug.Log(jumpQueue);
        Debug.Log(timeSinceGrounded);

        if (jumpQueue > 0 && timeSinceGrounded > 0)
        {

            jumpQueue = 0;
            timeSinceGrounded = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.2f, platform);
        return raycastHit.collider != null;
    }

}
