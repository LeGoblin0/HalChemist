using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angelica : Enemy01
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        MakeShoot();
    }
    float StopCoolTime = 5;
    public float MoveSpeed = 3;
    bool MoveNow = false;
    int NowPatt = 0;
    
    bool PlySS = false;
    public Transform AttShoots;
    public float ShootSpeed = 5;
    public Transform[] PPPP;
    
    public void MakeShoot()
    {
        ani.SetInteger("State", 0);
        int af = 0;
        Transform ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

        af++;
        ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

        af++;
        ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

        af++;
        ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

        af++;
        ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

        af++;
        ff = Instantiate(AttShoots).transform;
        ff.position = transform.GetChild(0).GetChild(af).position;
        ff.parent = transform.GetChild(0).GetChild(af);

    }
    public int COOLDOWN = 2;//cooltime
    bool AngBossN = false;//페이즈 
    // Update is called once per frame
    override protected void Update()
    {
        if (Hp <= 50 && !NWater) 
        {
            NWater = true;
            Invoke("Water1On", 0);
            Invoke("Water2On", 10);
            Invoke("Water3On", 20);
        }
        if (SenserPly) PlySS = true;
        if (!PlySS) return;
        StopCoolTime -= Time.deltaTime;

        if (Hp <= 30 && StopCoolTime > 0 && !AngBossN)
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
                StopCoolTime = COOLDOWN;
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
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if ((NowPatt == 8 || NowPatt == 9) && StopCoolTime < -1001.1f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if (StopCoolTime < -1030f)
        {
            //Debug.Log(NowPatt);
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
        }
        else if (StopCoolTime <= -100 && StopCoolTime > -1000)//이동
        {
            if (new Vector3(PPPP[NowPatt-1].position.x - transform.position.x, PPPP[NowPatt-1].position.y - transform.position.y).sqrMagnitude <= 0.001)//이동끝 1번
            {
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
                    Invoke("Shoot0", Random.Range(.5f, 2));
                    Invoke("Shoot1", Random.Range(.5f, 2));
                    Invoke("Shoot2", Random.Range(.5f, 2));
                }
                if (NowPatt == 5)
                {
                    ani.SetInteger("State", 5);
                    int aa = -1;
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
                        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
                    }
                    if (transform.GetChild(0).GetChild(++aa).childCount > 0)
                    {
                        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(transform.GetChild(0).GetChild(aa).GetChild(0).position.x - transform.position.x, transform.GetChild(0).GetChild(aa).GetChild(0).position.y - transform.position.y) * ShootSpeed;
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
                transform.position += new Vector3(PPPP[NowPatt - 1].position.x - transform.position.x, PPPP[NowPatt - 1].position.y - transform.position.y).normalized * MoveSpeed * Time.deltaTime;
            }
        }
        else if (StopCoolTime <= 0 && StopCoolTime > -100) //대기시간 끝나면 패턴 설정
        {
            StopCoolTime = -101;
            MoveNow = true;
            if (AngBossN) NowPatt = Random.Range(6, 10);
            else NowPatt = Random.Range(1, 4);
            MoveTrue(true);
        }


        base.Start();
    }
    public void MakePlySh()
    {
        Transform ff = Instantiate(AttShoots).transform;
        ff.position = GameSystem.instance.Ply.position;
        ff.position += new Vector3(0, 0, -.1f);
        ff.GetComponent<BossShoot>().SetDie(.5f);
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
        transform.GetChild(4).localScale = new Vector3(transform.GetChild(0).GetComponent<SpriteRenderer>().flipX ? -1 : 1, 1, 1);
        transform.GetChild(4).GetChild(0).GetComponent<Animator>().SetTrigger("Att");
        transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
    }
}
