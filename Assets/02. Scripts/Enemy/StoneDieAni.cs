using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDieAni : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("날라가는 속도")]
    public float ThrowSpeed=6;
    [Header("회전 속도")]
    public float RollSpeed=720;

    [Header("스핀 제거 시간")]
    public float SpDesTime = 5;
    [Header("다용도 변수")]
    public float StoneSpecialNum;
    [Header("과열 게이지 변수")]
    public int AngNum = 5;
    [Header("상점가격")]
    public int MoneyBuy = 10;
    [Header("중력크기")]
    public float Grascale = 0;
    [Header("벽충돌 사망")]
    public bool GroundDie = true;
    [Header("체력하트 확률")]
    public float HPDropPer = 5;
    void Start()
    {

        //StonImg = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    public Transform DieObj;
    // Update is called once per frame
    void Update()
    {
        
    }
    public int Code;
    public Sprite StonImg;

    public bool DieSet = false;
    private void OnDestroy()
    {
        if (DieSet)
        {
            Transform aa = Instantiate(DieObj);
            aa.position = transform.position;
            if (Random.Range(0, 100f) < HPDropPer)
            {
                if (transform.parent != null)
                {
                    Transform hphp = Instantiate(GameSystem.instance.ItemPre[1]);
                    hphp.position = transform.position;
                    hphp.parent = transform.parent;
                    Destroy(hphp, 180);
                    Debug.Log(0);
                }
            }
            Destroy(aa.gameObject, .5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DieSet && collision.tag == "EndMap")
        {
            transform.position = new Vector3(-1000, -1000);
            Destroy(gameObject);
        }
        if (collision.tag == "TrapGround")
        {
            DieSet = true;
            Destroy(gameObject);
        }

    }
}
