using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSen : MonoBehaviour
{
    Player ply;
    void Start()
    {
        ply = transform.parent.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Stone")
        {
            ply.Pick_Stone(collision);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
