using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Rigidbody2D rb;

    public float health;
    public float weight = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void TakeDamage(float damage, float knockback, Vector2 damageLocation)
    {
        Debug.Log("Damage");
        Debug.Log(health);
        health -= damage;

        if (health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
        else
        {
            Vector2 directionFromHazard = new Vector2(gameObject.transform.position.x - damageLocation.x, gameObject.transform.position.y - damageLocation.y);
            directionFromHazard.Normalize();
            directionFromHazard *= 10;
            rb.AddForce(new Vector2((directionFromHazard.x * knockback) / weight, (((directionFromHazard.y) + 5) * knockback) / weight), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hazard")
        {
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
}
