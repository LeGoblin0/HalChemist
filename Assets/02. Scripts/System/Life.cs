using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{

    [Header("체력")]
    public int MaxHP = 1;
    public int Hp = 1;

    protected int LastAtt = -1;
    protected virtual void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>()!=null && collision.GetComponent<Att>().Set)
        {
            if (collision.GetComponent<Att>().AttCode == -1 || collision.GetComponent<Att>().AttCode != LastAtt)
            {
                LastAtt = collision.GetComponent<Att>().AttCode;
                Hp -= collision.GetComponent<Att>().AttDamage;
            }
        }
    }
    public enum State { 없음=-1,일반, 경직, 기절, 감전, 느려짐, 속박 ,일반공격, 탱탱}
    [Header("상태")]
    public State state = State.일반;
}
