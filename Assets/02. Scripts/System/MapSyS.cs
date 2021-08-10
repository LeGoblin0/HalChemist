using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSyS : MonoBehaviour
{
    //[HideInInspector]
    public int ObjCode = -1;
    public GameObject[] MapObj;
    public MapSyS[] MapTTT;
    public float MapTTTTime = 0;
    public Life.State state;

    public bool SaveOn = false;

    [Header("충돌해도 실행")]
    [Tooltip("일반공격 제외 모두")]
    public bool All = false;
    [Tooltip("플레이어 충돌")]
    public bool AllSS = false;
    [Header("반복 실행")]
    public bool Loop = false;

    public bool YSet = true;
    
    protected SpriteRenderer ImgRander;
    private void Start()
    {
        if (AllSS) gameObject.layer = 12;
        if (MapObj == null)
        {
            MapObj = new GameObject[1];
            MapObj[0] = gameObject;
        }

        if (transform.GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetComponent<SpriteRenderer>();
        else if (transform.GetChild(0).GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetChild(0).GetComponent<SpriteRenderer>();
        else if (transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>() != null) ImgRander = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        if (ImgRander != null) ImgRander.material = GameSystem.instance.EnemyMaterial;

        if (YSet) transform.position = new Vector3(transform.position.x, transform.position.y, 6);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        if (End && !Loop) return;
        //Debug.Log(collision.tag);
        //Debug.Log(collision.GetComponent<Att>().AttState);
        if (AllSS && collision.tag == "Player")
        {
            if (ObjCode >= 0) { aniNum++; GameSystem.instance.MapSSS(ObjCode, aniNum); }
            SpeO();
        }
        else if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && (collision.GetComponent<Att>().AttState == state || (All && (collision.GetComponent<Att>().AttState != Life.State.일반공격))))
        {

            if (ObjCode >= 0) { aniNum++; GameSystem.instance.MapSSS(ObjCode, aniNum); }
            SpeO();
        }
    }
    bool End = false;

    bool Onset = false;
    public virtual void MapTrue()
    {
        if (MapObj == null)
        {
            MapObj = new GameObject[1];
            MapObj[0] = gameObject;
        }
        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        for (int i = 0; MapObj != null && i < MapObj.Length; i++)
        {
            if (MapObj[i] != null && MapObj[i].GetComponent<Animator>() != null)
            {
                MapObj[i].GetComponent<Animator>().SetInteger("State", aniNum);
                MapObj[i].GetComponent<Animator>().SetTrigger("On");

                Onset = true;
                //Debug.Log(name);
            }
        }
        if (!OneTimeSet) Invoke("AniCont", MapTTTTime);
    }
    bool OneTimeSet = false;
    public void AniCont()
    {
        OneTimeSet = true;
        for (int i = 0; MapTTT != null && i < MapTTT.Length; i++)
        {
            if (MapTTT[i] != null)
            {
                MapTTT[i].SpSp();
                //Debug.Log(MapTTT[i]);
            }
        }
    }
    public void SpSp()
    {
        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        if (ObjCode >= 0) { aniNum++; if (aniNum > 100) aniNum = 100; GameSystem.instance.MapSSS(ObjCode, aniNum); }
        SpeO();
    }
    public virtual void SpeO()
    {
        MapTrue();
        
    }
    private void OnEnable()
    {
        SetAni();
    }
    public void SetAni()
    {

        if (Onset)
        {
            int aniNum = GameSystem.instance.MapSSS(ObjCode);
            if (MapObj == null)
            {
                MapObj = new GameObject[1];
                MapObj[0] = gameObject;
            }
            for (int i = 0; MapObj != null && i < MapObj.Length; i++)
            {
                if (MapObj[i] != null && MapObj[i].GetComponent<Animator>() != null)
                {
                    MapObj[i].GetComponent<Animator>().SetInteger("State", aniNum);
                    Onset = true;
                    //Debug.Log(ObjCode+"   "+aniNum);
                }
            }
        }
    }
    public void EndAni()
    {
        GameSystem.instance.MapSSS(ObjCode, 100);
        End = true; SaveOn = true;
    }
}
