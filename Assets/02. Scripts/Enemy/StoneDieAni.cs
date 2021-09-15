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
