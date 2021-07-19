using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSyS : MonoBehaviour
{
    [HideInInspector]
    public int ObjCode = -1;
    public GameObject[] MapObj;
    public Life.State state;

    public bool SaveOn = false;

    [Header("충돌해도 실행")]
    public bool All = false;
    public bool AllSS = false;
    public bool Loop = false;

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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        if (End && !Loop) return;
        if(AllSS && collision.tag == "Player")
        {
            if (ObjCode >= 0) { aniNum++; GameSystem.instance.MapSSS(ObjCode, aniNum); }
            SpeO();
        }
        else if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && (collision.GetComponent<Att>().AttState == state || (All && collision.GetComponent<Att>().AttState != Life.State.일반공격))) 
        {
            if (ObjCode >= 0) { aniNum++; GameSystem.instance.MapSSS(ObjCode, aniNum); }
            SpeO();
        }
    }
    bool End = false;

    bool Onset = false;
    public virtual void MapTrue(int a = 1)
    {
        if (MapObj == null)
        {
            MapObj = new GameObject[1];
            MapObj[0] = gameObject;
        }
        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        for (int i = 0; MapObj != null &&i< MapObj.Length; i++) 
        {
            if (MapObj[i] != null && MapObj[i].GetComponent<Animator>() != null) 
            {
                MapObj[i].GetComponent<Animator>().SetInteger("State", aniNum);
                Onset = true;
                //Debug.Log(0);
            }
        }
    }
    public virtual void SpeO()
    {
        MapTrue();
        
    }
    private void OnEnable()
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
                    //Debug.Log(0);
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
