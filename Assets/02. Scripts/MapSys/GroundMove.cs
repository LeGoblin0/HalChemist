using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 move;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // collision.GetComponent<Rigidbody2D>().velocity = move;
    }
}
