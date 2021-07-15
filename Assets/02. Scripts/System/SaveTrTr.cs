using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrTr : MonoBehaviour
{
    [HideInInspector]
    public int ObjCode;
    Animator ani;

    public int BGSound = 0;
    private void Start()
    {
        ani = GetComponent<Animator>();
        AniSet(); 

    }
    private void Update()
    {
        AniSet();
    }
    void AniSet()
    {
        if (GameSystem.instance.SaveNow() == ObjCode)
        {
            ani.SetInteger("State", 1);
            Debug.Log(0);
        }
        else ani.SetInteger("State", 0);
    }


    public Transform MovePos;

    public void SaveOn()
    {
        GameSystem.instance.ChangeSave(ObjCode);
    }
}
