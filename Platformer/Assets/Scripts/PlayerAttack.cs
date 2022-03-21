using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float range;
    public float knockback;

    Vector3 mousePos;

    /*
    private void OnDrawGizmos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = mousePos.x - gameObject.transform.position.x;
        mousePos.y = mousePos.y - gameObject.transform.position.y;

        Quaternion rotation = Quaternion.LookRotation(new Vector2(mousePos.x, mousePos.y), Vector3.up);
        //Gizmos.DrawWireCube(gameObject.transform.position, new Vector2(0.5f, 0.5f));
        Gizmos.color = Color.magenta;
        Gizmos.matrix = Matrix4x4.TRS(gameObject.transform.position, rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, new Vector2(0.2f, 0.2f));
    }
    */

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            //Get the mouse position in a 2d plane
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 relMousePos;
            RaycastHit2D[] attackTargets;
            LayerMask actor = LayerMask.GetMask("Actor");

            //Get relative position of the mouse from the player
            relMousePos.x = mousePos.x - transform.position.x;
            relMousePos.y = mousePos.y - transform.position.y;

            //Boxcast towards the mouse with a distance of "range", finding only objects on the actor layer. Acts as a sword swing
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            attackTargets = Physics2D.BoxCastAll(gameObject.transform.position, new Vector2(0.2f, 0.2f), angle, relMousePos, range, actor);
            Debug.Log(attackTargets.Length);
            //Iterating all items found in the raycast
            foreach(RaycastHit2D i in attackTargets)
            {
                //If the object found inherits from the abstract enemy class, run the takeDamage function.
                if (i.transform.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(damage, knockback, gameObject.transform.position);
                }
            }
        }
    }
}
