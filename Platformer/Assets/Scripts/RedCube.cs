using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Enemy
{
    public float speed;
    bool aggro;
    BoxCollider2D bc;

    float currentVelocity;

    GameObject gameManager;


    Vector3 playerPos;

    [SerializeField] private LayerMask platform;
    [SerializeField] private LayerMask actor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        aggro = false;
        currentVelocity = speed;
        currentHealth = maxHealth;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager");
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
            if ((playerPos - transform.position).magnitude > 6)
            {
                aggro = false;
            }
        }
        else
        {
            //Check if it has collided with a wall
            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, new Vector2(currentVelocity, 0), 0.6f, platform);

            RaycastHit2D hitActor = Physics2D.Raycast(transform.position, new Vector2(currentVelocity, 0), 0.6f, actor);

            if (hitWall.collider != null || hitActor.collider != null)
            {
                //Switch direction
                currentVelocity = -currentVelocity;
            }
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                //Move (Slightly slower than if it was aggro)
                rb.velocity = new Vector2(currentVelocity * 0.8f , rb.velocity.y);

            }
            if ((playerPos - transform.position).magnitude < 1.5)
            {
                aggro = true;
            }


        }
    }

    public override void Die()
    {
        Object.Destroy(this.gameObject);
        gameManager.GetComponent<Score>().AddScore(1);

    }
    bool IsGrounded()
    {
        RaycastHit2D hitActor = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, actor);
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, platform);
        return (raycastHit.collider != null || hitActor.collider != null);
    }
}
