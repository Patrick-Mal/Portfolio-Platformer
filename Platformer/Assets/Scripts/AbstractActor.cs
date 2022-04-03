using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The abstract class that all enemies and players inherit from directly or indirectly
abstract public class Actor : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float maxHealth;
    public float currentHealth;
    public float weight = 5;

    [HideInInspector]
    public float noMoveUntil;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage, float knockback, Vector2 damageLocation)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Knockback(knockback, damageLocation);
        }
    }

    public virtual void Knockback(float knockback, Vector2 damageLocation)
    {
        if (rb != null)
        {
            //Create a directional vector from the damage source to the actor taking damage
            Vector2 directionFromHazard = new Vector2(gameObject.transform.position.x - damageLocation.x, gameObject.transform.position.y - damageLocation.y);
            directionFromHazard.Normalize();
            //Create a pure -1 or 1 direction of knockback along the x axis
            float leftRight = (directionFromHazard.x / Mathf.Abs(directionFromHazard.x));
            //Applies the force, partially away from the damage source and partially a flat amount up and left or right.
            //Provides a larger knockback when attacking upwards but still has some knockback when attacking down
            rb.AddForce(new Vector2(((directionFromHazard.x * knockback * 2) + ( leftRight * knockback * 5) ) / weight, ((directionFromHazard.y * 1.5f * knockback) + (2.5f * knockback)) / weight), ForceMode2D.Impulse);
            noMoveUntil = Time.time + 0.4f;
        }
        else
        {
            Debug.LogError("Rigidbody not found on " + gameObject.name + " for knockback");
        }
    }

    public virtual void Die()
    {
        Object.Destroy(this.gameObject);
    }
}
