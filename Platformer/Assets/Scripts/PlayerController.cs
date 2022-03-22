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

    //Variables for the time until which space counts as pressed and the time until which, the game counts the player as being on the ground.
    //If Time.time exceeds either of these values, the player cannot jump
    float jumpWindow;
    float groundedWindow;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Setting a window until which the player counts as having jumped (to allow for the player to jump very slightly before landing)
        if (Input.GetButtonDown("Jump"))
        {
            jumpWindow = Time.time + earlyJumpLeniency;
        }
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        //Left/Right movement applied to player
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * acceleration, 0));
        }
        //else
        else if(isGrounded)
        {
            //If player isn't pressing left or right, slow character down
            rb.AddForce(new Vector2(rb.velocity.x * -deceleration, 0));
            //Brings the player to a complete stop
            if(-0.8 < rb.velocity.x && rb.velocity.x < 0.8)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            //If player isn't pressing left or right, slow character down
            rb.AddForce(new Vector2(rb.velocity.x * -deceleration / 3, 0));
            //Brings the player to a complete stop
            if (-0.8 < rb.velocity.x && rb.velocity.x < 0.8)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }


        //Implements a max speed
        if(rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        //Setting a window until which the player counts as being grounded (to allow for the player to jump very slightly after leaving a ledge)
        if (isGrounded)
        {
            groundedWindow = Time.time + lateJumpLeniency;
        }
        

        //Jump when conditions are met
        if (Time.time < jumpWindow && Time.time < groundedWindow)
        {
            //Stop double jump
            jumpWindow = Time.time;
            groundedWindow = Time.time;
            //Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //Improves feeling of jump by falling faster
        if (rb.velocity.y < 0)
        {
            rb.velocity +=  Physics2D.gravity.y * fallMultiplier * 0.02f * Vector2.up;
        }
        //Make player not jump as high when not holding space
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity +=  Physics2D.gravity.y * lowJumpMultiplier * 0.02f * Vector2.up;
        }
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.2f, platform);
        return raycastHit.collider != null;
    }

}
