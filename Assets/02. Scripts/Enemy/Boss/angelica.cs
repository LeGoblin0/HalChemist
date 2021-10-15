﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angelica : Enemy01
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        MakeShoot();
        rig = GetComponent<Rigidbody2D>();
    }
    float StopCoolTime = 5;
    public float MoveSpeed = 3;
    public float CaveSpeed = 7;
    public float DisSpeed = 0.2f;

    public float DownTime = 2;
    bool MoveNow = false;
    int NowPatt = 0;

    public float BeemTime = 0;


    bool PlySS = false;
    public Transform AttShoots;
    public float ShootSpeed = 5;
    public Transform[] PPPP;

    Rigidbody2D rig;
    
    public void MakeShoot()
    {
        ani.SetInteger("State", 0);
        int af = 0;
        Transform ff;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

        af++;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

        af++;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

        af++;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

        af++;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

        af++;
        if (transform.GetChild(0).GetChild(af).childCount == 0)
        {
            ff = Instantiate(AttShoots).transform;
            ff.position = transform.GetChild(0).GetChild(af).position;
            ff.parent = transform.GetChild(0).GetChild(af);
        }

    }
    public int COOLDOWN = 2;//cooltime
    bool AngBossN = false;//페이즈 
    bool OnDownT = false;//Down
    // Update is called once per frame
    override protected void Update()
    {
        if (Die) return;
        base.Update();
        if (Hp <= 50 && !NWater) 
        {
            NWater = true;
            //Invoke("Water1On", 0);
            //Invoke("Water2On", 10);
            //Invoke("Water3On", 20);
        }
        if (SenserPly) PlySS = true;
        if (!PlySS) return;
        StopCoolTime -= Time.deltaTime;

        if (Hp <= 30 && StopCoolTime > 0 && (!AngBossN || (ani.GetCurrentAnimatorStateInfo(0).IsName("Ang_Idle") && NowPatt != 5))) 
        {
            StopCoolTime = -101;
            AngBossN = true;
            NowPatt = 5;
        }
        //Debug.Log(StopCoolTime+"  "+ NowPatt);
        if ((NowPatt == 1 || NowPatt == 2) && StopCoolTime < -1003)
        {
            if (transform.GetChild(0).GetChild(0).childCount == 0 && transform.GetChild(0).GetChild(4).childCount == 0)
            {
                StopCoolTime = -100;
                NowPatt = 4;
                ani.SetInteger("State", 0);

            }
            else
            {
                StopCoolTime = COOLDOWN/2;
                ani.SetInteger("State", 0);
                //transform.GetChild(0).GetChild(3).GetChild(0).parent = transform.GetChild(0).GetChild(0);
                //transform.GetChild(0).GetChild(4).GetChild(0).parent = transform.GetChild(0).GetChild(1);
                //transform.GetChild(0).GetChild(5).GetChild(0).parent = transform.GetChild(0).GetChild(2);
            }
        }
        else if ((NowPatt == 3) && StopCoolTime < -1002.3f)
        {
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);

        }
        else if ((NowPatt == 4) && StopCoolTime < -1003)
        {
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if ((NowPatt == 5) && StopCoolTime < -1002.3f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if ((NowPatt == 6 || NowPatt == 7) && StopCoolTime < -1001f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN+3.5f;
            ani.SetInteger("State", 0);
        }
        else if ((NowPatt == 8 || NowPatt == 9) && StopCoolTime < -1001.1f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN+1;
            Invoke("SetSSS", BeemTime);
        }
        else if (StopCoolTime < -1030f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if (StopCoolTime <= -100 && StopCoolTime > -1000)//이동
        {
            if (new Vector3(PPPP[NowPatt-1].position.x - transform.position.x, PPPP[NowPatt-1].position.y - transform.position.y).sqrMagnitude <= 0.1)//이동끝 1번
            {
                NNMM = false;
                rig.velocity = Vector2.zero;
                MoveTrue(false);
                if (430 < transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (430 > transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                StopCoolTime = -1000;
                if (NowPatt == 1 || NowPatt == 2)
                {
                    ani.SetInteger("State", 1);
                    float[] a = { .5f, 1.2f, 1.9f ,2};

                    for(int i = 0; i < 100; i++)
                    {
                        int ch1 = Random.Range(0, 3);
                        int ch2 = Random.Range(0, 3);
                        a[3] = a[ch1];
                        a[ch1] = a[ch2];
                        a[ch2] = a[3];
                    }

                    Invoke("Shoot0", a[0]);
                    Invoke("Shoot1", a[1]);
                    Invoke("Shoot2", a[2]);
                }
                else if (NowPatt == 6 || NowPatt == 7)
                {
                    ani.SetInteger("State", NowPatt);
                    OnDownT = true;
                }
                else if (NowPatt == 5)
                {
                    ani.SetInteger("State", 5);
                    int aa = -1;
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * 0;
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Die");
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                }
                else
                {
                    ani.SetInteger("State", NowPatt);
                }
            }
            else
            {
                if (PPPP[NowPatt-1].position.x < transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (PPPP[NowPatt-1].position.x > transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                NNMM = true;
            }
        }
        else if (StopCoolTime <= 0 && StopCoolTime > -100) //대기시간 끝나면 패턴 설정
        {
            StopCoolTime = -101;
            MoveNow = true;
            if (AngBossN) NowPatt = Random.Range(6, 10);
            else NowPatt = Random.Range(1, 4);
            MoveTrue(true);
            rig.velocity = new Vector2(Random.Range(-CaveSpeed, CaveSpeed), Random.Range(-CaveSpeed, CaveSpeed));
        }


        base.Start();
    }
    bool NNMM = false;
    void SetSSS()
    {
        ani.SetInteger("State", 0);
    }
    private void FixedUpdate()
    {
        if (Die) return;
        if (NNMM)
        {
            //Debug.Log(rig.velocity);
            rig.velocity = rig.velocity - rig.velocity * DisSpeed * Time.deltaTime;
            transform.position += new Vector3(PPPP[NowPatt - 1].position.x - transform.position.x, PPPP[NowPatt - 1].position.y - transform.position.y).normalized * (MoveSpeed) * Time.deltaTime;
        }
    }
    public void MakePlySh()
    {
        Transform ff = Instantiate(AttShoots).transform;
        ff.position = GameSystem.instance.Ply.position;
        ff.position += new Vector3(0, +.5f, -.1f);
        ff.GetComponent<Animator>().SetTrigger("SSS");
        ff.GetComponent<BossShoot>().SetDie(1.5f);
    }
    void MoveTrue(bool NN)
    {
        ani.SetBool("Move", NN);
        int aa = -1;
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
        if (transform.GetChild(0).GetChild(++aa).childCount > 0) transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetBool("Move", NN);
    }
    void Shoot0()
    {
        int aa = NowPatt * 3 - 3;

        if (transform.GetChild(0).GetChild(aa).childCount == 0) return;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3( transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    void Shoot1()
    {
        int aa = NowPatt * 3 - 2;
        if (transform.GetChild(0).GetChild(aa).childCount == 0) return;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    void Shoot2()
    {
        int aa = NowPatt * 3 - 1;
        if (transform.GetChild(0).GetChild(aa).childCount == 0) return;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3( transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (OnDownT && collision.GetComponent<StoneDieAni>() != null) 
        {
            ani.SetTrigger("Down");
            Invoke("SetSSS", DownTime);
        }
        
    }
    public GameObject[] WaterOn;
    bool NWater = false;
    void Water1On()
    {
        WaterOn[0].SetActive(true);
    }
    void Water2On()
    {
        WaterOn[1].SetActive(true);
    }
    void Water3On()
    {
        WaterOn[2].SetActive(true);
    }
    public void Shotss()
    {
        OnDownT = false;
        transform.GetChild(4).localScale = new Vector3(transform.GetChild(0).GetComponent<SpriteRenderer>().flipX ? -1 : 1, 1, 1);
        transform.GetChild(4).GetChild(0).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
    }
    protected override void Dieset()
    {
        if (Hp <= 0)
        {
            if (!Die)
            {
                gameObject.layer = 17;
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.layer == 11) transform.GetChild(i).gameObject.layer = 17;
                }
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    if (transform.GetChild(0).GetChild(i).gameObject.layer == 11) transform.GetChild(0).GetChild(i).gameObject.layer = 17;
                }


                Die = true;
                if (DieItem != null && DieItem.Length > 0)
                {
                    for (int i = 0; i < DieItem.Length; i++)
                    {
                        Transform aa = Instantiate(DieItem[i]);
                        aa.position = new Vector3(transform.position.x, transform.position.y, 2);
                        aa.parent = transform.parent;
                        if (aa.GetComponent<Rigidbody2D>() != null)
                        {
                            aa.GetComponent<Rigidbody2D>().sharedMaterial = null;
                            aa.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3f), Random.Range(0, 4f));
                        }
                    }
                }
                for (int i = 0; i < MoneyDie; i++)
                {
                    Transform aa = Instantiate(GameSystem.instance.ItemPre[0]);
                    aa.position = new Vector3(transform.position.x, transform.position.y, 2);
                    aa.parent = transform.parent;
                    aa.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f)) * 5 * transform.GetChild(0).localScale.x;
                    Destroy(aa.gameObject, 5);
                }
                if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.layer = 19;
                ani.SetTrigger("Die");
                GameSystem.instance.PlyAttSlow = false;
                Time.timeScale = 0.2f;
                if (!NoDie) Destroy(gameObject, DieAniTime);
                //사망 애니메이션 실행
                //
            }
            return;
        }
    }
    private void OnDestroy()
    {
        GameSystem.instance.PlyAttSlow = true;
        Time.timeScale = 1;
    }
}
