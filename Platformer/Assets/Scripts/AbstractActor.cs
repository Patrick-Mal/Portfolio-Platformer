using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Actor : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    public float health;
    public float weight = 5;

    [HideInInspector]
    public float noMoveUntil;

    public virtual void TakeDamage(float damage, float knockback, Vector2 damageLocation)
    {
        Debug.Log("Take damage " + damage + " " + knockback);
        health -= damage;

        if (health <= 0)
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
            Vector2 directionFromHazard = new Vector2(gameObject.transform.position.x - damageLocation.x, gameObject.transform.position.y - damageLocation.y);
            directionFromHazard.Normalize();
            float leftRight = (directionFromHazard.x / Mathf.Abs(directionFromHazard.x));
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
