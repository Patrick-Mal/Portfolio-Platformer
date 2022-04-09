using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public GameOverScreen gameOverScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {

            //If the object collided with has the HazardStats component
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0;
        gameOverScreen.Activate();
    }
}
