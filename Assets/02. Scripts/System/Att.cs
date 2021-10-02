using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Att : MonoBehaviour
{
    [Header("공격력")]
    public int AttDamage = 1;
    [Header("몇초뒤 삭제  0이면 삭제 안됨-시작타이머")]
    public float DesTime = 0;
    [Header("몇초뒤 삭제  0이면 삭제 안됨-충돌등 타이머")]
    public float DesDesTime = 0;
    [Header("몇초뒤 삭제  0이면 삭제 안됨-수동 타이머")]
    public float SelfDesTime = 0;
    [Header("관통")]
    public bool HitDesT = false;
    [Header("어텍코드")]
    public int AttCode = -1;
    public int HitNum = 1;
    [Tooltip("-1=없음 0=상 1=하 2=좌 3=우")]
    public int AttArrow = -1;
    float nowTime = 0;
    void Start()
    {
    }
    public GameObject attObj;
    public void Attnow()
    {
        attObj.SetActive(true);
    }
    public void SelfDie()
    {
        Invoke("DDD", SelfDesTime);
    }
    public void Attnow2()
    {
        attObj.SetActive(false);
    }
    // Update is called once per frame
    [HideInInspector]
    public bool set2 = true;
    void Update()
    {
        if (DesTime != 0)
        {
            nowTime -= Time.deltaTime;
            if (nowTime < 0) 
            {
                Destroy(gameObject);
            }
        }
        if (Set && set2) 
        {
            set2 = false;
            tag = "Att";
            nowTime = DesTime;
        }
    }
    public Life.State AttState = Life.State.일반;
    [Header("지형,적 충돌시 제거")]
    public bool GroundDes = true;
    [Header("활성화 안하면 충돌 안함(초기화 하면 꼭 체크할것)")]
    public bool Set = false;

    public bool DieAni = false;
    public bool DieCollDes = true;
    public void DDD()
    {
        Destroy(gameObject, DesDesTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy01 nowhit = collision.GetComponent<Enemy01>();
        if (nowhit == null && collision.transform.parent != null) nowhit = collision.transform.parent.GetComponent<Enemy01>();
        if (nowhit == null && collision.transform.parent != null && collision.transform.parent.parent != null) nowhit = collision.transform.parent.parent.GetComponent<Enemy01>();
        if (DesTime == 0 && Set && GroundDes && (collision.tag == "Ground" || collision.tag == "Att")) 
        {
            if (DieAni)
            {
                if (GetComponent<Animator>() != null) GetComponent<Animator>().SetTrigger("Die");
                if (GetComponent<Collider2D>() != null) Destroy(GetComponent<Collider2D>());
                if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else Destroy(gameObject, DesDesTime);
        }
        if (Set && (collision.tag == "Att") && nowhit != null)
        {
            nowhit.DieHit();
            HitNum--;
            if (HitNum <= 0 && HitDesT)
            {
                Destroy(gameObject, DesDesTime);
            }
        }
    }
}
