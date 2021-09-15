using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manlime : Enemy01
{
    [Space]
    [Header("한걸음마다 가는거리 픽셀단위")]
    public float MovePos = 3;

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

        if (SenserPly && ani.GetCurrentAnimatorStateInfo(0).IsName("manlime00"))  //플레이어와 충돌시 러쉬
        {
            ani.SetInteger("state", 1);
            //Debug.Log(ani.GetInteger("state"));
            //Invoke("stop", RushTime);
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
        rig.gravityScale = 1;

    }
    public void GoMove()
    {
        transform.position += new Vector3(1 / 45f * MovePos * flip, 0, 0);
    }

    [Header("쏘는 파워")]
    public Vector2 ShootPower;
    public void ShootAtt()
    {
        air = true;
        ani.SetBool("Air", air);
        //Debug.Log("ATt");
        rig.velocity = new Vector2(ShootPower.x * flip, ShootPower.y);
    }
    int flip = -1;

    float GroundKnckBackTime = 0;
    public float GroundKnckBackTimeMax = .5f;
    public float GroundKnckBackSpeed = 3;
    bool air = false;
    public override void GroundSen(bool Out = false)
    {
        base.GroundSen(Out);
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("manlime00"))
        {
            flip *= -1;
            transform.GetChild(0).localScale = new Vector3(-flip, 1, 1);
        }
        //else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Desh") && Out)
        //{
        //    //ani.SetTrigger("Hit");
        //    GroundKnckBackTime = GroundKnckBackTimeMax;
        //}

    }
    public void DownD()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("manlime01"))
        {
            air = false;
            ani.SetBool("Air", air);
            ani.SetInteger("state", 2);
        }
    }
    public void EndDownD()
    {
        ani.SetInteger("state", 0);
    }
    public GameObject[] col;
    public void ChangeCol(int a)
    {
        for (int i = 0; i < col.Length; i++)
        {
            if (a == i) continue;
            col[i].SetActive(false);
        }
        col[a].SetActive(true);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set)
        {
            //Debug.Log(Hp + "  " + collision.GetComponent<Att>().AttDamage);
            Hp -= collision.GetComponent<Att>().AttDamage;
            if (Hp != 0)
            {
                HHIITime = .2f;
                if (HitAni) ani.SetTrigger("Hit");
            }

            if (ani.GetCurrentAnimatorStateInfo(0).IsName("manlime00"))
            {
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
