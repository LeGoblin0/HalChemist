using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    public bool Hold_X;
    public bool Hold_Y;
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 21;
        //gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
