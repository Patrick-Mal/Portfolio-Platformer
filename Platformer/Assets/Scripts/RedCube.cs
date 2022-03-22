using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Enemy
{
    public float speed;
    bool aggro;
    Rigidbody2D rb;
    BoxCollider2D bc;

    float currentVelocity;

    float noMoveUntil;

    Vector3 playerPos;

    [SerializeField] private LayerMask platform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        aggro = false;
        currentVelocity = speed;
    }

    private void FixedUpdate()
    {
        playerPos = player.transform.position;
        if (aggro)
        {
            /*
            if((playerPos - transform.position).x > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            */
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                float directionValue = ((playerPos - transform.position).x / Mathf.Abs((playerPos - transform.position).x));
                rb.velocity = new Vector2(speed * directionValue, rb.velocity.y);
            }
                

            if ((playerPos - transform.position).magnitude > 8)
            {
                Debug.Log("De-aggro");
                aggro = false;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(currentVelocity, 0), 0.6f, platform);

            if (hit.collider != null)
            {
                currentVelocity = -currentVelocity;
            }
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                rb.velocity = new Vector2(currentVelocity * 0.8f , rb.velocity.y);
                //rb.AddForce(new Vector2(currentVelocity, 0));

            }
            if ((playerPos - transform.position).magnitude < 3)
            {
                aggro = true;
            }


        }
    }

    public override void TakeDamage(float damage, float knockback, Vector2 damageLocation)
    {
        Vector2 directionFromPlayer = new Vector2(gameObject.transform.position.x - damageLocation.x, gameObject.transform.position.y - damageLocation.y);
        directionFromPlayer.Normalize();
        directionFromPlayer *=  10;
        health -= damage;

        aggro = true;
        if(health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
        else
        {
            noMoveUntil = Time.time + 0.4f;
            rb.AddForce(new Vector2((directionFromPlayer.x * knockback) / weight, (((directionFromPlayer.y) + 5) * knockback ) / weight), ForceMode2D.Impulse);
            //rb.velocity = new Vector2((directionFromPlayer.x * knockback)/weight, ((directionFromPlayer.y + 2) * knockback)/weight);
        }
    }

    private bool IsGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, platform);
        return raycastHit.collider != null;
    }
}
