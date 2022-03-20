using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Enemy
{
    public override void TakeDamage(float damage, float knockback)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Dead");
        }
        else
        {
            Debug.Log(health);
        }
    }


}
