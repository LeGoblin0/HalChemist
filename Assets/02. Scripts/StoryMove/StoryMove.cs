using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMove : MonoBehaviour
{
    Player ply;
    StoryStart storyStart;


    [Header("좌표에서 멈추기")]
    [Header("===========기본이동===========")]
    [Space]
    public bool PlyStop;
    [Header("오른쪽 이동")]
    public bool PlyRMove;
    [Header("위치로 이동")]
    public bool PlyPosMove;
    public Transform PlyPosStopTr;
    [Header("왼쪽 이동")]
    public bool PlyLMove;
    [Header("점프")]
    public bool PlyJump;
    [Header("앉기")]
    public bool PlyD;
    [Header("앉기 취소")]
    public bool PlyDCencle;


    [Header("NPC스토리 띄우기")]
    [Header("===========N P C===========")]
    [Space]
    [Space]
    [Space]
    [Space]
    public bool NPCNowSay;
    [Header("NPC현재 스토리 넘기기(읽기보다 먼저 실행)")]
    public int NPCEndSay = -1;
    [Header("NPC수동설정-미설정시 부모검색")]
    public ObjData SetNPC;
    [Header("대사 띄우기")]
    public bool ONUI;
    bool ONUI2;
    [TextArea(2, 100)]
    public string OnUITxt;
    public float PointLookTime = .2f;
    float NowPointLookTime = 0;
    int NowPoint = 0;

    [Header("다음 실행")]
    [Header("===========시 스 템===========")]
    [Space]
    [Space]
    [Space]
    [Space]
    public bool EndNext;
    [Header("다음 실행 G키 입력")]
    public bool EndNext_G;
    bool FirstG = true;
    [Header("반복")]
    public bool Loop = false;
    [Header("마지막 처다볼곳 지연시간 적용 X")]
    [Range(-1,1)]
    public int LastLook;
    [Header("다음실행 지연시간")]
    public float DelT = 0;
    [Header("오브젝트 활성화")]
    public GameObject SetObjT;
    [Header("오브젝트 비활성화")]
    public GameObject DisObjT;


    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        storyStart = transform.GetComponent<StoryStart>();
        if (storyStart == null) storyStart = transform.parent.GetComponent<StoryStart>();
        if (storyStart == null) storyStart = transform.parent.parent.GetComponent<StoryStart>();
        ONUI2 = ONUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (ONUI)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                NowPoint = OnUITxt.Length;
            }
            GameSystem.instance.TextSay.SetActive(true);
            if (NowPointLookTime <= 0)
            {
                if(NowPoint< OnUITxt.Length)
                {
                    NowPoint++;
                }
                else
                {
                    EndNext_G = true;
                    ONUI = false;
                }
                GameSystem.instance.TextSay.transform.GetChild(0).GetComponent<Text>().text = OnUITxt.Substring(0, NowPoint);
                NowPointLookTime = PointLookTime;
            }
            else NowPointLookTime -= Time.deltaTime;
        }
        else if (EndNext_G && FirstG && Input.GetKeyDown(KeyCode.G))
        {
            GameSystem.instance.TextSay.SetActive(false);
            NowPointLookTime = 0;
            NowPoint = 0;
            ONUI = ONUI2;

            FirstG = false;
            Invoke("EEE", DelT);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (EndNext && !PlyStop) 
            {
                if (LastLook != 0)
                {
                    ply.PlyLook = LastLook;
                }
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

            if (PlyPosMove)
            {
                if (ply.transform.position.x > PlyPosStopTr.position.x)
                {
                    ply.Story_LeftMove = true;
                }
                else
                {
                    ply.Story_RightMove = true;
                }
            }
            if (NPCEndSay != -1) 
            {
                if (SetNPC != null) ;
                else if (transform.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.GetComponent<ObjData>();
                else if (transform.parent.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.parent.GetComponent<ObjData>();
                else if (transform.parent.parent.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.parent.parent.GetComponent<ObjData>();
                SetNPC.EndSay(NPCEndSay);
            }
            if (NPCNowSay)
            {
                if (SetNPC != null) ;
                else if (transform.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.GetComponent<ObjData>();
                else if (transform.parent.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.parent.GetComponent<ObjData>();
                else if (transform.parent.parent.parent.GetComponent<ObjData>() != null) SetNPC = transform.parent.parent.parent.GetComponent<ObjData>();
            }
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
                    if (LastLook != 0)
                    {
                        ply.PlyLook = LastLook;
                    }
                    Invoke("EEE", DelT);
                }
            }
            else if (ply.Story_LeftMove && ply.transform.position.x <= transform.position.x)
            {
                ply.Story_LeftMove = false;
                if (EndNext)
                {
                    if (LastLook != 0)
                    {
                        ply.PlyLook = LastLook;
                    }
                    Invoke("EEE", DelT);
                }
            }
        }
    }
    void EEE()
    {
        storyStart.NextStory();
        if (NPCNowSay)
            SetNPC.NPCOpen();
        if (DisObjT != null) DisObjT.SetActive(false);
         FirstG = true;
    }
    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
        if (SetObjT != null) SetObjT.SetActive(true);
    }
}
