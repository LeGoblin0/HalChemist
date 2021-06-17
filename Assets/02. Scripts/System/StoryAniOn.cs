using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAniOn : MonoBehaviour
{
    public GameObject[] ObjAni;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < ObjAni.Length; i++)
            {
                ObjAni[i].SetActive(true);
            }
        }
    }
}
