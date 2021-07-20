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

    public bool On_1 = false;
    public bool On_2 = false;
    public bool On_3 = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (--a <= 0 && On_1 && collision.tag == "Ground") 
        {
            Invoke("Oncol", .1f);
            col.enabled = false;
            enemy.GroundSen(On_3);
            a = groundNum;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (On_2 && collision.tag == "Ground") enemy.GroundSen(On_3);
    }
    void Oncol()
    {
        col.enabled = true;
    }
}
