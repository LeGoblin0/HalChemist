using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StCh : MonoBehaviour
{
    public Player ply;
    void Start()
    {
        
    }
    public float Speed = 5;
    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.parent.GetChild(0).GetChild(ply.NowChoose).position - transform.position) * Time.deltaTime * Speed;
    }
}
