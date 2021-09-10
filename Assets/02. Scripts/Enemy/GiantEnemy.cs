using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : Enemy01
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

        if (SenserPly)  //플레이어와 충돌시 러쉬
        {
            ani.SetInteger("state", 1);
            //Debug.Log(ani.GetInteger("state"));
            //Invoke("stop", RushTime);
        }
        rig.gravityScale = 1;

    }
    public void GoMove()
    {
        transform.position += new Vector3(1 / 45f * MovePos * flip , 0, 0);
    }
    public Transform ShootTr;
    public Transform Shoot;

    [Header("구체 쏘는 파워")]
    public Vector2 ShootPower;
    public void ShootAtt()
    {
        //Debug.Log("ATt");
        Transform aa= Instantiate(Shoot);
        aa.position = ShootTr.position;
        aa.GetComponent<Rigidbody2D>().velocity = new Vector2(ShootPower.x * flip, ShootPower.y);
        ani.SetInteger("state", 0);
    }
    int flip = -1;

    float GroundKnckBackTime = 0;
    public float GroundKnckBackTimeMax = .5f;
    public float GroundKnckBackSpeed = 3;
    public override void GroundSen(bool Out = false)
    {
        base.GroundSen(Out);
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("giantlime_Idle"))
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
    public GameObject[] col;
    public void ChangeCol(int a)
    {
        for(int i = 0; i < col.Length; i++)
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
            Debug.Log(Hp + "  " + collision.GetComponent<Att>().AttDamage);
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
