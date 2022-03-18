using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public float range;

    Vector3 mousePos;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 relMousePos;
            RaycastHit2D[] attackTargets;
            LayerMask actor = LayerMask.GetMask("Actor");

            relMousePos.x = mousePos.x - transform.position.x;
            relMousePos.y = mousePos.y - transform.position.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            attackTargets = Physics2D.BoxCastAll(gameObject.transform.position, new Vector2(0.2f, 0.2f), angle, relMousePos, range, actor);
            Debug.Log(attackTargets.Length);
            foreach(RaycastHit2D i in attackTargets)
            {
                
                //i.transform.gameObject.SendMessage("TakeDamage", damage);
                i.transform.gameObject.GetComponent<Enemy>().takeDamage(damage, 5);
            }
        }
    }
}
