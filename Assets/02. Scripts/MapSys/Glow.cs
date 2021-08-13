using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 5.9f);
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rig = collision.GetComponent<Rigidbody2D>();
            if (rig.velocity.x > 0)
            {
                ani.SetTrigger("On1");
            }
            if (rig.velocity.x < 0)
            {
                ani.SetTrigger("On2");
            }
        }
    }
}
