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
        if (aggro)
        {

        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(currentVelocity,0), 0.6f, platform);
            
            if (hit.collider != null)
            {
                Debug.Log("Here");
                currentVelocity = -currentVelocity;
            }
            if (IsGrounded() & Time.time > noMoveUntil)
            {
                rb.velocity = new Vector2(currentVelocity, rb.velocity.y);
                //rb.AddForce(new Vector2(currentVelocity, 0), ForceMode2D.Impulse);
                
            }
            
        }
    }

    public override void TakeDamage(float damage, float knockback, Vector2 playerLocation)
    {
        Vector2 directionFromPlayer = new Vector2(gameObject.transform.position.x - playerLocation.x, gameObject.transform.position.y - playerLocation.y);
        directionFromPlayer.Normalize();
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Dead");
        }
        else
        {
            noMoveUntil = Time.time + 0.4f;
            rb.AddForce(new Vector2((directionFromPlayer.x * knockback * 9) / weight, (((directionFromPlayer.y * 9) + 5) * knockback ) / weight), ForceMode2D.Impulse);
            //rb.velocity = new Vector2((directionFromPlayer.x * knockback)/weight, ((directionFromPlayer.y + 2) * knockback)/weight);
        }
    }

    private bool IsGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.05f, platform);
        return raycastHit.collider != null;
    }
}
