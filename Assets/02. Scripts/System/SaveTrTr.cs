using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrTr : MonoBehaviour
{
    [HideInInspector]
    public int ObjCode;
    Animator ani;
    Player ply;
    public int BGSound = 0;
    private void Start()
    {
        ani = GetComponent<Animator>();
        AniSet();
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        if (SaveUI == null) SaveUI = transform.GetChild(1).gameObject;

    }
    public void openUI()
    {
        if (SaveUI == null) SaveUI = transform.GetChild(1).gameObject;
        SaveUI.SetActive(true);
        SaveUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        SaveUI.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        car = 0;
        ply.OnStory = true;
        Invoke("OOPP", 0.1f);
    }
    int car = 0;
    [HideInInspector]
    public bool OpenNow = false;
    void OOPP()
    {
        OpenNow = true;
    }
    GameObject SaveUI;
    private void Update()
    {
        AniSet();
        if (OpenNow ) 
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                car++;
                SaveUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                SaveUI.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);

                SaveUI.transform.GetChild(0).GetChild(car % 2).GetChild(0).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (car == 0)
                {
                    SaveOn();
                    SaveUI.SetActive(false);
                    ply.OnStory = false; OpenNow = false;
                }
                else if (car == 1)
                {
                    SaveUI.SetActive(false);
                    GameSystem.instance.StoneButUI.SetActive(true);
                    OpenNow = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                SaveUI.SetActive(false);
                ply.OnStory = false; OpenNow = false;

            }
        }
    }
    private void OnDisable()
    {
        OpenNow = false;
    }
    void AniSet()
    {
        if (GameSystem.instance.SaveNow() == ObjCode)
        {
            ani.SetInteger("State", 1);
            //Debug.Log(0);
        }
        else ani.SetInteger("State", 0);
    }


    public Transform MovePos;

    public bool SaveOn()
    {
        ply.LookMoney();
        ply.PlySave();
        return GameSystem.instance.ChangeSave(ObjCode);
    }
}
