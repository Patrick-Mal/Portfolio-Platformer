using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Enemy : MonoBehaviour
{
    public float health;
    public float weight;

    public virtual void takeDamage(float damage, float knockback)
    {
    }


}
