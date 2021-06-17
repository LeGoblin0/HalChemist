using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rig.velocity = moveNow;
    }
    Rigidbody2D rig;
    Vector2 moveNow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GroundMove>() != null)
        {
            moveNow = collision.GetComponent<GroundMove>().move;
        }
    }
}
