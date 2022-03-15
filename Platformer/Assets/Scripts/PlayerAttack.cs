using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float range;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            LayerMask actor = LayerMask.GetMask("Actor");
            Debug.Log("Attack");
            RaycastHit2D[] attackTargets;
            attackTargets = Physics2D.BoxCastAll(gameObject.transform.position, new Vector2(range, 5), 0f, new Vector2(1,0), 0f, actor);
            Debug.Log(attackTargets.Length);
            foreach(RaycastHit2D i in attackTargets)
            {
                Debug.Log(i);
            }
        }
    }
}
