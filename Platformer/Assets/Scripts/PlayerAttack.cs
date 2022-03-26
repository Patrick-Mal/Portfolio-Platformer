using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float range;
    public float knockback;

    Vector3 mousePos;

    bool attackPressed;
    float nextAttackTime;

    private void Start()
    {
        attackPressed = false;
        nextAttackTime = Time.time;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            attackPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (attackPressed & Time.time >= nextAttackTime)
        {
            attackPressed = false;
            nextAttackTime = Time.time + 0.2f;
            
            Vector2 relMousePos = GetRelativeMousePos();
            RaycastHit2D[] attackTargets;
            LayerMask actor = LayerMask.GetMask("Actor");


            //Boxcast towards the mouse with a distance of "range", finding only objects on the actor layer. Acts as a sword swing.
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            attackTargets = Physics2D.BoxCastAll(gameObject.transform.position, new Vector2(0.2f, 0.2f), angle, relMousePos, range, actor);
            //Iterating all items found in the raycast
            foreach (RaycastHit2D i in attackTargets)
            {
                //If the object found inherits from the abstract enemy class, run the takeDamage function.
                if (i.transform.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(damage, knockback, gameObject.transform.position);
                }
            }
            
        }
    }

    //Get a vector of the x and y distance from the player to the mouse.
    Vector2 GetRelativeMousePos()
    {
        Vector2 relMousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Get relative position of the mouse from the player
        relMousePos.x = mousePos.x - transform.position.x;
        relMousePos.y = mousePos.y - transform.position.y;
        return relMousePos;
    }
}
