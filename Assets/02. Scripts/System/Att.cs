using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Att : MonoBehaviour
{
    [Header("공격력")]
    public int AttDamage = 1;
    [Header("몇초뒤 삭제  0이면 삭제 안됨")]
    public float DesTime = 0;
    float nowTime = 0;
    void Start()
    {
        tag = "Att";
        nowTime = DesTime;
    }
    public GameObject attObj;
    public void Attnow()
    {
        attObj.SetActive(true);
    }
    public void Attnow2()
    {
        attObj.SetActive(false);
    }
    // Update is called once per frame
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
    }
    public Life.State AttState = Life.State.일반;
    [Header("지형,적 충돌시 제거")]
    public bool GroundDes = true;
    [Header("활성화 안하면 충돌 안함(초기화 하면 꼭 체크할것)")]
    public bool Set = false;

    public bool DieAni = false;
    public void DDD()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DesTime == 0 && Set && GroundDes && (collision.tag == "Ground" || collision.tag == "Att")) 
        {
            if (DieAni)
            {
                if (GetComponent<Animator>() != null) GetComponent<Animator>().SetTrigger("Die");
                if (GetComponent<Collider2D>() != null) Destroy(GetComponent<Collider2D>());
                if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else Destroy(gameObject);
        }
        if (Set && (collision.tag == "Att") && GetComponent<Enemy01>() != null)
        {
            GetComponent<Enemy01>().DieHit();
        }
    }
}
