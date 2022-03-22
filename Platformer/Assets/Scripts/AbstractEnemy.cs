using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Enemy : MonoBehaviour
{
    public float health;
    public float weight;
    public GameObject player;

    public virtual void TakeDamage(float damage, float knockback, Vector2 damageLocation)
    {
    }


}
