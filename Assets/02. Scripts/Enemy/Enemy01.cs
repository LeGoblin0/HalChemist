using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Life
{
    public Animator ani;
    protected virtual void Start()//protected override void Start() 상속받아서 이렇게 작성
    {
        gameObject.layer = 11;
        gameObject.tag = "Att";//없어도 되는내용 적 레이어하고 테그를 Enemy로 바꿀것
        if (GetComponent<Animator>() != null) ani = GetComponent<Animator>();
        
    }
    protected bool Die = false;//죽으면 참이됨 죽었을떄 1번만 발생하도록 해주는 변수

    [Header("센서에 플레이어 감지")]
    public bool SenserPly = false;
    public virtual void GroundSen(bool Right=false)
    {

    }

    [Header("사망하면 떨구는 아이템")]
    public Transform[] DieItem;
    [Header("사망 애니메이션 시간")]
    public float DieAniTime;
    public bool NoDie = false;
    protected virtual void Update()//protected override void Update() 상속받아서 이렇게 작성
    {
        if (Die) return;
        if (HHIITime > 0) 
        {
            if (transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>() != null) transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            HHIITime -= Time.deltaTime;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        Dieset();//체력다되면 사망
    }
    protected void Dieset()
    {
        if (Hp <= 0)
        { 
            if (!Die)
            {
                Die = true;
                if (DieItem.Length > 0)
                {
                    for (int i = 0; i < DieItem.Length; i++)
                    {
                        Transform aa = Instantiate(DieItem[i]);
                        aa.position = transform.position + new Vector3(0, 0, 1f);
                        aa.parent = transform.parent;
                    }
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
    float HHIITime = 0;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);

        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set)
        {
            Hp -= collision.GetComponent<Att>().AttDamage;
            if (Hp != 0) HHIITime = .2f;
        }
    }
}
