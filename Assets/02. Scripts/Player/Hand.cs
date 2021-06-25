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

    // Update is called once per frame
    void Update()
    {
        
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
