using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishlime : Enemy01
{
    Transform ply;
    override protected void Start()
    {
        base.Start();
        ply = GameSystem.instance.Ply;
    }
    [Header("잠수이동속도")]
    public float MoveSpeed = 3;
    [Header("잠수따라가는시간")]
    public float MoveTime = 2;
    [Header("잠수멈추는시간")]
    public float MoveStopTime = .5f;
    bool setF = true;
    bool Attss = false;
    float TT = 0;
    public void FFFS()
    {

        transform.GetChild(0).position = new Vector3(ply.position.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
    }
    override protected void Dieset()
    {
        if (Hp <= 0)
        {
            if (!Die)
            {
                Die = true;
                if (DieItem != null && DieItem.Length > 0)
                {
                    for (int i = 0; i < DieItem.Length; i++)
                    {
                        Transform aa = Instantiate(DieItem[i]);
                        aa.position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, 2);
                        aa.parent = transform.parent;
                        if (aa.GetComponent<Rigidbody2D>() != null)
                        {
                            aa.GetComponent<Rigidbody2D>().sharedMaterial = null;
                            aa.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 3f), Random.Range(0, 4f));
                        }
                    }
                }
                for (int i = 0; i < MoneyDie; i++)
                {
                    Transform aa = Instantiate(GameSystem.instance.ItemPre[0]);
                    aa.position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, 2);
                    aa.parent = transform.parent;
                    aa.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f)) * 5 * transform.GetChild(0).localScale.x;
                    Destroy(aa.gameObject, 5);
                }
                if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.layer = 19;
                ani.SetTrigger("Die");
                if (!NoDie) Destroy(gameObject, DieAniTime);
                //사망 애니메이션 실행
                //
            }
            return;
        }
    }
    override protected void Update()
    {
        if (!SenserPly)
        {
            ani.SetInteger("State", 0);
            setF = true;
            Attss = false;
            TT = 0;
            return;
        }
        else if (setF)
        {
            WWW();
            return;

        }
        if (TT > 0)
        {
            Attss = true;
            TT -= Time.deltaTime;
            if (TT > MoveStopTime) transform.GetChild(0).position += new Vector3(-transform.GetChild(0).position.x + ply.position.x, 0).normalized * Time.deltaTime * MoveSpeed;
        }
        else if (Attss)
        {
            Attss = false;
            ani.SetInteger("State", 2);
        }
        
        base.Update();
    }
    public void WWW()
    {
        Attss = false;
        setF = false;
        ani.SetInteger("State", 1);
        TT = MoveTime + MoveStopTime;
    }
    public void SSAni()
    {
        if (ani.GetInteger("State") != 0)
        {
            WWW();
        }
        ani.SetInteger("State", 1);
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
    
}
