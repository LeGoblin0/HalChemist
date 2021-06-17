using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCont : MonoBehaviour
{
    public int aniState = 0;
    [Range(-1,1)]
    public int PlayerLook = 1;
    
    public bool MoveRight = false;
    public bool Moveleft = false;

    [Header("특수한 애니메이션 실행")]
    public bool StoryAni = false;
    public string ParametersName = "";

    [Header("플레이어 다음 애니메이션으로 넘어감 0보다 작으면 시간 무제한")]
    public float EndStoryTime = -1;
    public GameObject Hando;
    Player ply;
    
    Transform plyTr;
    void Start()
    {
        if (plyTr == null) plyTr = GameSystem.instance.Ply;
        if (ply == null) ply = plyTr.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool MovePlayer = true;
    public void EndS()
    {
        //Debug.Log(1);
        if (plyTr == null) plyTr = GameSystem.instance.Ply;
        if (ply == null) ply = plyTr.GetComponent<Player>();
        ply.OnStory_left = false;
        ply.OnStory_rigtht = false;
        transform.parent.GetComponent<StoryOnOFf>().EndStory();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (plyTr == null) plyTr = GameSystem.instance.Ply;
            if (ply == null) ply = plyTr.GetComponent<Player>();

            if (MovePlayer)
            {
                plyTr.position = transform.position;
                //Debug.Log(0);
            }
            plyTr.GetChild(0).localScale = new Vector3(PlayerLook, 1, 1);
            if (StoryAni)
            {
                ply.ani.SetTrigger(ParametersName);
                ply.ani.SetInteger("State", aniState);
            }
            else
            {
                ply.ani.SetInteger("State", aniState);
            }
            ply.OnStory_rigtht = MoveRight;

            ply.OnStory_left = Moveleft;

            if (EndStoryTime >= 0)
            {
                Invoke("EndS", EndStoryTime);
            }
        }
    }
    public void HandoOff()
    {
        Hando.gameObject.SetActive(false);
    }
   
}
