using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCounter : MonoBehaviour
{
    public Boss_yangBoss boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set) 
        {
            boss.StunOn();
        }
    }
}
