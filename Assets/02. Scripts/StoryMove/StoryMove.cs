using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMove : MonoBehaviour
{
    public bool Loop = false;
    Player ply;
    StoryStart storyStart;
    [Header("좌표에서 멈추기")]
    public bool PlyStop;
    [Header("오른쪽 이동")]
    public bool PlyRMove;
    [Header("왼쪽 이동")]
    public bool PlyLMove;
    [Header("점프")]
    public bool PlyJump;
    [Header("앉기")]
    public bool PlyD;
    [Header("앉기 취소")]
    public bool PlyDCencle;
    [Header("다음 실행")]
    public bool EndNext;
    [Header("다음실행 지연시간")]
    public float DelT = 0;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        storyStart = transform.GetComponent<StoryStart>();
        if (storyStart == null) storyStart = transform.parent.GetComponent<StoryStart>();
        if (storyStart == null) storyStart = transform.parent.parent.GetComponent<StoryStart>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (EndNext && !PlyStop) 
            {
                Invoke("EEE", DelT);
            }
            if (PlyRMove) ply.Story_RightMove = true;
            else if (!PlyStop) ply.Story_RightMove = false;
            if (PlyLMove) ply.Story_LeftMove = true;
            else if (!PlyStop) ply.Story_LeftMove = false;
            if (PlyJump) ply.Story_Jump = true;
            else ply.Story_Jump = false;
            if (PlyD) ply.Story_Down = true;
            else ply.Story_Down = false;
            if (PlyDCencle) ply.Story_DownUp = true;
            else ply.Story_DownUp = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log(PlyStop+"  "+ collision+"  "+ collision.tag);
        if (collision.tag == "Player" && PlyStop)
        {
            if (ply.Story_RightMove && ply.transform.position.x >= transform.position.x)
            {
                ply.Story_RightMove = false;
                if (EndNext)
                {
                    Invoke("EEE", DelT);
                }
            }
            else if (ply.Story_LeftMove && ply.transform.position.x <= transform.position.x)
            {
                ply.Story_LeftMove = false;
                if (EndNext)
                {
                    Invoke("EEE", DelT);
                }
            }
        }
    }
    void EEE()
    {
        storyStart.NextStory();
    }
    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
