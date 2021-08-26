using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSet : MonoBehaviour
{
    public Vector2[] GoTT;
    public bool Stop = false;
    public bool YSet = true;

    private void Awake()
    {
        if (YSet)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 6);
        }
    }
    void Start()
    {
        gameObject.layer = 27;
        gameObject.tag = "TrainSet";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
