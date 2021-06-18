using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MapSyS
{
    public int SSS = 0;
    public override void SpeO()
    {
        int a = 0;
        base.SpeO();
        if (SSS == a++) 
        {
            if (GameSystem.instance.Ply.GetComponent<Player>().Hp < 3)
            {
                GameSystem.instance.Ply.GetComponent<Player>().Hp++;
                GameSystem.instance.Ply.GetComponent<Player>().HpSetUI();
            }
        }
    }
}
