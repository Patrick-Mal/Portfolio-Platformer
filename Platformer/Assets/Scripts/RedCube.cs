using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void takeDamage(float damage, float knockback)
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
