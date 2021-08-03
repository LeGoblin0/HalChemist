using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObj : MonoBehaviour
{
    public GameObject[] Obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            for (int i = 0; i < Obj.Length; i++)
            {
                Obj[i].SetActive(true);
            }
            Destroy(gameObject);
        }
        
    }
}
