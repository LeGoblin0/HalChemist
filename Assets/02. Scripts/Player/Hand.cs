using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Player ply;
    void Start()
    {
        ply = transform.parent.parent.GetComponent<Player>();
    }

    public Vector3 offset;
    public float Speed = 3;
    void Update()
    {
        transform.position += (new Vector3(offset.x * ply.transform.GetChild(0).localScale.x, offset.y, offset.z) + ply.transform.position - transform.position) * Speed * Time.deltaTime;
    }
    public void MakeStone()
    {
        ply.MakeStone();
    }
    public void ThrowStone()
    {
        ply.TStone();
    }
}
