using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStop : MonoBehaviour
{
    public int aniState = 0;
    [Range(-1, 1)]
    public int PlayerLook = 1;
    public bool MoveTrue = false;
    public Vector2 Moveing;
    Player ply;
    Transform plyTr;
    void Start()
    {
        plyTr = GameSystem.instance.Ply;
        ply = plyTr.GetComponent<Player>();

     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log(0);
            if (aniState == -1)
            {
                ply.ani.SetTrigger("Test");
                ply.ani.SetInteger("State", 0);
            }
            else
            {
              //  Debug.Log(aniState);
                ply.ani.SetInteger("State", aniState);
            }
            plyTr.GetChild(0).localScale = new Vector3(PlayerLook, 1, 1);
            if (MoveTrue)
            {
                plyTr.GetComponent<Rigidbody2D>().velocity = Moveing;
            }
            transform.parent.GetComponent<StoryCont>().EndS();
        }
    }
}
