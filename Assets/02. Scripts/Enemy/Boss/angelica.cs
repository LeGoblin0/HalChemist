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
    // Update is called once per frame
    override protected void Update()
    {
        if (SenserPly) PlySS = true;
        if (!PlySS) return;
        StopCoolTime -= Time.deltaTime;
        Debug.Log(StopCoolTime+"  "+ NowPatt);
        if ((NowPatt == 1 || NowPatt == 2) && StopCoolTime < -1003)
        {
            if (transform.GetChild(0).GetChild(4).childCount == 0)
            {
                StopCoolTime = -100;
                NowPatt = 6;
                ani.SetInteger("State", 0);

            }
            else
            {
                StopCoolTime = COOLDOWN;
                ani.SetInteger("State", 0);
                transform.GetChild(0).GetChild(3).GetChild(0).parent = transform.GetChild(0).GetChild(0);
                transform.GetChild(0).GetChild(4).GetChild(0).parent = transform.GetChild(0).GetChild(1);
                transform.GetChild(0).GetChild(5).GetChild(0).parent = transform.GetChild(0).GetChild(2);
            }
        }
        else if ((NowPatt == 3 || NowPatt == 4 || NowPatt == 5 || NowPatt == 6) && StopCoolTime < -1003)
        {
            StopCoolTime = COOLDOWN;
            ani.SetInteger("State", 0);
            
        }
        else if ((NowPatt == 6) && StopCoolTime < -1003)
        {
            ani.SetInteger("State", 0);
        }
        else if (StopCoolTime <= -100 && StopCoolTime > -1000)
        {
            if (new Vector3(PPPP[NowPatt-1].position.x - transform.position.x, PPPP[NowPatt-1].position.y - transform.position.y).sqrMagnitude <= 0.001)
            {
                if (430 < transform.position.x)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else if (430 > transform.position.x)
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
                StopCoolTime = -1000;
                if (NowPatt == 1 || NowPatt == 2)
                {
                    ani.SetInteger("State", 1);
                    Invoke("Shoot0", Random.Range(.5f, 2));
                    Invoke("Shoot1", Random.Range(.5f, 2));
                    Invoke("Shoot2", Random.Range(.5f, 2));
                }
                else if (NowPatt == 3 || NowPatt == 4 || NowPatt == 5)
                {
                    ani.SetInteger("State", 3);
                    Transform ff = Instantiate(AttShoots).transform;
                    ff.position = GameSystem.instance.Ply.position;
                    ff.position += new Vector3(0, 0, -.1f);
                }
                else if (NowPatt == 6)
                {
                    ani.SetInteger("State", 6);
                }
            }
            else
            {
                if (PPPP[NowPatt-1].position.x < transform.position.x)
                {
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else if (PPPP[NowPatt-1].position.x > transform.position.x)
                {
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
                transform.position += new Vector3(PPPP[NowPatt - 1].position.x - transform.position.x, PPPP[NowPatt - 1].position.y - transform.position.y).normalized * MoveSpeed * Time.deltaTime;
            }
        }
        else if (StopCoolTime <= 0 && StopCoolTime > -100) 
        {
            StopCoolTime = -101;
            MoveNow = true;
            NowPatt = Random.Range(1, 6);
        }


        base.Start();
    }
    void Shoot0()
    {
        int aa = 0;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(-transform.GetChild(0).localScale.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    void Shoot1()
    {
        int aa = 1;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(-transform.GetChild(0).localScale.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    void Shoot2()
    {
        int aa = 2;
        transform.GetChild(0).GetChild(aa).GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector3(-transform.GetChild(0).localScale.x, 0) * ShootSpeed;
        transform.GetChild(0).GetChild(aa).GetChild(0).parent = null;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
    }
}
