using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Enemy
{
    public float speed;
    bool aggro;
    BoxCollider2D bc;

    float currentVelocity;


    Vector3 playerPos;

    [SerializeField] private LayerMask platform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        aggro = false;
        currentVelocity = speed;
    }

    void FixedUpdate()
    {
        playerPos = player.transform.position;
        if (aggro)
        {
            //If it can move freely
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                //Gives 1 if player is to the right or -1 if player is to the left
                float directionValue = ((playerPos - transform.position).x / Mathf.Abs((playerPos - transform.position).x));
                //Sets new velocity towards the player
                rb.velocity = new Vector2(speed * directionValue, rb.velocity.y);
            }
                
            //If the player is out of range, de-aggro
            if ((playerPos - transform.position).magnitude > 8)
            {
                Debug.Log("De-aggro");
                aggro = false;
            }
        }
        else
        {
            //Check if it has collided with a wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(currentVelocity, 0), 0.6f, platform);

            if (hit.collider != null)
            {
                //Switch direction
                currentVelocity = -currentVelocity;
            }
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                //Move (Slightly slower than if it was aggroed
                rb.velocity = new Vector2(currentVelocity * 0.8f , rb.velocity.y);

            }
            if ((playerPos - transform.position).magnitude < 3)
            {
                aggro = true;
            }


        }
    }


    bool IsGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, platform);
        return raycastHit.collider != null;
    }
}
