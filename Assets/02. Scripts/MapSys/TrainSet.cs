using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSet : MonoBehaviour
{
    public Vector2 GoTT;
    public bool Stop = false;
    void Start()
    {
        gameObject.layer = 12;
        gameObject.tag = "TrainSet";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
