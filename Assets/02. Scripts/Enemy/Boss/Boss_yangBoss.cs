using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_yangBoss : Enemy01
{
    [Space]
    [Header("-보스-")]
    [Space]
    public float RushSpeed = 5;
    Transform ply;
    Rigidbody2D rig;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rig = GetComponent<Rigidbody2D>();
        ply = GameSystem.instance.Ply;
        BigHitHp = new int[(int)(BigHitTime * 20)];
        for (int i = 0; i < BigHitHp.Length; i++)
        {
            BigHitHp[i] = Hp;
        }
        InvokeRepeating("ChackBigHit", 1f, 0.05f);
        cam = Camera.main.GetComponent<Mcam>();
    }

    public float IdleTime = 3;
    public float StunTime = 4;
    public float JumpPower = 6;
    public float JumpTime = 1;
    public float JumpDownPower = 6;
    public float BigHitTime = .2f;
    public int BigHitPower = 4;
    int[] BigHitHp;
    int bighitnum = 0;
    public Vector2 PlyXPos;
    float NowTime = 5f;
    public GameObject[] HitCol;
    public void ChangeCol(int a)
    {
        for(int i = 0; i < HitCol.Length; i++)
        {
            if (i == a)
            {
                HitCol[i].SetActive(true);
                continue;
            }

            HitCol[i].SetActive(false);

        }
    }
    [Header("카메라")]
    Mcam cam;
    void ChackBigHit()
    {
        //Debug.Log(BigHitHp[bighitnum] - BigHitHp[(bighitnum + 1) % BigHitHp.Length]+"     "+ BigHitPower);
        //Debug.Log((bighitnum + 1) % BigHitHp.Length + "  " + bighitnum);
        if (BigHitHp[bighitnum] - BigHitHp[(bighitnum + 1) % BigHitHp.Length] >= BigHitPower) 
        {
            StunOn();
        }
        BigHitHp[bighitnum] = Hp;
        bighitnum = (bighitnum + 1) % BigHitHp.Length;
    }
    private void FixedUpdate()
    {
        if (Stop) return;
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (NowTime <= 0)
            {
                int a = Random.Range(1, 4);
                if (a == 3)
                {
                    NowTime = JumpTime;
                    PlyXPos = ply.position;
                }
                ani.SetInteger("State", a);
            }
            else
            {
                if (transform.position.x <= 225)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else if (transform.position.x >= 238)
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < ply.position.x)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Stun_2"))
        {
            if (NowTime <= 0)
            {
                ani.SetInteger("State", 5);
            }
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att1_2"))
        {
            rig.velocity = new Vector2(transform.GetChild(0).localScale.x, 0) * RushSpeed;
            rig.gravityScale = 1;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att2_2"))
        {
            rig.gravityScale = 0;
            rig.velocity = new Vector2(0, JumpPower);
            if (NowTime <= 0)
            {
                ani.SetInteger("State", 6);
                rig.gravityScale = 0;
                //Debug.Log(ply.position+"    " + transform.position);
            }
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att2_3"))
        {
            float plyXX = ply.position.x;
            if (plyXX < 225) plyXX = 224;
            else if (plyXX > 238) plyXX = 238;
            PlyXPos = new Vector2(plyXX - transform.position.x, -2 - transform.position.y).normalized;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att2_4"))
        {
            rig.gravityScale = 0;
            rig.velocity = PlyXPos * JumpDownPower;
            //Debug.Log(PlyXPos);
        }
    }
    bool Stop = true;

    protected override void Update()
    {
        //Debug.Log(Stop + "   " + SenserPly);
        if (SenserPly && Stop)
        {
            Stop = false;
            PlyXPos = new Vector2(0, -1);
        }
        if (Stop) return;
        base.Update();
        NowTime -= Time.deltaTime;
     

    }
    public float SlowTime = .2f;
    public void DDD()
    {
        if (transform.position.y > -.8f)
        {
            ani.SetInteger("State", 21);
        }
        rig.bodyType = RigidbodyType2D.Kinematic;
        Time.timeScale = SlowTime;
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = First;
    }
    public void DDD2()
    {
        Time.timeScale = 1;
    }
    public void StunOn()
    {
        //Debug.Log(0);
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Stun_1") && !ani.GetCurrentAnimatorStateInfo(0).IsName("Stun_2") && !ani.GetCurrentAnimatorStateInfo(0).IsName("Stun_3"))
        {
            //Debug.Log(1);
            NowTime = StunTime;
            ani.SetTrigger("Hit");
            rig.gravityScale = 1;
            //camsh.CamMove(.2f); //흔들림 효과
        }
    }
    public Transform EEE;
    public Transform EEEPos;
    public void ShootEEE()
    {
        Transform a;
        a = Instantiate(EEE);
        a.position = EEEPos.position;
        a.parent = transform.parent;
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * transform.GetChild(0).localScale.x, 0);
        a.GetChild(0).GetComponent<BoxCollider2D>().size = Vector2.one * 100;
        a = Instantiate(EEE);
        a.position = EEEPos.position;
        a.parent = transform.parent;
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * transform.GetChild(0).localScale.x, 2);
        a.GetChild(0).GetComponent<BoxCollider2D>().size = Vector2.one * 100;
        a = Instantiate(EEE);
        a.position = EEEPos.position;
        a.parent = transform.parent;
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * transform.GetChild(0).localScale.x, 4);
        a.GetChild(0).GetComponent<BoxCollider2D>().size = Vector2.one * 100;
    }
    public void AniIdle()
    {
        NowTime = IdleTime;
        rig.gravityScale = 1;
        rig.velocity = Vector2.zero;
        ani.SetInteger("State", 0);
    }
    
    public override void GroundSen(bool Out = false)
    {
        base.GroundSen(Out);

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set)
        {
            Hp -= collision.GetComponent<Att>().AttDamage;
            if (Hp != 0)
            {
                HHIITime = .2f;
            }

        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att1_2") && collision.tag == "TurnPoint") 
        {
            ani.SetInteger("State", 4);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Att2_4") && collision.gameObject.tag == "Ground") 
        {
            rig.gravityScale = 1;
            rig.velocity = Vector2.zero;
            ani.SetInteger("State", 7);
        }
    }
}
