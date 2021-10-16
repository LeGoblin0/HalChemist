using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenserPly : MonoBehaviour
{
    public bool CheckOff = false;
    public float ININTime = .1f;
    private void Start()
    {
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
    }
    Enemy01 enemy;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            enemy.SenserPly = true;
            if (CheckOff) Invoke("OffGO", ININTime);
        }
    }
    void OffGO()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.SenserPly = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.SenserPly = false;
        }
    }
}
