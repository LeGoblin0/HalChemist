using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Vector2[] GoSet;
    public bool GoNow = true;
    public TrainButton[] But;

    public float Speed = 3;
    float NowSpeed = 0;
    public bool AcSpeedOff = false;
    public float LastSpeed = 1;
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
        if (last != null)
        {
            if (((Vector2)(transform.position - last.position)).sqrMagnitude > 0.001)
            {
                if (NowSpeed > 1f && !AcSpeedOff) NowSpeed -= Time.deltaTime;
                else NowSpeed = LastSpeed;
                rig.velocity = -(transform.position - last.position).normalized * NowSpeed;
            }
            else
            {
                //Debug.Log(transform.position);
                //Debug.Log(new Vector3((int)(transform.position.x + 0.5f), (int)(transform.position.y + 0.5f)));
                //Debug.Log(new Vector3((transform.position.x + 0.5f), (transform.position.y + 0.5f)));
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));//정확한 좌표에 정지
                //Debug.Log(transform.position);
                last = null;
                NowSpeed = 0;
            }
        }
        else if (GoNow)
        {
            if (NowSpeed < Speed && !AcSpeedOff) NowSpeed += Time.deltaTime;
            else NowSpeed = Speed;
            rig.velocity = GoSet[GoNum] * NowSpeed;
            //Debug.Log(GoSet[GoNum]+"   "+GoNum);
        }
        else
        {
            ani.SetInteger("State", 0);
            rig.bodyType = RigidbodyType2D.Static;
            for (int i = 0; But != null && i < But.Length; i++)
            {
                But[i].OutBut();
            }
        }
    }
    public int GoNum = 0;
    public void GoTrain(int a)
    {
        if (GoNow) return;
        GoNum = a;
        GoNow = true;
        rig.bodyType = RigidbodyType2D.Dynamic;
        ani.SetInteger("State", 1);
        last = null;
        //Debug.Log(lastlast);
        if (GoSet[GoNum] == Vector2.zero)
        {
            GoNow = false;
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));//정확한 좌표에 정지
            ani.SetInteger("State", 0);
            rig.bodyType = RigidbodyType2D.Static;
            last = null;
            for (int i = 0; But != null && i < But.Length; i++)
            {
                But[i].OutBut();
            }
        }
    }
    public void GoTrain()
    {
        if (GoNow) return;
        GoNum = (GoNum + 1) % GoSet.Length;
        GoNow = true;
        rig.bodyType = RigidbodyType2D.Dynamic;
        ani.SetInteger("State", 1);
        last = null;
        //Debug.Log(lastlast);
        if (GoSet[GoNum] == Vector2.zero)
        {
            GoNow = false;
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));//정확한 좌표에 정지
            ani.SetInteger("State", 0);
            rig.bodyType = RigidbodyType2D.Static;
            last = null;
            for (int i = 0; But != null && i < But.Length; i++)
            {
                But[i].OutBut();
            }
        }
    }

    Transform last;
    Transform lastlast;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(lastlast + "   " + collision.name);
        if (collision.tag == "TrainSet" && lastlast != collision.transform) 
        {
            last = collision.transform;
            lastlast = last;
            GoSet = collision.GetComponent<TrainSet>().GoTT;
            if (collision.GetComponent<TrainSet>().Stop)
            {
                GoNow = false;
            }

        }
    }
}
