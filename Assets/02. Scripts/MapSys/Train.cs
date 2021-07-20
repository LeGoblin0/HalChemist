using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Vector2 GoSet;
    public bool GoNow = true;
    public TrainButton[] But;

    public float Speed = 3;

    Rigidbody2D rig;
    Animator ani;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GoNow)
        {
            rig.velocity = GoSet * Speed;
        }
    }
    public void GoTrain()
    {
        GoNow = true;
        rig.bodyType = RigidbodyType2D.Dynamic;
        ani.SetInteger("State", 1);
    }

    Transform last;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TrainSet" && last != collision.transform) 
        {
            last = collision.transform;
            GoSet = collision.GetComponent<TrainSet>().GoTT;
            transform.position = new Vector3((int)(transform.position.x + 0.5f), (int)(transform.position.y + 0.5f));//정확한 좌표에 정지
            rig.bodyType = RigidbodyType2D.Static;
            if (collision.GetComponent<TrainSet>().Stop)
            {
                GoNow = false;
                ani.SetInteger("State", 0);
                for (int i = 0; i < But.Length; i++)
                {
                    But[i].OutBut();
                }
            }
        }
    }
}
