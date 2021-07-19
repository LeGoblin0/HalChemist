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
    private void Start()
    {
        if (AllSS) gameObject.layer = 12;
        if (MapObj == null)
        {
            MapObj = new GameObject[1];
            MapObj[0] = gameObject;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (End && !Loop) return;
        if(AllSS && collision.tag == "Player")
        {
            if (ObjCode >= 0) GameSystem.instance.MapSSS(ObjCode);
            SpeO();
        }
        else if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && (collision.GetComponent<Att>().AttState == state || (All && collision.GetComponent<Att>().AttState != Life.State.일반공격))) 
        {
            if (ObjCode >= 0) GameSystem.instance.MapSSS(ObjCode);
            SpeO();
        }
    }
    bool End = false;

    bool FF = true;
    bool Onset = false;
    public virtual void MapTrue()
    {
        if (MapObj == null)
        {
            MapObj = new GameObject[1];
            MapObj[0] = gameObject;
        }
        for (int i = 0; MapObj != null &&i< MapObj.Length; i++) 
        {
            if (MapObj[i] != null && MapObj[i].GetComponent<Animator>() != null) 
            {
                if (FF) MapObj[i].GetComponent<Animator>().SetInteger("State", 2);
                else MapObj[i].GetComponent<Animator>().SetInteger("State", 1);
                Onset = true;
                FF = true;
                //Debug.Log(0);
            }
        }
        End = true;
    }
    public virtual void SpeO()
    {
        FF = false;
        MapTrue();
        
    }
    private void OnEnable()
    {
        if (Onset)
        {
            if (MapObj == null)
            {
                MapObj = new GameObject[1];
                MapObj[0] = gameObject;
            }
            for (int i = 0; MapObj != null && i < MapObj.Length; i++)
            {
                if (MapObj[i] != null && MapObj[i].GetComponent<Animator>() != null)
                {
                    if (FF) MapObj[i].GetComponent<Animator>().SetInteger("State", 2);
                    else MapObj[i].GetComponent<Animator>().SetInteger("State", 1);
                    Onset = true;
                    FF = true;
                    //Debug.Log(0);
                }
            }
        }
    }
}
