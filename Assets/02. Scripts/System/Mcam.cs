using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mcam : MonoBehaviour
{
    BoxCollider2D col;
    const int MAPPIXEL = 48;
    public Transform Target;
    Rigidbody2D rig;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    public float CamSpeed = 3;
    void Start()
    {
        
       
    }
    private void FixedUpdate()
    {
        Vector2 dir = Target.position - transform.position;
        rig.velocity = dir * CamSpeed;

    }
    void Update()
    {
        Vector2 LD = Camera.main.ViewportToScreenPoint(new Vector3(0.0f, 0f, 0f));
        Vector2 RU = Camera.main.ViewportToScreenPoint(new Vector3(1f, 1f, 0f));
        //Debug.Log(Camera.main.ViewportToScreenPoint(new Vector3(1f, 1f, 0f)));
        col.size = new Vector2(RU.x - LD.x, RU.y - LD.y) / MAPPIXEL / 2;
    }
}
