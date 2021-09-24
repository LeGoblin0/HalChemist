using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Life
{
    public Animator ani;
    protected SpriteRenderer ImgRander;
    public bool YSet = true;
    public float YNum = 4;

    protected Material First;
    protected virtual void Start()//protected override void Start() 상속받아서 이렇게 작성
    {
        gameObject.layer = 11;
        gameObject.tag = "Att";//없어도 되는내용 적 레이어하고 테그를 Enemy로 바꿀것
        if (GetComponent<Animator>() != null) ani = GetComponent<Animator>();
        ani.SetFloat("HitSpeed", HitAniSpeed);

        if (transform.GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetComponent<SpriteRenderer>();
        else if (transform.GetChild(0).GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetChild(0).GetComponent<SpriteRenderer>();
        else if (transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        if (ImgRander != null)
        {
            First = ImgRander.material;
            ImgRander.material = GameSystem.instance.EnemyMaterial;
        }


        if (YSet)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, YNum);
        }
    }
    protected bool Die = false;//죽으면 참이됨 죽었을떄 1번만 발생하도록 해주는 변수

    [Header("센서에 플레이어 감지")]
    public bool SenserPly = false;
    public virtual void GroundSen(bool Out=false)
    {

    }

    [Header("사망하면 떨구는 아이템")]
    public bool DropItem = true;
    public Transform[] DieItem;
    public int MoneyDie = 0;
    [Header("사망 애니메이션 시간")]
    public float DieAniTime;
    public bool NoDie = false;
    protected virtual void Update()//protected override void Update() 상속받아서 이렇게 작성
    {
        if (Die) return;
        if (HitWhite)
        {
            if (HHIITime > 0)
            {
                ImgRander.material.SetFloat("_HitCol", 0.55f);
                HHIITime -= Time.deltaTime;
            }
            else
            {
                ImgRander.material.SetFloat("_HitCol", 0);
            }
        }
        Dieset();//체력다되면 사망
    }
    protected virtual void Dieset()
    {
        if (Hp <= 0)
        {
            if (!Die)
            {
                gameObject.layer = 17;
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.layer==11) transform.GetChild(i).gameObject.layer = 17;
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
                for(int i = 0; i < MoneyDie; i++)
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
                if (!NoDie) Destroy(gameObject, DieAniTime);
                //사망 애니메이션 실행
                //
            }
            return;
        }
    }
    private void OnDestroy()
    {
    }
    public virtual void DieHit()
    {
        //죽은상태에서 벽에 충돌   부메랑처럼 돌아오는 적은 이 함수 상속받아서 충돌시 플레이어에게 가는 함수 작성함
    }

    [Header("히트관련")]
    public float HHIITime = 0;
    public bool HitAni = true;
    public float HitAniSpeed=10f;
    public float HitAniknockBack=1;
    public bool HitWhite = true;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);

        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set)
        {
            Hp -= collision.GetComponent<Att>().AttDamage;
            if (Hp != 0)
            {
                HHIITime = .2f;
                if(HitAni) ani.SetTrigger("Hit");
            }

        }
        if (collision.tag == "TrapGround")
        {
            DropItem = false;
            Hp = 0;
        }
    }
}
