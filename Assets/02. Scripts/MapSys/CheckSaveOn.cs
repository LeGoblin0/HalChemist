using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSaveOn : MonoBehaviour
{
    public bool MonsterS = false;
    public int MonsterSNum = 0;

    Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        startt = true;
    }
    bool startt = false;
    public float DelTime;
    // Update is called once per frame
    void Update()
    {
        if (startt &&MonsterS && GameSystem.instance.GiveMonster(MonsterSNum) == 1) 
        {
            startt = false;
            Invoke("DD", DelTime);
        }
    }

    void DD()
    {
        col.enabled = true;
        Destroy(this);
    }
}
