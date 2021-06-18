using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDDDD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    SpriteRenderer spr;
    public float Downd = 3f;
    public bool Black = false;
    // Update is called once per frame
    void Update()
    {
       if(!Black)  spr.color = new Color(1, 1, 1, spr.color.a - Time.deltaTime * Downd);
       else spr.color = new Color(0, 0, 0, spr.color.a - Time.deltaTime * Downd);
        if (spr.color.a <= 0) Destroy(gameObject);
    }
}
