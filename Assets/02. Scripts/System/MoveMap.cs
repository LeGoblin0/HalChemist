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
        gameObject.tag = "EndMap";
        Ply = GameSystem.instance.Ply;
        cam = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineConfiner>();
        //gameObject.layer = 23;
        if (GoMap != null) MovePos = GoMap.transform;
        if (MovePos == null) 
        {
            MovePos = transform;
        }
        if (GetComponent<SpriteRenderer>() != null) Destroy(GetComponent<SpriteRenderer>());
    }
    Transform Ply;
    //public Transform MovePos.GetChild(0);
    //public Transform MovePos.GetChild(1);
    Transform MovePos;
    public MoveMap GoMap;
    public void DrowLine(int Time = 10)
    {
        Vector3 aa = (Vector2)transform.position + GetComponent<Collider2D>().offset;
        if (MovePos == transform) Debug.DrawLine(aa, aa + Vector3.one * 5, Color.red, Time);
        Debug.DrawLine(aa, MovePos.position + (Vector3)MovePos.GetComponent<Collider2D>().offset, Color.yellow, Time);

    }

    public Vector3 ShootPly;
    Cinemachine.CinemachineConfiner cam;
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
        //Ply.GetComponent<Player>().Hand.position = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, 0);
        //Ply.GetComponent<Player>().MapMove = true;
        Ply.GetComponent<Rigidbody2D>().velocity = ShootPly;
        MovePos.parent.parent.gameObject.GetComponent<MapManager>().MakeEEE();
        if (transform.parent.parent.gameObject.GetComponent<MapManager>().EEE != null)
            Destroy(transform.parent.parent.gameObject.GetComponent<MapManager>().EEE.gameObject);
        //Ply.GetComponent<Player>().MoveMap = true;
        Ply.GetComponent<Player>().DontMove = false;

        MovePos.parent.parent.gameObject.SetActive(true);
        transform.parent.parent.gameObject.SetActive(false);

        cam.m_BoundingShape2D= MovePos.parent.parent.GetChild(0).GetComponent<PolygonCollider2D>();
    }
}
