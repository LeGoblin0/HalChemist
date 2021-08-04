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

    Transform DownObj;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground"|| collision.tag == "Enemy")
        {
            if (right) ply.right = true;
            if (left) ply.left = true;

            if (down)
            {
                ply.down = true;
                ply.airAtt = true;
                ply.Jump01 = true;

                DownObj = collision.transform;
                if (collision.transform.parent != null && collision.transform.parent.GetComponent<Train>() != null)
                {
                    ply.TrainNow = collision.transform.parent.GetComponent<Rigidbody2D>();
                }

            }
        }
        if (collision.tag == "Att" && down && collision.GetComponent<StoneDieAni>() != null && down && collision.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezePosition) 
        {
            ply.PutStonePly(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground"|| collision.tag == "Enemy")
        {
            if (right) ply.right = false;
            if (left) ply.left = false;
            if (down)
            {
                if (DownObj == collision.transform)
                {
                    ply.down = false;

                    if (collision.transform.parent != null && collision.transform.parent.GetComponent<Train>() != null)
                    {
                        ply.TrainNow = null;
                    }
                }
            }
        }
    }
}
