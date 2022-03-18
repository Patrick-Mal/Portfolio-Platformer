using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Enemy : MonoBehaviour
{
    public float health;
    public float weight;

    public virtual void takeDamage(float damage, float knockback)
    {
        //insert default implementation to be inherited here
    }

    void Update()
    {
        
    }

}
