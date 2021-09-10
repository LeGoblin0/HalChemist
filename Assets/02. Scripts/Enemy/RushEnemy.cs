using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : Enemy01
{
    [Space]
    public float Speed;
    public float RushTime = 0;
    public float RushSpeed = 5;

    Rigidbody2D rig;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (First) return;
        base.Update();//체력감소하면 사망

        if (SenserPly)  //플레이어와 충돌시 러쉬
        {
            ani.SetInteger("state", 1);
            RushTime = 99999999;
            //Invoke("stop", RushTime);
        }
        else
        {
            RushTime -= Time.deltaTime;
            if (RushTime <= 0)
            {
                ani.SetInteger("state", 0);
            }
        }
        rig.gravityScale = 1;

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            rig.velocity = new Vector3(-HitAniknockBack * flip, rig.velocity.y, 0);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            rig.velocity = new Vector3(Speed * flip, rig.velocity.y, 0);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Notice"))
        {
            //rig.velocity = new Vector3(Speed * flip, rig.velocity.y, 0);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Desh"))
        {
            if (GroundKnckBackTime > 0)
            {
                rig.velocity = new Vector3(-GroundKnckBackSpeed * flip, rig.velocity.y, 0);
                GroundKnckBackTime -= Time.deltaTime;
            }
            else rig.velocity = new Vector3(RushSpeed * flip, rig.velocity.y, 0);
        }
    }
    int flip = -1;

    float GroundKnckBackTime = 0;
    public float GroundKnckBackTimeMax = .5f;
    public float GroundKnckBackSpeed = 3;
    public override void GroundSen(bool Out = false)
    {
        base.GroundSen(Out);
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            flip *= -1;
            transform.GetChild(0).localScale = new Vector3(-flip, 1, 1);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Desh") && Out)
        {
            //ani.SetTrigger("Hit");
            GroundKnckBackTime = GroundKnckBackTimeMax;
        }

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set)
        {
            Hp -= collision.GetComponent<Att>().AttDamage;
            if (Hp != 0)
            {
                HHIITime = .2f;
                if (HitAni) ani.SetTrigger("Hit");
                if (transform.position.x > GameSystem.instance.Ply.position.x)
                {
                    flip = -1;
                    transform.GetChild(0).localScale = new Vector3(-flip, 1, 1);
                }
                else
                {
                    flip = +1;
                    transform.GetChild(0).localScale = new Vector3(-flip, 1, 1);
                }
            }

        }
        if (collision.gameObject.tag == "TurnPoint" && !ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) 
        {
            flip *= -1;
            transform.GetChild(0).localScale = new Vector3(-flip, 1, 1);
        }
        if (collision.tag == "TrapGround")
        {
            Hp = 0;
        }
    }
    bool First = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        First = false;
    }
}
