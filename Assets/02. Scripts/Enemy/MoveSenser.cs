using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSenser : MonoBehaviour
{

    Enemy01 enemy;
    Collider2D col;
    void Start()
    {
        gameObject.layer = 17;
        enemy = transform.parent.GetComponent<Enemy01>();
        if (enemy == null)
        {
            enemy = transform.parent.parent.GetComponent<Enemy01>();
        }
        Rigidbody2D rig;
        if (GetComponent<Rigidbody2D>() == null)
        {
            rig = gameObject.AddComponent<Rigidbody2D>();
        }
        else
        {
            rig = GetComponent<Rigidbody2D>();
        }
        rig.bodyType = RigidbodyType2D.Kinematic;
        col = GetComponent<Collider2D>();
        a = groundNum;
    }
    public int groundNum = 1;
    int a = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (a <= 0)
        {
            Invoke("Oncol", .1f);
            col.enabled = false;
            enemy.GroundSen();
            a = groundNum;
        }
        else a--;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("Oncol", .1f);
        col.enabled = false;
        enemy.GroundSen();
    }
    void Oncol()
    {
        col.enabled = true;
    }
}
