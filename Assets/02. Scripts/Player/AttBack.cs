using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttBack : MonoBehaviour
{
    public Player ply;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == 11)
        {
            ply.AttBa();
        }
    }
}
