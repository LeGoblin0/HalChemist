using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Life
{
    Rigidbody2D rig;
    Camera cam;
    AudioSource aus;
    public AudioClip[] SoundPly;
    public bool InGame = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
        cam = Camera.main;
        tag = "Player";
        gameObject.layer = 10;
        if (InGame)
        {
            GameSystem.instance.Ply = transform;
            AllHold = GameSystem.instance.AllHold;
        }
        rig.gravityScale = DownSpeed;
        HpSetUI();
        ani.SetFloat("AttSpeed", AttSpeed);
        ani.SetFloat("DeshTime", DeshTime);
        StartCoroutine(Hulc());
        DrowUIHolder();
    }
    void PlySound(int i)
    {
        aus.PlayOneShot(SoundPly[i]);
    }

    [Header("테스트 중인가? 실시간으로 변경값 적용")]
    public bool TestSeting = true;
    [Header("이동가속도")]
    public float AcSpeed = 10;
    [HideInInspector]
    public float nowSpeedX = 0;
    [Header("이동 최대속도")]
    public float MaxSpeed = 10;

    [Header("기본공격 이미지")]
    public Transform BasAtt;
    [Header("적 던지는 속도")]
    public float EnemyThrowSpeed;
    [Tooltip("평타 쿨타임")]
    [Header("평타쿨타임")]
    public float AttTime = 2;
    //[Header("평타 클릭후 딜레이")]
    //public float AttTime1 = .1f;
    //[Header("평타클릭후 없어지는 시간")]
    //public float AttTime2 = .5f;

    [Header("공격 속도")]
    public float AttSpeed = 1.8f;

    [Tooltip("평타 쿨남은시간")]
    float nowAttTime = 0;
    [Header("평타 넉백 파워")]
    public float AttPower = 3;
    [Tooltip("잡는거리 원")]
    [Header("잡는거리")]
    public float HoldDis = 1;
    [Tooltip("잡는위치플레이어기준")]
    [Header("잡는위치(플레이어기준")]
    public Vector3 HoldPosPos;
    [Tooltip("점프 파워")]
    [Header("점프 파워")]
    public float JumpPower = 4;
    [Header("점프 추가")]
    public float JumpAddTime = .5f;
    float NowJumpAddTime = 0;
    [Header("점프 떨어지는 속도")]
    public float DownSpeed = 1;
    [Header("점프 떨어지는 최고속도")]
    public float DownMaxSpeed = 1;
    List<Hold> AllHold = new List<Hold>();
    //Transform HoldNowPos;
    [Tooltip("저장슬롯갯수")]
    [Header("저장슬롯갯수")]
    [Range(0,100)]
    public int HoldMax = 3;
    public Transform HoldUIBox;
    public Transform NowHoldUI;
    public Transform HoldUIBoxPar;
    [Tooltip("적 슬롯 넣기")]
    List<Transform> HoldSavePos = new List<Transform>();
    [Tooltip("지금 선택된 슬롯")]
    int nowSavePos = 0;
    [HideInInspector]
    [Tooltip("우센서")]
    public bool right = false;
    [HideInInspector]
    [Tooltip("좌센서")]
    public bool left = false;
    [HideInInspector]
    [Tooltip("바닥에 센서")]
    public bool down = false;

    [HideInInspector]
    [Tooltip("더블점프 했는지 여부")]
    public bool DJump = true;
    [Tooltip("더블점프 가능여부")]
    [Header("더블점프 가능한가?")]
    public bool DJumpOK = true;
    [Tooltip("움직임 제한")]
    [Header("움직임 제한")]
    public bool OnStory = false;
    //[HideInInspector]
    public bool OnStory_rigtht = false;
    //[HideInInspector]
    public bool OnStory_left = false;

    [HideInInspector]
    public bool StopGame = false;

    [Tooltip("움직임 제한")]
    [Header("움직임 제한")]
    public bool DontMove = false;
    [Tooltip("핸도 제한")]
    [Header("핸도 제한")]
    public bool DontHand = true;
    [Tooltip("잡기 제한(수동조절X)")]
    [Header("잡기 제한(수동조절X)")]
    public bool DontHold = false;
    [Tooltip("상하 던지기 제한")]
    [Header("상하 던지기 제한")]
    public bool DontUPThorw = false;
    public Animator ani;
    public Transform Hand;
    public Animator HandAni;
    [Tooltip("하강 불가")]
    [Header("하강 불가")]
    public bool DontDown = false;
    [Header("플레이어 무적")]
    public bool GodMove = false;
    [Header("플레이어 넉백파워")]
    public Vector2 KnockBackPower ;
    bool Knockback = false;
    [Header("플레이어 무적 시간")]
    public float GodTime = 2;
    float NowGodTime = 0;

    [Header("플레이어 평캔")]
    public bool AttStop = false;
    [Tooltip("핸도 On off")]
    [Header("핸도 On off")]
    public GameObject Hando;
    public bool HandoOn = true;

    bool AttDeshNow = false;
    private void FixedUpdate()
    {
        if (!DontMove)
        {
            AttDeshNow = false;
            Attback = false;
        }
        ComeStone();
        if (DontDown)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.gravityScale = 0;

        }
        else
        {
            rig.gravityScale = DownSpeed;
        }
        if (Knockback)
        {

            rig.velocity = new Vector2(KnockBackPower.x * transform.GetChild(0).localScale.x, KnockBackPower.y);
        }
        else if (Attback)
        {

            rig.velocity = -new Vector2(-AttPower * transform.GetChild(0).localScale.x, 0);
        }
        else if (AttDeshNow)
        {
            rig.velocity = new Vector2(AttDesh * transform.GetChild(0).localScale.x, 0);
        }
        

    }
    bool EEEE = false;
    public bool MoveMap = false;

    void Update()
    {
        MoveNowHoldUI();//잡는 UI 표시하기
        if (Hp <= 0 && !EEEE)
        {
            DiePly();//플레이어 사망
            return;
        }
        else if (EEEE) return;
        GodTimes();//무적시간
        DropMax();//떨어지는 최고속도 조절
        if (MoveMap)
        {
            if (down) MoveMap = false;
            return;
        }
        PlyAtt();//플레이어 공격
        if (DontMove)
        {
            rig.velocity = Vector2.zero;
            bool ats = true;
            if (AttStop && (ani.GetInteger("State") == 3 || ani.GetInteger("State") == 4)) 
            {
                if (Input.GetKeyDown(KeyCode.Space)  && NowDeshCoolTime <= 0&& !OnStory&&!StopGame) 
                {
                    ats = false;
                    EndAtt(1);
                }
            }
            if (ats) return;
        }
        
        if (TestSeting) TestSet();//테스트 실시간 수치 변경
        if (DeshNow)
        {
            if (left && transform.GetChild(0).localScale.x == -1) return;
            if (right && transform.GetChild(0).localScale.x == 1) return;
            rig.velocity = new Vector2(transform.GetChild(0).localScale.x * DeshSpeed,0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.M)&& !OnStory&&!StopGame)
        {
            MiniMap.SetActive(!MiniMap.activeSelf);
        }
        MoveRL();//좌우이동
        Jump();//점프

        Desh();
        if (!DontHand)
        {
            HoldThrow();//던지기 잡기
            //InputOutPut();//창고 넣고 꺼내기
        }
        if (HandoOn)
        {
            Hand.gameObject.SetActive(true);
        }
        else
        {
            Hand.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q) && StoneHold == null&& !OnStory&&!StopGame)
        {
            nowSavePos--;
            if (nowSavePos < 0) nowSavePos += HoldMax;
            nowSavePos = nowSavePos % HoldMax;
            PlySound(0); InputOutPut();
            //Debug.Log(nowSavePos + "창고:" + HoldSavePos[nowSavePos] + "...손 :" + HoldNowPos);
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F)) && StoneHold == null&& !OnStory && !StopGame)   
        {
            nowSavePos++;
            nowSavePos = nowSavePos % HoldMax;
            PlySound(0); InputOutPut();
            //Debug.Log(nowSavePos + "창고:" + HoldSavePos[nowSavePos] + "...손 :" + HoldNowPos);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)&& !OnStory && !StopGame)
        {
            nowSavePos = 0;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&& !OnStory && !StopGame)
        {
            nowSavePos = 1;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)&& !OnStory && !StopGame)
        {
            nowSavePos = 2;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)&& !OnStory && !StopGame)
        {
            nowSavePos = 3;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)&& !OnStory && !StopGame)
        {
            nowSavePos = 4;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)&& !OnStory && !StopGame)
        {
            nowSavePos = 5;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)&& !OnStory && !StopGame)
        {
            nowSavePos = 6;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)&& !OnStory && !StopGame)
        {
            nowSavePos = 7;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9)&& !OnStory && !StopGame)
        {
            nowSavePos = 8;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0)&& !OnStory && !StopGame)
        {
            nowSavePos = 9;
            while (nowSavePos > HoldMax) nowSavePos--;
        }
    }
    public GameObject MiniMap;

    Transform StoneHold;
    public float ComeStoneSpeed = 7;

    void ComeStone()
    {
        if (StoneHold != null)
        {
            Vector3 dis = new Vector3(StoneHold.position.x - Hand.position.x, StoneHold.position.y - Hand.position.y, 0);
            if (dis.sqrMagnitude < 0.1)
            {
                EndComdStone();
                return;
            }
            StoneHold.GetComponent<Rigidbody2D>().velocity = -dis.normalized * ComeStoneSpeed;
        }
    }
    void EndComdStone()
    {
        StoneHold = null;
        PlySound(6);
        HandAni.SetBool("Hold", true);
        HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().gravityScale = 0;
        HoldSavePos[nowSavePos].gameObject.layer = 8;//playeratt
        HoldSavePos[nowSavePos].GetComponent<Collider2D>().isTrigger = true;
        //Debug.Log(HoldNowPos.GetComponent<Rigidbody2D>().gravityScale);
        HoldSavePos[nowSavePos].parent = Hand;
        HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        HoldSavePos[nowSavePos].position = Hand.position + HoldPosPos;
        if (HoldSavePos[nowSavePos].GetComponent<Animator>() != null)
        {
            HoldSavePos[nowSavePos].GetComponent<Animator>().SetBool("On", true);
        }
        InputOutPut();
    }
    IEnumerator Hulc()
    {
        while (true)
        {
            if (HallucinationNow)
            {
                DDDDD hal = Instantiate(hallucinationObj, transform.position + new Vector3(0, 0, 1), Quaternion.Euler(Vector2.zero)).GetComponent<DDDDD>();
                hal.Downd = HallucinationTime1;
                hal.Black = HallucinationBlock;
                hal.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                hal.transform.localScale = transform.GetChild(0).lossyScale;
            }
            yield return new WaitForSeconds(HallucinationTime);
        }
    }
    bool HallucinationNow = false;
    [Header("플레이어 잔상")]
    public Transform hallucinationObj;

    [Header("플레이어 잔상 주기")]
    public float HallucinationTime = .05f;
    [Header("플레이어 잔상 사라지는 시간")]
    public float HallucinationTime1=3;

    [Header("플레이어 잔상 검은색")]
    public bool HallucinationBlock=false;
 
    void InputOutPut()
    {
        for(int i = 0; i < HoldMax; i++)
        {
            if (HoldSavePos[i] != null)
            {
                HoldSavePos[i].position = new Vector3(100000, 100000, 0);
                HoldSavePos[i].gameObject.SetActive(false);
                //PlySound(7);
                //Debug.Log(nowSavePos + "창고에 " + HoldSavePos[nowSavePos] + "저장");
            }
        }
        if (HoldSavePos[nowSavePos] != null)
        {
            HoldSavePos[nowSavePos].gameObject.SetActive(true);
            //HoldSavePos[nowSavePos].position = transform.position + HoldPosPos;
            HoldSavePos[nowSavePos].parent = Hand;
            HoldSavePos[nowSavePos].position = Hand.position + HoldPosPos;

            if (HoldSavePos[nowSavePos].GetComponent<Animator>() != null)
            {
                HoldSavePos[nowSavePos].GetComponent<Animator>().SetBool("On", true);
            }

            HandAni.SetBool("Hold", true);
            //Debug.Log(nowSavePos + "창고에 " + HoldNowPos + "꺼냄");
        }
        else
        {
            HandAni.SetBool("Hold", false);
        }
        DrowUIHolder();

    }

    void DrowUIHolder()
    {
        PlySound(0);
        while (HoldUIBoxPar.childCount < HoldMax)
        {
            Instantiate(HoldUIBox, HoldUIBoxPar);
        }
        while (HoldSavePos.Count < HoldMax)
        {
            HoldSavePos.Add(null);
        }
        for (int i = 0; i < HoldMax; i++)
        {
            if (HoldSavePos[i] == null) HoldUIBoxPar.GetChild(i).GetChild(0).gameObject.SetActive(false);
            else
            {
                HoldUIBoxPar.GetChild(i).GetChild(0).GetComponent<Image>().sprite = HoldSavePos[i].GetComponent<StoneDieAni>().StonImg;
                HoldUIBoxPar.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    void MoveNowHoldUI()
    {
        if ((NowHoldUI.position - HoldUIBoxPar.GetChild(nowSavePos).position).sqrMagnitude > .1f)
        {
            NowHoldUI.position += -(NowHoldUI.position - HoldUIBoxPar.GetChild(nowSavePos).position) * Time.deltaTime * 10;
        }
        else
        {
            NowHoldUI.position = HoldUIBoxPar.GetChild(nowSavePos).position;
        }
    }
    public Transform baseThrow;
    void HoldThrow()
    {
        if (Input.GetKeyDown(KeyCode.S) && !DontHold && StoneHold == null&& !OnStory && !StopGame)
        {
            HoldOk();
            
        }
        else if (Input.GetKeyDown(KeyCode.D) && !OnStory && !StopGame)
        {
            if (TThrowPos == null && HoldSavePos[nowSavePos] != null && StoneHold == null)
            {
                DontHold = true; HandAni.SetInteger("State", 2);
            }
            else if (TThrowPos == null)
            {
                HoldSavePos[nowSavePos] = Instantiate(baseThrow);
                StoneHold = HoldSavePos[nowSavePos];
                StoneHold.position = Hand.position;
                StoneHold.gameObject.layer = 8;
                StoneHold.GetComponent<Att>().Set = true;
                DontHold = true; HandAni.SetInteger("State", 2);
            }
            else if (TThrowPos != null)
            {
                TThrowPos.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                TThrowPos.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                TThrowPos.GetComponent<Collider2D>().isTrigger = false;
                TThrowPos.tag = "Ground";
                TThrowPos.gameObject.layer = 12;
            }


        }
    }
    [Tooltip("공격시 앞으로 순간이동 거리")]
    [Header("공격시 앞으로 순간이동 거리")]
    public float AttPosMove = .6f;
    [Tooltip("공중 공격시 앞으로 순간이동 가능여부")]
    [Header("공중 공격시 앞으로 순간이동 가능여부")]
    public bool AirAttPosMove = false;
    [Tooltip("공격 가능 / 불가능")]
    [Header("공격 가능 / 불가능")]
    public bool att = false;
    [Tooltip("공격시 앞으로 이동 속도")]
    [Header("공격시 앞으로 이동 속도")]
    public float AttDesh = 10;
    Transform AttNowImg;
    public void ATTDDD()
    {

        AttDeshNow = true;
    }
    public void ATT2()
    {
        att2p = 1;
    }
    int att2p = 0;
    void PlyAtt()
    {
        if (nowAttTime > 0) nowAttTime -= Time.deltaTime;
            //Debug.Log(!OnStory+"   "+att);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (att2p==1)
            {
                att2p = 2;
                Debug.Log(0);
                ani.SetBool("Att2", true);
            }
            if (nowAttTime <= 0 && att && !OnStory && !StopGame)
            {
                att = false;
                rig.velocity = new Vector2(0, rig.velocity.y);
                nowAttTime = AttTime;
                if (down)
                {
                    ani.SetInteger("State", 3);


                }
                else
                {
                    ani.SetInteger("State", 4);
                    DontDown = true;
                }

                PlySound(3);
                DontMove = true;
            }
        }
    }
    
    public Transform AttObj;
    void GodTimes()
    {
        if (NowGodTime > 0)
        {
            NowGodTime -= Time.deltaTime;
            if(((int)(NowGodTime*10))%2==0) transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            else transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            if (NowGodTime <= 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                GodMove = false;
                NowGodTime = 0;
            }
        }
    }
    void DiePly()
    {
        HandAni.SetTrigger("On");
        PlySound(1);
        EEEE = true;
        rig.bodyType = RigidbodyType2D.Kinematic;
        ani.SetTrigger("Die");
        DontMove = true;
        rig.velocity = Vector2.zero;
    }
    void DropMax()
    {
        if (rig.velocity.y < -DownMaxSpeed)
        {
            rig.velocity = new Vector2(rig.velocity.x, -DownMaxSpeed);
        }
    }
    public void ThrowEnd()
    {
        DontHold = false;
        HandAni.SetBool("Hold", false);
        bool ddd = true;
        for (int j = 0; j < HoldMax; j++)
        {
            if (HoldSavePos[(nowSavePos + j) % HoldMax] != null)
            {
            //Debug.Log(HoldSavePos[(nowSavePos + j) % HoldMax]+"    " + (nowSavePos + j) % HoldMax);
                nowSavePos = (nowSavePos + j) % HoldMax;
                ddd = false;
                break;
            }
        }
        if (ddd)
        {
            nowSavePos = 0;
           // Debug.Log(0);
        }
        InputOutPut();
    }
    Transform TThrowPos = null;
    public void Throw()
    {
        float yy = 0;
        float xx = 0;
        if (!DontUPThorw)
        {
            if (Input.GetKey(KeyCode.DownArrow)&& !OnStory && !StopGame) yy = -1;
            else if (Input.GetKey(KeyCode.UpArrow)&& !OnStory && !StopGame) yy = 1;
        }
        if ((Input.GetKey(KeyCode.RightArrow) && !OnStory && !StopGame) || (OnStory_rigtht && !StopGame)) xx = 1;
        else if ((Input.GetKey(KeyCode.LeftArrow) && !OnStory && !StopGame) || (OnStory_left && !StopGame)) xx = -1;
        if (yy == 0) xx = transform.GetChild(0).localScale.x;

        //Debug.Log(0);
        if (HoldSavePos[nowSavePos].GetComponent<Att>().AttState == Life.State.일반) 
        {
            TThrowPos = HoldSavePos[nowSavePos];
            Destroy(TThrowPos.gameObject, 5);
            HoldSavePos[nowSavePos].tag = "Att";
            HoldSavePos[nowSavePos].gameObject.AddComponent<Att>().AttDamage = HoldSavePos[nowSavePos].GetComponent<Hold>().ThrowDamage;
            HoldSavePos[nowSavePos].gameObject.GetComponent<Att>().Set = true;
            HoldSavePos[nowSavePos].gameObject.GetComponent<Att>().GroundDes = true;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().gravityScale = 0;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().velocity = new Vector2(xx, yy).normalized * EnemyThrowSpeed;
            Destroy(HoldSavePos[nowSavePos].GetComponent<Hold>());
            HoldSavePos[nowSavePos].parent = null;
            HoldSavePos[nowSavePos] = null;
        }
        else
        {
            HoldSavePos[nowSavePos].tag = "Att";
            HoldSavePos[nowSavePos].gameObject.AddComponent<Att>().AttDamage = HoldSavePos[nowSavePos].GetComponent<Hold>().ThrowDamage;
            HoldSavePos[nowSavePos].gameObject.GetComponent<Att>().Set = true;
            HoldSavePos[nowSavePos].gameObject.GetComponent<Att>().GroundDes = true;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().gravityScale = 0;
            HoldSavePos[nowSavePos].GetComponent<Rigidbody2D>().velocity = new Vector2(xx, yy).normalized * EnemyThrowSpeed;
            Destroy(HoldSavePos[nowSavePos].GetComponent<Hold>());
            HoldSavePos[nowSavePos].parent = null;
            HoldSavePos[nowSavePos] = null;
        }
    }
    public bool MapMove = false;
    void Jump()//점프
    {
        if (MapMove)
        {
            NowJumpAddTime = 0;
            MapMove = false;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (down || DJump)&& !OnStory && !StopGame)
        {

            if (down)
            {
                down = false;
                NowJumpAddTime = JumpAddTime;
                nowAttTime = 0.1f;//공격쿨타임 제한
            }
            if (down && DJumpOK)
            {
                DJump = true;
            }
            else DJump = false;
            
            rig.velocity = new Vector2(rig.velocity.x, JumpPower);

            PlySound(4);
        }
        if (!Input.GetKey(KeyCode.UpArrow)&& !OnStory && !StopGame)
        {
            NowJumpAddTime = 0;
        }
        else if (NowJumpAddTime > 0) 
        {
            NowJumpAddTime -= Time.deltaTime;

            rig.velocity = new Vector2(rig.velocity.x, JumpPower);
        }
        //Debug.Log(NowJumpAddTime > 0);
    }
    bool DeshNow = false;
    public float DeshTime = 1f;
    public float DeshSpeed = 5;
    public float DeshCoolTime = 1;
    float NowDeshCoolTime = 0;
    void Desh()
    {
        if (Input.GetKey(KeyCode.Space)  && NowDeshCoolTime<=0&& !OnStory && !StopGame)
        {
            DeshNow = true;
            HallucinationNow = true;
            DontDown = true;
            ani.SetTrigger("Desh");
            NowDeshCoolTime = DeshCoolTime;

            PlySound(2);

            if (AttNowImg != null) AttNowImg.parent = null;
        }
        else if (NowDeshCoolTime >= 0)
        {
            NowDeshCoolTime -= Time.deltaTime;
        }
    }
    public void EndDesh()
    {
        DeshNow = false;
        HallucinationNow = false;
        DontDown = false;
    }
    float WalkTime = 0.2f;
    void MoveRL()//좌우이동
    {
        if (Input.GetKey(KeyCode.DownArrow) && down&& !OnStory && !StopGame)
        {
            ani.SetInteger("State", 5);
            rig.velocity = Vector2.zero;
            Hand.GetComponent<Hand>().Offset = new Vector3(-1, 0, 0);
            if (Hand.gameObject.activeSelf) HandAni.SetInteger("State", 0);
            return;
        }
        else
        {
            Hand.GetComponent<Hand>().Offset = new Vector3(-1, .5f, 0);
        }
        if (((Input.GetKey(KeyCode.RightArrow) && !OnStory && !StopGame) ||( OnStory_rigtht && !StopGame)) && !right) 
        {
            nowSpeedX += AcSpeed * Time.deltaTime;
            if (nowSpeedX > MaxSpeed) nowSpeedX = MaxSpeed;
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            if (down) ani.SetInteger("State", 1);
            else ani.SetInteger("State", 2);

            if (Hand.gameObject.activeSelf) HandAni.SetInteger("State", 1);
            if (WalkTime < 0)
            {
                WalkTime = .3f;
            }
            else WalkTime -= Time.deltaTime;
        }
        else if (((Input.GetKey(KeyCode.LeftArrow) && !OnStory && !StopGame) || (OnStory_left && !StopGame)) && !left) 
        {
            nowSpeedX -= AcSpeed * Time.deltaTime;
            if (nowSpeedX < -MaxSpeed) nowSpeedX = -MaxSpeed;
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            if (down) ani.SetInteger("State", 1);
            else ani.SetInteger("State", 2);

            if (Hand.gameObject.activeSelf) HandAni.SetInteger("State", 1);
            if (WalkTime < 0)
            {
                WalkTime = .3f;
            }
            else WalkTime -= Time.deltaTime;
        }
        else
        {
            nowSpeedX = 0;

            if (down) ani.SetInteger("State", 0);
            else ani.SetInteger("State", 2);

            if (Hand.gameObject.activeSelf) HandAni.SetInteger("State", 0);
        }
        rig.velocity = new Vector2(nowSpeedX, rig.velocity.y);/// (Mass + holdMass);
    }
    public void AttOff()//근접공격 몇초뒤 없애기 
    {
        if (AirAttPosMove || ani.GetInteger("State")==3) 
        {
            AttNowImg = Instantiate(AttObj, BasAtt);
            AttNowImg.parent = transform;
            transform.position = transform.position + new Vector3(AttPosMove * transform.GetChild(0).localScale.x, 0);
        }
        //Debug.Log(down);
        //BasAtt.gameObject.SetActive(true);
        AttDeshNow = true;
    }
    bool Attback = false;
    public void AttBa()
    {
        Attback = true;
    }
    public void EndAtt(int a)
    {
        if (att2p == 2)
        {
            ani.SetInteger("State", 13);
            Debug.Log(1);
        }
        else
        {
            ani.SetInteger("State", a);
            att2p = 0;
            Debug.Log(2);
        }
        DontDown = false;
        DontMove = false;


    }
    public void EndAtt2()
    {
        att2p = 0;
        ani.SetInteger("State", 0);
        DontDown = false;
        DontMove = false;


        ani.SetBool("Att2", false);
    }
    public void TestSet()
    {
        rig.gravityScale = DownSpeed;

        ani.SetFloat("AttSpeed", AttSpeed);

        ani.SetFloat("DeshTime", DeshTime);
    }
    bool HoldOk()//잡기
    {
        
        float MinHoldDis = HoldDis * HoldDis + 1;
        int i;
        bool ddd = true;
        int nnn = nowSavePos;

        for (int j = 0; j < HoldMax; j++)
        {
            //Debug.Log(HoldSavePos[(nowSavePos + j) % HoldMax]);
            if (HoldSavePos[(nowSavePos + j) % HoldMax] == null)
            {
                nowSavePos = (nowSavePos + j) % HoldMax;
                ddd = false;
            }
        }
        if (ddd) return false;


        for (i = 0; i < AllHold.Count; i++)
        {
            if (AllHold[i] != null && AllHold[i].gameObject.activeSelf && new Vector3(AllHold[i].transform.position.x - transform.position.x, AllHold[i].transform.position.y - transform.position.y, 0).sqrMagnitude < HoldDis * HoldDis && new Vector3(AllHold[i].transform.position.x - transform.position.x, AllHold[i].transform.position.y - transform.position.y, 0).sqrMagnitude < MinHoldDis)
            {
                HoldSavePos[nowSavePos] = AllHold[i].transform;
                MinHoldDis = new Vector3(AllHold[i].transform.position.x - transform.position.x, AllHold[i].transform.position.y - transform.position.y, 0).sqrMagnitude;
            }
        }
        if (HoldSavePos[nowSavePos] != null)
        {
            StoneHold = HoldSavePos[nowSavePos];
            StoneHold.GetChild(1).gameObject.SetActive(false);

        }
        else
        {
            nowSavePos = nnn;
        }
        return false;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (!GodMove)
        {
            if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set) 
            {
                Hp -= collision.GetComponent<Att>().AttDamage;
                GodMove = true;
                DontMove = true;
                rig.velocity = Vector2.zero;
                HpSetUI();
                EndDesh();
                if (Hp > 0)
                {
                    ani.SetTrigger("Hit");
                    rig.velocity = new Vector2(KnockBackPower.x * transform.GetChild(0).localScale.x, KnockBackPower.y);
                    Knockback = true;
                    NowGodTime = GodTime;
                    //BasAtt.gameObject.SetActive(false);
                }
                else
                {
                    GodMove = true;
                    ani.SetTrigger("Die");
                }
            }
        }
    }
    public void EndHit()
    {
        DontMove = false;
        Knockback = false;

        if (down) ani.SetInteger("State", 1);
        else ani.SetInteger("State", 2);
        DontDown = false;


    }
    public void EndDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//죽으면 씬 다시로드
        //Vector3 GG = GameSystem.instance.DiePos();
        //Hand.position = new Vector3( GG.x,GG.y,Hand.position.z);
        //transform.position = new Vector3(GG.x, GG.y, transform.position.z);
        //cam.transform.position = new Vector3(GG.x, GG.y, cam.transform.position.z);
        //ani.SetInteger("State", 0);
    }
    public Transform HpParObj;
    public Transform HpPrefeb;
    public void HpSetUI()
    {
        if (InGame)
        {
            while (HpParObj.childCount < Hp)
            {
                Instantiate(HpPrefeb, HpParObj);
            }
            int i = 0;
            for (i = 0; i < Hp; i++)
            {
                HpParObj.GetChild(i).gameObject.SetActive(true);
            }
            for (; i < HpParObj.childCount; i++)
            {
                HpParObj.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
    public void FullHp()
    {
        Hp = HpParObj.childCount;
        HpSetUI();
    }

    public void HandoOnOff()
    {
       HandoOn = true;
       DontHand = false;
    }
}
