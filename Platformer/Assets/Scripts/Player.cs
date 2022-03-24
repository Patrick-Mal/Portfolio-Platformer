using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Collide");

            if (other.gameObject.TryGetComponent<HazardStats>(out var hazardStats))
            {
                
                TakeDamage(hazardStats.damage, hazardStats.knockback, other.transform.position);
            }
            else
            {
                Debug.LogWarning("HazardStats not found on " + other.gameObject.name);
                TakeDamage(5, 10, other.transform.position);
            }
        }
        
    }

    override public void Die()
    {
        Debug.Log("Dead");
    }
}
