using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManlimeSenser : MonoBehaviour
{
    public manlime enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            enemy.DownD();
        }
    }
}
