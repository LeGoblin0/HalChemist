using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttSub : MonoBehaviour
{
    public Player ply;
    public void EndAtt(int a)
    {
        //Debug.Log(0);
        ply.EndAtt(a);
    }
    public void EndHit()
    {
        ply.EndHit();
    }
    public void EndDie()
    {
        ply.EndDie();
    }
    public void attoff()
    {
        ply.AttOff();
    }
    public void EndDesh()
    {
        ply.EndDesh();
    }
    public bool First = true;
    public void ReSpawn()
    {
        if (First)
        {
            ply.DontMove = false;
            First = false;
            GetComponent<Animator>().SetInteger("State", 0);
        }
    }
    public void Att2P()
    {
        ply.ATT2();
    }
    public void End2()
    {
        ply.EndAtt2();
    }
}
