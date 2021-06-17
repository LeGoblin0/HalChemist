using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrTr : MonoBehaviour
{
    [HideInInspector]
    public int ObjCode;
    public Life.State state;
    public int NowState = 0;
    Animator ani;
    public bool All = false;

    public int BGSound = 0;
    private void Start()
    {
        ani = GetComponent<Animator>();
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && (collision.GetComponent<Att>().AttState == state || All))
        {
            if (GameSystem.instance.SaveMeNow(ObjCode) == 0)
            {
                GameSystem.instance.SaveSET(ObjCode);
                NowState = 1;
                if (collision.GetComponent<StoneDieAni>() != null) collision.transform.position += new Vector3(0, 0, 10000);
            }
            else if (GameSystem.instance.SaveMeNow(ObjCode) == 1) GameSystem.instance.ChangeSave(ObjCode);


        }
    }
    private void Update()
    {
        if (GameSystem.instance.SaveNow() == ObjCode)
        {
            ani.SetInteger("State", 2);
          //  Debug.Log(0);
        }
        else ani.SetInteger("State", NowState);
        if (NowState > 0) state = Life.State.일반공격;

    }
    public Transform MovePos;
}
