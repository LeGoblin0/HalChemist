using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angelica : Enemy01
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //MakeShoot();
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

    public float Speed1 = 3;
    public float Speed2 = 3;
    public float Speed3 = 3;

    bool PlySS = false;
    public Transform AttShoots;
    public float ShootSpeed = 5;
    public Transform[] PPPP;

    Rigidbody2D rig;
    
    public void MakeShoot()
    {
        NowPatt = 0;
        StopCoolTime = COOLDOWN;
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
    public void SetIdle(float a)
    {
        StopCoolTime = a;
    }
    public int COOLDOWN = 2;//cooltime
    bool AngBossN = false;//페이즈 
    bool OnDownT = false;//Down
    // Update is called once per frame
    public Transform DeDeshPr;
    public Color DeshCol;
    public float DeshPrTime = 0.03f;
    public float DeshPrStayTime = 0.3f;
    float cool=0;
    override protected void Update()
    {
        if (cool<=0&&(ani.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ang_Att08-2") || ani.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ang_Att08-3"))) 
        {
            cool = DeshPrTime;
            Transform aa = Instantiate(DeDeshPr);
            aa.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            aa.GetComponent<SpriteRenderer>().flipX = transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
            aa.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);

            aa.GetComponent<SpriteRenderer>().material.SetColor("_Desh", DeshCol);
            Destroy(aa.gameObject, DeshPrStayTime);
        }
        else
        {
            cool -= Time.deltaTime;
        }
        if (ani.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ang_Att08-2") &&( 8 >= transform.position.y|| (421 >= transform.position.x || 437 <= transform.position.x)))
        {
            ani.SetInteger("State", 77);
            //Debug.Log(0);
            //transform.position = new Vector3(transform.position.x, 8, transform.position.z);
            
        }
        if (ani.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ang_Att08-3") && (421 >= transform.position.x || 437 <= transform.position.x)) 
        {
            SetSSS();
        }
        ani.SetBool("PlySen", SenserPly);
        if (Die) return;
        base.Update();
        if (Hp <= 50 && !NWater) 
        {
            NWater = true;
            //Invoke("Water1On", 0);
            //Invoke("Water2On", 10);
            //Invoke("Water3On", 20);
        }
        if (SenserPly && !PlySS)
        {
            ani.SetInteger("State", 0);
            PlySS = true;
        }
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
                StopCoolTime = COOLDOWN / 2;
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
            StopCoolTime = COOLDOWN + 13.5f;
            //ani.SetInteger("State", 0);
        }
        else if ((NowPatt == 8 || NowPatt == 9) && StopCoolTime < -1001.1f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
            //Invoke("SetSSS", BeemTime);
        }
        else if (StopCoolTime < -1030f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if (StopCoolTime <= -100 && StopCoolTime > -1000)//이동
        {

            if (NowPatt == 8)
            {
                return;
            }
            Debug.Log(NowPatt - 1);
            if (PPPP.Length <= NowPatt - 1 || NowPatt <= 0)
            {
                StopCoolTime = COOLDOWN;
                ani.SetInteger("State", 0);
            }
            else if (new Vector3(PPPP[NowPatt - 1].position.x - transform.position.x, PPPP[NowPatt - 1].position.y - transform.position.y).sqrMagnitude <= 0.1)//이동끝 1번
            {
                transform.position = new Vector3(PPPP[NowPatt - 1].position.x, PPPP[NowPatt - 1].position.y, transform.position.z);
                NNMM = false;
                rig.velocity = Vector2.zero;
                MoveTrue(false);
                StopCoolTime = -1000;
                if (430 < transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (430 > transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                if (NowPatt == 1 || NowPatt == 2)
                {
                    ani.SetInteger("State", 1);
                    float[] a = { .5f, 1.2f, 1.9f, 2 };

                    for (int i = 0; i < 100; i++)
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
                else if (NowPatt == 8)
                {
                    ani.SetInteger("State", NowPatt);
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
                if (PPPP[NowPatt - 1].position.x < transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (PPPP[NowPatt - 1].position.x > transform.position.x)
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
            while (ff == NowPatt)
            {
                if (AngBossN) NowPatt = Random.Range(6, 9);
                else NowPatt = Random.Range(1, 4);

            }
            ff = NowPatt;
            if (NowPatt == 8)
            {
                plpo = new Vector3(GameSystem.instance.Ply.position.x, 8);
                ani.SetInteger("State", NowPatt);
                if (plpo.x < transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (plpo.x > transform.position.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                rig.velocity = new Vector2(Random.Range(-CaveSpeed, CaveSpeed), Random.Range(0, CaveSpeed));
            }
            MoveTrue(true);
        }
        else if (StopCoolTime >0) //대기시간 끝나면 패턴 설정
        {
            rig.velocity = Vector3.zero;
        }


            base.Start();
    }
    int ff = 0;
    bool NNMM = false;

    public void upM()
    {
        rig.velocity = new Vector2(0, 1)* Speed1;
    }
    Vector3 plpo;
    public void FrM()
    {
        rig.velocity = new Vector2(transform.GetChild(0).GetComponent<SpriteRenderer>().flipX ? -1 : 1, 0) * -Speed3;
    
    }
    public void StopM()
    {
        rig.velocity = Vector2.zero;
    }
    public void SetSSS()
    {
        rig.velocity = Vector2.zero;
        NowPatt = 8;
        ani.SetInteger("State", 0);
        StopCoolTime = COOLDOWN+1;
    }
    public void PlM()
    {
        rig.velocity = new Vector2(transform.position.x - plpo.x, transform.position.y - plpo.y).normalized * -Speed2;
    }
    public void FrPly()
    {
        if((transform.GetChild(0).GetComponent<SpriteRenderer>().flipX ? transform.position.x >= GameSystem.instance.Ply.position.x : transform.position.x <= GameSystem.instance.Ply.position.x) )
        {
            rig.velocity = Vector2.zero;
            ani.SetInteger("State", 0);
            StopCoolTime = COOLDOWN;
        }
        else
        {
            ani.SetInteger("State", 9);
        }
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
    int SSh = 0;
    public void RanSS()
    {
        SSh = Random.Range(0, 2);
    }
    public void Shotss(int a)
    {
        if (a < 2) ; 
        else SSh = a;
        OnDownT = false;
        transform.GetChild(4).localScale = new Vector3(transform.GetChild(0).GetComponent<SpriteRenderer>().flipX ? -1 : 1, 1, 1);
        transform.GetChild(4).GetChild(SSh).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(4).GetChild(SSh).gameObject.SetActive(true);
        SSh = ++SSh % 2;
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
