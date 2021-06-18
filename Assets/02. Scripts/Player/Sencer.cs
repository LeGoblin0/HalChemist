using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Player ply;

    public bool right;
    public bool left;
    public bool down;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground"|| collision.tag == "Enemy")
        {
            if (right) ply.right = true;
            if (left) ply.left = true;

            if (down)
            {
                ply.down = true;
                ply.att = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground"|| collision.tag == "Enemy")
        {
            if (right) ply.right = false;
            if (left) ply.left = false;
            if (down) ply.down = false;
        }
    }
}
