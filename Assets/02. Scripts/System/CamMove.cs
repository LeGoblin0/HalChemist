using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform Target;
    public float MaxSpeed = 5;
    public float AcSpeed = 3;
    float Speed=0;
    float SpeedY=0;
    public Vector2 Offset;

    [HideInInspector]
    public bool RightSen;
    [HideInInspector]
    public bool LeftSen;
    [HideInInspector]
    public bool UpSen;
    [HideInInspector]
    public bool DownSen;


    Player ply;
    Rigidbody2D rig;
    void Start()
    {
        ply = Target.GetComponent<Player>();
        rig = GetComponent<Rigidbody2D>();
    }
    public bool XLock = false;
    public bool YLock = false;
    private void FixedUpdate()
    {
        Vector3 goPos = new Vector3(Target.position.x + Offset.x * RL, Target.position.y + Offset.y);
        if ( Move)
        {
            Speed += Time.deltaTime * AcSpeed;
        }
        if (MoveY)
        {
            if (UpSen)
            {
                if (SpeedY < 0) SpeedY = 0;
                SpeedY += Time.deltaTime * AcSpeed * Mathf.Abs(transform.position.y - goPos.y);
            }
            else if (DownSen)
            {
                if (SpeedY > 0) SpeedY = 0;
                SpeedY -= Time.deltaTime * AcSpeed * Mathf.Abs(transform.position.y - goPos.y);
            }
        }
        else
        {
            if(SpeedY * SpeedY < 0.1f)
            {
                SpeedY = 0;
            }
            else if (SpeedY > 0)
            {
                SpeedY -= Time.deltaTime * AcSpeed;
            }
            else if (SpeedY < 0)
            {
                SpeedY += Time.deltaTime * AcSpeed;
            }
            else
            {
                SpeedY = 0;
            }
        }
        if (XLock) Speed = 0;
        if (YLock) SpeedY = 0;
        //Debug.Log(Speed);
        rig.velocity = -new Vector3((transform.position.x - goPos.x) * Speed, -SpeedY, 0);

        if (rig.velocity.y > 10) rig.velocity = new Vector2(rig.velocity.x, 10);
        else if (rig.velocity.y < -10) rig.velocity = new Vector2(rig.velocity.x, -10);
        if (rig.velocity.x > 10) rig.velocity = new Vector2(10, rig.velocity.y);
        else if (rig.velocity.x < -10) rig.velocity = new Vector2(-10, rig.velocity.y);

        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Move = false;
            if (Speed > 0.1f)
            {
                Speed -= Time.deltaTime * AcSpeed;
            }
            else
            {
                Speed = 0;
            }
        }

    }
    int RL = 1;
    public bool Move = false;
    public bool MoveY = false;
    public float RightSenser = 3;
    public float LeftSenser = -3;
    public float UpSenser = 3;
    public float DownSenser = -3;
    private void OnCollisionStay2D(Collision2D collision)
    {
       // Speed = 0;
    }
    void Update()
    {
        if (transform.position.x + RightSenser < Target.position.x)//우센서
        {
            Move = true;
            RightSen = true;
            LeftSen = false;
            RL = 1;
            Speed = MaxSpeed;
        }
        else if (transform.position.x + LeftSenser > Target.position.x)//좌센서
        {
            Move = true;
            RightSen = false;
            LeftSen = true;
            RL = -1;
            Speed = MaxSpeed;
        }
        else
        {

            RightSen = false;
            LeftSen = false;
        }

        if (transform.position.y + UpSenser < Target.position.y)//상
        {
            MoveY = true;
            UpSen = true;
            DownSen = false;
        }
        else if (transform.position.y + DownSenser > Target.position.y)//하
        {
            MoveY = true;
            UpSen = false;
            DownSen = true;
        }
        else
        {
            UpSen = false;
            DownSen = false;
            MoveY = false;
        }
        Debug.DrawLine(new Vector3(LeftSenser, UpSenser) + transform.position, new Vector3(RightSenser, UpSenser) + transform.position, UpSen ? Color.red : Color.green);
        Debug.DrawLine(new Vector3(LeftSenser, DownSenser) + transform.position, new Vector3(RightSenser, DownSenser) + transform.position, DownSen ? Color.red : Color.green);
        Debug.DrawLine(new Vector3(LeftSenser, DownSenser) + transform.position, new Vector3(LeftSenser, UpSenser) + transform.position, LeftSen ? Color.red : Color.green);
        Debug.DrawLine(new Vector3(RightSenser, DownSenser) + transform.position, new Vector3(RightSenser, UpSenser) + transform.position, RightSen ? Color.red : Color.green);
        Debug.DrawLine(transform.position, (Vector2)transform.position+ rig.velocity,  Color.yellow);
    }
}
