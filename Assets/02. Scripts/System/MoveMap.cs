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
        cam = Camera.main.transform;
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
    [ContextMenu("맵 연결 확인(단일)")]
    public void DrowLine()
    {
        if (MovePos == null)
        {
            if (GoMap != null) MovePos = GoMap.transform;
            else MovePos = transform;
        }
        Vector3 aa = (Vector2)transform.position + GetComponent<Collider2D>().offset;
        if (MovePos == transform) Debug.DrawLine(aa, aa + Vector3.one * 5, Color.red, .1f);
        Debug.DrawLine(aa, MovePos.position , Color.yellow, .1f);
        Debug.DrawLine(MovePos.position, MovePos.GetChild(0).position , Color.white, .1f);

    }

    public Vector3 ShootPly;
    Transform cam;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSystem.instance.CanversUI.GetChild(2).GetComponent<Animator>().SetTrigger("On");
            Ply.GetComponent<Player>().OnStory = true;
            Invoke("gogoMap", 0.2f);
            Invoke("MovePly", 2f);
            if (BGChange >= 0)
            {
                GameSystem.instance.Sond(BGChange);
            }
        }
    }
    void MovePly()
    {
        Ply.GetComponent<Player>().OnStory = false;
    }
    void gogoMap()
    {
        Ply.position = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, Ply.position.z);
        Ply.GetComponent<Player>().trapsavepoint = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, Ply.position.z);
        //Ply.GetComponent<Player>().Hand.position = new Vector3(MovePos.GetChild(0).position.x, MovePos.GetChild(0).position.y, 0);
        //Ply.GetComponent<Player>().MapMove = true;
        Ply.GetComponent<Rigidbody2D>().velocity = ShootPly;
        MovePos.parent.parent.gameObject.GetComponent<MapManager>().MakeEEE();
        if (transform.parent.parent.gameObject.GetComponent<MapManager>().EEE != null)
            Destroy(transform.parent.parent.gameObject.GetComponent<MapManager>().EEE.gameObject);
        //Ply.GetComponent<Player>().MoveMap = true;

        MovePos.parent.parent.gameObject.SetActive(true);
        transform.parent.parent.gameObject.SetActive(false);

        cam.position = new Vector3(Ply.position.x, Ply.position.y, cam.position.z);
    }
}
