using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    // Start is called before the first frame update
    public int BGChange = -1;
    void Start()
    {
        gameObject.layer = 12;
        gameObject.tag = "Ground";
        Ply = GameSystem.instance.Ply;
        cam = Camera.main.transform;
        //gameObject.layer = 23;
        if (MovePos == null) 
        {
            MovePos = transform;
        }
        Vector3 aa = (Vector2)transform.position + GetComponent<Collider2D>().offset;
        if (MovePos == transform) Debug.DrawLine(aa, aa + Vector3.one * 5, Color.red, 10000);
        Debug.DrawLine(aa, MovePos.position + (Vector3)MovePos.GetComponent<Collider2D>().offset, Color.yellow, 10000);
    }
    Transform Ply;
    Transform cam;
    //public Transform MovePos.GetChild(0);
    //public Transform MovePos.GetChild(1);
    public Transform MovePos;


    public Vector3 ShootPly;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSystem.instance.CanversUI.GetChild(1).GetComponent<Animator>().SetTrigger("On");
            Ply.GetComponent<Player>().DontMove = true;
            Ply.GetComponent<Player>().GodMove = true;
            Invoke("gogoMap", 0.2f);
            if (BGChange >= 0)
            {
                GameSystem.instance.Sond(BGChange);
            }
        }
    }
    void gogoMap()
    {
        Ply.position = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, 0);
        Ply.GetComponent<Player>().Hand.position = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, 0);
        Ply.GetComponent<Player>().MapMove = true;
        Ply.GetComponent<Rigidbody2D>().velocity = ShootPly;
        cam.position = new Vector3(MovePos.GetChild(1).position.x, MovePos.GetChild(1).position.y, -10);
        cam.GetComponent<CamMove>().XLock = MovePos.parent.parent.gameObject.GetComponent<MapManager>().XLock;
        cam.GetComponent<CamMove>().YLock = MovePos.parent.parent.gameObject.GetComponent<MapManager>().YLock;
        MovePos.parent.parent.gameObject.GetComponent<MapManager>().MakeEEE();
        if (transform.parent.parent.gameObject.GetComponent<MapManager>().EEE != null)
            Destroy(transform.parent.parent.gameObject.GetComponent<MapManager>().EEE.gameObject);
        Ply.GetComponent<Player>().MoveMap = true;
        Ply.GetComponent<Player>().DontMove = false;
        Ply.GetComponent<Player>().GodMove = false;
    }
}
