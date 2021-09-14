using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Life
{
    Rigidbody2D rig;
    AudioSource aus;
    BoxCollider2D col;
    public AudioClip[] SoundPly;
    [HideInInspector]
    [Tooltip("0 : 기본 \n1:뛰기\n2:점프")]
    public Animator ani;
    public Animator Handani;

    const float PlYLAYER_NUM = 3;

    [Header("핸도 위치")]
    public Transform Hand;

    public void SaveHp()
    {
        Hp = MaxHP;
        HPUI();
    }
    private void Awake()
    {
        cam = Camera.main.GetComponent<Mcam>();
        rig = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        Handani = Hand.GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        MaxHP = GameSystem.instance.GiveMaxHp();
        Hp = MaxHP;

        tag = "Player";
        rig.gravityScale = gravityScale;


        HaveStone = GameSystem.instance.GiveStone();
        if (HaveStone == null) HaveStone = new int[MaxStoneNum];
        if (HaveStone.Length < MaxStoneNum)
        {
            int[] aa = HaveStone;
            HaveStone = new int[MaxStoneNum];
            for (int i = 0; i < aa.Length; i++) 
            {
                HaveStone[i] = aa[i];
            }
        }
        HaveStone[0] = 1;
        StoneUI();
        HPUI();
        
        ani.SetFloat("HitThrowTime", HitThrowTime);

        saveMoneyInt = GameSystem.instance.MoneyInt();
        Money = saveMoneyInt[0];
        Money2 = saveMoneyInt[1];
        LookMoney();

        MoneyBagImg.sprite = MoneyBag[GameSystem.instance.GiveMoneyBag() ? 1 : 0];
    }

    public Sprite[] MoneyBag;
    public Image MoneyBagImg;

    public int[] saveMoneyInt;

    [Header("스토리 실행")]
    public bool OnStory = false;
    public bool Story_LeftMove = false;
    public bool Story_RightMove = false;
    public bool Story_Jump = false;
    public bool Story_Down = false;
    public bool Story_DownUp = false;
    public bool Story_Desh = false;


    [Header("센서")]
    public bool right = false;
    public bool left = false;
    public bool down = false;
    public bool airAtt = false;
    public bool Jump01 = false;


    [Header("움직임")]
    public bool DontMove = false;
    public float MaxSpeed = 1.8f;
    public float JumpPower = 3;
    public float gravityScale = 2;
    public float WatergravityScale = .8f;
    bool DontKeyStayMove = false;
    public float DownMaxSpeed = 4;
    [Range(0,1f)]
    public float LongJumpTime = .3f;

    void PlySound(int i)
    {
        aus.PlayOneShot(SoundPly[i]);
    }

    [Header("데쉬")]
    public float DeshCoolTime = 2;
    float nowDeshCoolTime = 0;
    public float DeshSpeedd = 5;
    public Transform PlyDeshPre;
    public float ImgDeshTime = .1f;
    public float ImgDestorTime = 0.1f;
    float ImgDeshNowTime = 0;

    [Header("공격")]
    public float AttSpeed = 1.8f;
    public float AttCoolTime = 2f;
    [Tooltip("평타 쿨남은시간")]
    float nowAttTime = 0;

    [Tooltip("평타 불가시간")]
    float DontAttTime = 0;

    [Header("피격")]
    [Tooltip("무적모드")]
    public float GodTime = 1.8f;
    float nowGodTime = 1.8f;
    [Tooltip("넉백 파워")]
    public Vector2 ThrowF = new Vector2(-7, 4);
    [Tooltip("넉백 시간")]
    public float HitThrowTime = .6f;

    [Header("원석")]
    public int[] HaveStone;
    public int MaxStoneNum = 3;
    [HideInInspector]
    public int NowChoose = 0;
    [Tooltip("스톤 겹치는 수량")]
    public int StackStone = 3;//스톤겹친갯수
    Transform ThrowStone;//던진 스톤 정보 
    //[Tooltip("던질파워")]
    //public float ThrowPower = 5;
    //[Tooltip("던지고 없어지는 시간")]
    //public float DesTimeStone = 5;
    public Transform StoneUITr;
    [Tooltip("기본 원석 생성 주기")]
    public float BaseStoneCoolTime = 5;
    //[Tooltip("던질때 회전")]
    //public float ThrowRollPower = 720;
    //[Tooltip("추가탄 속도")]
    //public float StoneShootPower = 3;

    [HideInInspector]
    [Tooltip("플레이어가 보고 있는 방향 [1: 우 ]  [2: 좌 ]")]
    public int PlyLook = 1;


    [Header("시스템")]
    public Transform HPUITr;

    [Header("카메라")]
    [HideInInspector]
    public Mcam cam;

    //[Header("대화문")]
    //public DialogManager manager;
    //GameObject scanObject;
    ObjData NowObj;

    Vector3 dirVec;
    private void FixedUpdate()
    {
        if (NowDie) return;
        AniMove();
        //NpcCheck();

        if (rig.velocity.y < -DownMaxSpeed) 
        {
            rig.velocity = new Vector2(rig.velocity.x, -DownMaxSpeed);
        }
    }
    bool GodMode = false;
    bool GGGodMod;
    void GGGGG()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    bool NowDie = false;

    public GameObject ShootThrowUI;//G나오는 이모티콘
    void Update()
    {
        bool ff = false;
        if (NowObj != null) ff = true;
        if (ShootStone == null) ShootStone = new Transform[100];
        for (int i = 0; !ff && i < ShootStone.Length; i++) 
        {
            if (ShootStone[i] != null)
            {
                ff = true;
                break;
            }
        }
        ShootThrowUI.SetActive(ff);
        
        if (NowDie) return;

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_2")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down01") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down02"))
        {
            if (TrainNow != null)
            {
                transform.position = new Vector3(transform.position.x, TrainNow.transform.position.y+1, transform.position.z);
            }
        }
        

        if (Hp <= 0)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            ani.SetTrigger("Die");
            rig.bodyType = RigidbodyType2D.Static;
            Time.timeScale = 1;
            NowDie = true;
        }
        //Debug.Log(SaveTrtr);
        Ply_Move();
        Ply_Desh();
        Ply_Att();

        Ply_Throw();
        InputTest();
        ObjSet();

        if (nowGodTime >= 0)
        {
            nowGodTime -= Time.deltaTime;
            GGGodMod = true;
        }
        else if(GGGodMod)
        {
            GGGodMod = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("GGGGG", 0);
        }


        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Desh01") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Desh02")) 
        {
            ImgDeshNowTime -= Time.deltaTime;
            if (ImgDeshNowTime <= 0)
            {
                ImgDeshNowTime = ImgDeshTime;
                Transform aa = Instantiate(PlyDeshPre);
                aa.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                aa.localScale = transform.GetChild(0).localScale;
                aa.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
                Destroy(aa.gameObject, ImgDestorTime);
            }
        }
        

        if (nowGodTime > 0)
        {
            if (((int)(nowGodTime * 10)) % 10 % 2 == 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            }
            else transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        
    }
    public GameObject NowChooseObj;
    public void ObjSet()
    {
        if (!OnStory && Input.GetKeyDown(KeyCode.G))
        {

            if (ShootStone == null) ShootStone = new Transform[100];
            bool STSTS = false;
            //Debug.Log(ShootStone+ "   " +ShootStone.Length);
            for(int i = 0; i < ShootStone.Length; i++)
            {
                if (ShootStone[i] != null)
                {
                    ShootStone[i].parent.parent.GetComponent<Animator>().SetTrigger("Shoot");
                    STSTS = true;
                    Vector2 go = -(ShootStone[i].parent.parent.position - ShootStone[i].position);
                    ShootStone[i].parent = null;
                    //Debug.Log(go);
                    ShootStone[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    ShootStone[i].GetComponent<Rigidbody2D>().velocity = go.normalized * ShootStone[i].GetComponent<StoneDieAni>().ThrowSpeed;
                    ShootStone[i].GetComponent<Collider2D>().enabled = true;
                    ShootStone[i].GetComponent<StoneDieAni>().DieSet = true;
                    ShootStone[i].GetComponent<StoneDieAni>().enabled = true;
                    ShootStone[i] = null;
                }
            }
            if (STSTS) return;
            if (NowChooseObj != null && NowChooseObj.GetComponent<StoneDieAni>() != null)
            {
                int Stonecode = NowChooseObj.GetComponent<StoneDieAni>().Code;
                for (int i = 1; i < HaveStone.Length; i++)
                {
                    if (HaveStone[i] / 1000 == Stonecode && HaveStone[i] % 1000 < StackStone) //코드가 같으면 최대 스텍갯수가 적으면
                    {
                        HaveStone[i]++;
                        Destroy(NowChooseObj.gameObject);
                        StoneUI();
                        return;
                    }
                }
                for (int i = 1; i < HaveStone.Length; i++)
                {
                    if (HaveStone[i] == 0) //비어있으면
                    {
                        HaveStone[i] = (Stonecode * 1000 + 1);
                        Destroy(NowChooseObj.gameObject);
                        StoneUI();
                        return;
                    }
                }
            }
            else if (NowChooseObj != null && NowChooseObj.tag == "Save" && NowChooseObj.GetComponent<SaveTrTr>() != null) 
            {
                if (Money2 > 0) MonyBagAni.SetTrigger("On");
                int[] ss = new int[HaveStone.Length];
                for(int i = 0; i < ss.Length; i++)
                {
                    ss[i] = HaveStone[i];
                }
                if(NowChooseObj.GetComponent<SaveTrTr>().SaveOn())
                {
                    GameSystem.instance.GiveMoneyBag(false);

                    MoneyBagImg.sprite = MoneyBag[GameSystem.instance.GiveMoneyBag() ? 1 : 0];
                }
                Money += Money2;
                Money2 = 0;
                LookMoney();
                PlySave();
            }
        }
    }
    public Animator MonyBagAni;
    public void PlySave( bool moneySave=true)
    {
        if (moneySave)
        {
            saveMoneyInt[0] = Money;
            saveMoneyInt[1] = Money2;
        }

        GameSystem.instance.GiveStone(HaveStone);

        GameSystem.instance.Save();
    }
   
    [HideInInspector]
    public Vector3 trapsavepoint;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "TrapSavePoint")
        {
            trapsavepoint = transform.position;//+new Vector3(0,0.2f);
        }
        else if (!GodMode && collision.tag == "TrapGround")
        {
            if (Hp > 1)
            {
                TrapSaveDie();
            }
            Hp--; HPUI();
        }
        else if (!GodMode && collision.tag == "Water")
        {
            rig.mass = 2;
            rig.gravityScale = WatergravityScale;
            rig.velocity = rig.velocity * .9f;
            TrapSaveDie();
        }
        else if (collision.tag == "NPCObj")
        {
            NowObj = collision.GetComponent<ObjData>();
        }
    }


    void TrapSaveDie()
    {

        GodMode = true;
        DontMove = true;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        ani.SetTrigger("Hit");
        //rig.bodyType = RigidbodyType2D.Static;
        Time.timeScale = 1;
        //NowDie = true;
        Invoke("OffDisply", .5f);
        Invoke("ReAni", .8f);
    }
    void OffDisply()
    {
        GameSystem.instance.CanversUI.GetChild(1).GetComponent<Animator>().SetTrigger("On");
    }
    void ReAni()
    {
        GodMode = false;
        DontMove = false;
        //Debug.Log(trapsavepoint);
        transform.position = trapsavepoint;
        transform.position = new Vector3(transform.position.x, transform.position.y, PlYLAYER_NUM);
        ani.SetInteger("State", 0);
        //rig.bodyType = RigidbodyType2D.Dynamic;
        Time.timeScale = 1;
        //NowDie = false;
    }


    protected void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (NowDie) return;
        if (!GodMode && collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && nowGodTime <= 0 )
        {
            //            Debug.Log(collision.name);
            Hp -= collision.GetComponent<Att>().AttDamage;
            nowGodTime = GodTime;
            ani.SetTrigger("Hit");
            DontMove = true;
            rig.velocity = new Vector2(ThrowF.x * PlyLook, ThrowF.y);


            HPUI();
            //Debug.Log(rig.velocity);
            //Debug.Log(new Vector2(ThrowF.x * PlyLook, ThrowF.y));

        }
        if (ThrowStone != collision.transform && collision.gameObject.layer == 26)  
        {
            if (NowChooseObj != null)
            {
                if (NowChooseObj.transform.childCount >= 1 && NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>() != null)
                {
                    NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>().outlineSize = 0;
                    NowChooseObj.transform.position = new Vector3(NowChooseObj.transform.position.x, NowChooseObj.transform.position.y, NowChooseObj.transform.position.z + 0.1f);
                }
            }
            NowChooseObj = collision.gameObject;
            if (NowChooseObj.transform.childCount >= 1 && NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>() != null)
            {
                NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>().outlineSize = 1;
                NowChooseObj.transform.position = new Vector3(NowChooseObj.transform.position.x, NowChooseObj.transform.position.y, NowChooseObj.transform.position.z - 0.1f);
            }
        }

        if (collision.tag == "Item")
        {
            collision.GetComponent<ItemCode>().Eat();
            if (collision.GetComponent<ItemCode>().ItemCodeNum == 0)
            {
                if(Money<=9999)
                {
                    Money++;
                    if (!GameSystem.instance.GiveMoneyBag()) Money2++;
                    LookMoney();
                }
            }
            else if (collision.GetComponent<ItemCode>().ItemCodeNum == 1)
            {

                if (Hp < MaxHP) Hp++;
                HPUI();
            }
            else if (collision.GetComponent<ItemCode>().ItemCodeNum == 2)
            {

                Hp++;
                MaxHP++;
                GameSystem.instance.GiveMaxHp(MaxHP);
                HPUI();
            }
        }
        //if (!GodMode && collision.tag == "Water")
        //{
        //    rig.mass = 700;
        //    rig.gravityScale = .3f;
        //    rig.velocity = rig.velocity * .7f;
        //    //TrapSaveDie();
        //}

        //if (collision.transform.parent != null && collision.transform.parent.GetComponent<Train>() != null)
        //{
        //    TrainNow = collision.transform.parent.GetComponent<Rigidbody2D>();
        //}

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == NowChooseObj)
        {
            if (NowChooseObj.transform.childCount >= 1 && NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>() != null)
            {
                NowChooseObj.transform.GetChild(0).GetComponent<SpriteOutline>().outlineSize = 0;
                NowChooseObj.transform.position = new Vector3(NowChooseObj.transform.position.x, NowChooseObj.transform.position.y, NowChooseObj.transform.position.z + 0.1f);
            }
            NowChooseObj = null;
        }
        else if (collision.tag == "NPCObj")
        {
            if (NowObj == collision.GetComponent<ObjData>()) NowObj = null;
        }
        else if (collision.tag == "Water")
        {
            rig.mass = 1;
            rig.gravityScale = gravityScale;
        }
    }

    public Text MoneyInt;
    public int Money;

    public bool LostMoney2;
    public int Money2;
    public Text MoneyInt2;

    void LookMoney()
    {
        MoneyInt.text = Money + "";
        MoneyInt2.text = Money2 + "";
    }
    void Ply_Move()
    {
        if (!DontMove)
        {
            if (down && !ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump"))
            {
                ani.SetFloat("AirTime", 0);
            }
            else
            {
                ani.SetFloat("AirTime", ani.GetFloat("AirTime") + Time.deltaTime);
            }
            if (((!OnStory && Input.GetKey(KeyCode.DownArrow)) || (OnStory && Story_Down && !Story_DownUp)) && down) 
            {
                if (OnStory) Story_DownUp = false;
                ani.SetInteger("State", 3);
                this.Hand.GetComponent<Hand>().offset = new Vector3(-1, 0f, 0);
                col.offset = new Vector2(col.offset.x, 0.3257405f);
                col.size = new Vector2(col.size.x, 0.5742418f);
            }
            else if ((OnStory && Story_DownUp) || (Input.GetKeyUp(KeyCode.DownArrow) && !OnStory)) 
            {
                if (OnStory)
                {
                    Story_DownUp = false;
                    Story_Down = false;
                }
                col.offset = new Vector2(col.offset.x, .4846133f);
                col.size = new Vector2(col.size.x, 0.9344962f);
                this.Hand.GetComponent<Hand>().offset = new Vector3(-1, .5f, 0);
            }
            else if ((!OnStory && Input.GetKey(KeyCode.RightArrow)) || (OnStory && Story_RightMove))
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = 1;
                DontKeyStayMove = false;
            }
            else if ((!OnStory && Input.GetKey(KeyCode.LeftArrow)) || (OnStory && Story_LeftMove)) 
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = -1;
                DontKeyStayMove = false;
            }
            else if (OnStory||(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
            {
                if (down) ani.SetInteger("State", 0);
                else
                {
                    ani.SetInteger("State", 2);
                }
            }
            if (((!OnStory && Input.GetKeyDown(KeyCode.UpArrow)) || (OnStory && Story_Jump)) && down) 
            {
                if (OnStory) Story_Jump = false;
                ani.SetBool("RL", true);
                ani.SetInteger("State", 2);
                DontAttTime = .2f;
                Jump01 = false;
                DontMove = true;
                NowJumpPower = JumpPower;
            }
            else
            {
                ani.SetBool("RL", false);
            }
            if (rig.mass == 1) rig.gravityScale = gravityScale;
        }
        else
        {

        }
        transform.GetChild(0).localScale = new Vector3(PlyLook, 1, 1);
        if (!OnStory && Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (rig.velocity.y > 0) rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * LongJumpTime);
            else NowJumpPower = JumpPower * LongJumpTime;
            
        }
    }
    void Ply_Desh()
    {

        if ((!OnStory && Input.GetKeyDown(KeyCode.Space) && nowDeshCoolTime <= 0) || (OnStory && Story_Desh))  
        {
            if (OnStory) Story_Desh = false;
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle") || ani.GetCurrentAnimatorStateInfo(0).IsName("Run") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump"))
            {
                ani.SetTrigger("Desh01");
                nowDeshCoolTime = DeshCoolTime;
                DontMove = true;
            }
            else if ((ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Air_Att_1") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_2")) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            {
                ani.SetTrigger("Desh02");
                nowDeshCoolTime = DeshCoolTime;
                DontMove = true;
            }
        }
        if (nowDeshCoolTime >= 0) nowDeshCoolTime -= Time.deltaTime;
    }
    void Ply_Att()
    {
        //if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle") || !ani.GetCurrentAnimatorStateInfo(0).IsName("Run") || !ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump_00")) return;
        if (!OnStory && Input.GetKeyDown(KeyCode.A) && DontAttTime <= 0 && !ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down01") && !ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down02"))
        {
            if (down && nowAttTime <= 0 && !DontMove)
            {
                DontMove = true;
                ani.SetInteger("State", 4);
                nowAttTime = AttCoolTime;
                ani.SetFloat("AttSpeed", AttSpeed);
                transform.GetChild(0).GetChild(0).GetComponent<Att>().AttArrow = transform.GetChild(0).localScale.x == 1 ? 4 : 3;
                transform.GetChild(0).GetChild(1).GetComponent<Att>().AttArrow = transform.GetChild(0).localScale.x == 1 ? 4 : 3;
            }
            else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            {
                ani.SetInteger("State", 5);
            }
            else if (!down && airAtt)
            {
                DontMove = true;
                airAtt = false;
                ani.SetFloat("AttSpeed", AttSpeed);
                ani.SetInteger("State", 4);
                nowAttTime = AttCoolTime;
            }
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= .95f && ani.GetInteger("State") != 5)
        {
            DontMove = false;
        }
        if (nowAttTime > 0) nowAttTime -= Time.deltaTime;
        if (DontAttTime > 0) DontAttTime -= Time.deltaTime;
    }
    public Rigidbody2D TrainNow;
    /// <summary>
    /// 애니메이션 행동
    /// </summary>
    void AniMove()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Air_Att_1"))
        {
            rig.velocity = Vector2.zero;
            rig.gravityScale = 0;
            return;
        }
        else if(ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_2") 
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down01") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down02"))
        {
            if (TrainNow != null)
            {
                rig.velocity = TrainNow.velocity;
            }
            else
            {
                rig.velocity = Vector2.zero;
                rig.gravityScale = 1;
            }
            return;
        }
        else
        {
            if (rig.mass == 1) rig.gravityScale = gravityScale;
            else rig.gravityScale = WatergravityScale;
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle"))
        {
            if (TrainNow != null)
            {
                rig.velocity = TrainNow.velocity;
            }
            else
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
                rig.gravityScale = 1;
            }
            
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            ani.SetFloat("RunSpeed", MaxSpeed * 1.2f);
            rig.velocity = new Vector2(MaxSpeed * PlyLook, rig.velocity.y);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump") && !DontKeyStayMove) 
        {
            if ((OnStory && (Story_RightMove || Story_LeftMove)) || (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))) 
            {
                ani.SetFloat("RunSpeed", MaxSpeed * 1.2f);
                rig.velocity = new Vector2(MaxSpeed * PlyLook, rig.velocity.y);
            }
            else
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
            }
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Desh01"))
        {
            //rig.constraints = RigidbodyConstraints2D.FreezePositionY;
            rig.velocity = new Vector2(DeshSpeedd * PlyLook, 0);
            rig.gravityScale = 0;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Desh02"))
        {
            DontMove = false;
            //rig.constraints = RigidbodyConstraints2D.FreezePositionY;
            rig.velocity = new Vector2(-DeshSpeedd * PlyLook, 0);
            rig.gravityScale = 0;
            if (down)
            {
                ani.SetInteger("State", 0);
            }
            else
            {
                ani.SetInteger("State", 2);
            }
        }
       
    }
    public void DieDie()
    {
        if (Hp <= 0)
        {
            GameSystem.instance.GiveMoneyBag(true);
            saveMoneyInt[0] = Money;
            saveMoneyInt[1] = 0;
            PlySave(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
    //void NpcCheck()
    //{
    //    Debug.DrawRay(rig.position, dirVec * 0.7f, new Color(0, 1, 0));
        
    //    RaycastHit2D rayHit = Physics2D.Raycast(rig.position, dirVec, 0.7f, LayerMask.GetMask("ObjLayer"));
    //    if (rayHit.collider != null)
    //    {
    //        scanObject = rayHit.collider.gameObject;
    //    }
    //    else
    //    {
    //        scanObject = null;
    //    }
        
    //}
    void InputTest()
    {
        if (!DontMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dirVec = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                dirVec = Vector3.left;
            }
        }
        if (!OnStory && Input.GetKeyDown(KeyCode.G) && NowObj != null)
        {
            //manager.Action(scanObject);
            NowObj.NPCOpen(0, true);
        }
    }

    int Tcode;
    bool StopStone = false;
    float StopStoneTime = 0;

    Transform[] ShootStone;//던져진 스톤의 총알
    int ShootStoneNum = 0;//던져진 스톤 총알 넘버
    void Ply_Throw()
    {
        if (!OnStory && Input.GetKeyDown(KeyCode.S))
        {
            if (!Handani.GetCurrentAnimatorStateInfo(0).IsName("Hand_Att") && ThrowStone == null && HaveStone[NowChoose] > 0)
            {
                StopStone = true;
                Handani.SetTrigger("Att");
                Tcode = HaveStone[NowChoose] / 1000;
                --HaveStone[NowChoose];
                if (HaveStone[NowChoose] % 1000 == 0) HaveStone[NowChoose] = 0;
                if (NowChoose == 0) Invoke("REStone", BaseStoneCoolTime);
                StoneUI();
                StopStoneTime = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopStone = false;
        }
        if (StopStone)
        {
            StopStoneTime += Time.deltaTime;
        }
        if (StopStone && StopStoneTime>.5f && !Handani.GetCurrentAnimatorStateInfo(0).IsName("Hand_Att") && ThrowStone != null)
        {
            ThrowStone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ThrowStone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(ThrowStone.gameObject, ThrowStone.GetComponent<StoneDieAni>().SpDesTime);
            ThrowStone = null;
            StopStone = false;
        }
        else if(Hand.GetChild(0).childCount==0 && !StopStone)
        {
            ThrowStone = null;
        }
       
        if (!OnStory && Input.GetKeyDown(KeyCode.E))
        {
            NowChoose++;
            if (HaveStone.Length <= NowChoose) NowChoose = 0;
        }
    }
  
    void REStone()
    {
        HaveStone[0] = 1;
        StoneUI();
    }
    public void MakeStone()
    {
        ThrowStone = Instantiate(GameSystem.instance.AllSton[Tcode]).transform;

        if (ShootStone == null) ShootStone = new Transform[100];
        if (Tcode == 4)
        {
            ShootStone[ShootStoneNum++] = ThrowStone.GetChild(0).GetChild(0);
        }
        else if (Tcode == 5)
        {
            ShootStone[ShootStoneNum++] = ThrowStone.GetChild(0).GetChild(0);
            ShootStone[ShootStoneNum++] = ThrowStone.GetChild(0).GetChild(1);
            ShootStone[ShootStoneNum++] = ThrowStone.GetChild(0).GetChild(2);
            ShootStone[ShootStoneNum++] = ThrowStone.GetChild(0).GetChild(3);
        }
        if (ThrowStone.GetComponent<StoneDieAni>() != null) ThrowStone.GetComponent<StoneDieAni>().DieSet = true;
        ThrowStone.GetComponent<Animator>().SetInteger("Set", 1);
        ThrowStone.position = Hand.GetChild(0).position + new Vector3(0, 0.5f, -.1f);
        ThrowStone.parent = Hand.GetChild(0);
        ThrowStone.GetComponent<Rigidbody2D>().gravityScale = 0;
        if (ThrowStone.GetComponent<Att>() != null && ThrowStone.GetComponent<Att>().SelfDesTime != 0) ThrowStone.GetComponent<Att>().SelfDie();
    }

    public void TStone()
    {
        ThrowStone.parent = GameObject.Find("NowMapEnemy").transform;
        ThrowStone.gameObject.layer = 8;
        ThrowStone.GetComponent<Att>().Set = true;
        ThrowStone.GetComponent<Att>().GroundDes = true;
        ThrowStone.GetComponent<Rigidbody2D>().velocity = new Vector2(PlyLook * ThrowStone.GetComponent<StoneDieAni>().ThrowSpeed, 0);

        ThrowStone.gameObject.AddComponent<StoneThrow>().Speed = ThrowStone.GetComponent<StoneDieAni>().ThrowSpeed;

        ThrowStone.GetComponent<Rigidbody2D>().freezeRotation = false;
        ThrowStone.GetComponent<Rigidbody2D>().angularVelocity = ThrowStone.GetComponent<StoneDieAni>().RollSpeed * PlyLook;

        int codes = ThrowStone.GetComponent<StoneDieAni>().Code;
        if (codes == 3)
        {
            ThrowStone.GetComponent<Att>().GroundDes = false;
            ThrowStone.GetComponent<Att>().HitNum = 2;
            ThrowStone.GetComponent<Att>().HitDesT = true;
        }


    }

    void StoneUI()
    {
        for (int i = 0; i < HaveStone.Length; i++)
        {
            if (StoneUITr.GetChild(0).childCount <= i)
            {
                Instantiate(StoneUITr.GetChild(0).GetChild(1), StoneUITr.GetChild(0));
            }
            StoneUITr.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = GameSystem.instance.AllSton[HaveStone[i] / 1000].GetComponent<StoneDieAni>().StonImg;
            if (HaveStone[i] == 0)
            {
                StoneUITr.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                StoneUITr.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
            else
            {
                StoneUITr.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                StoneUITr.GetChild(0).GetChild(i).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = (HaveStone[i] % 1000) + "";
            }
        }
    }
    void HPUI()
    {
        for(int i = HPUITr.childCount; i < MaxHP; i++)
        {
            Instantiate(HPUITr.GetChild(0), HPUITr);
        }
        for(int i = 0; i < MaxHP; i++)
        {
            if (i < Hp) HPUITr.GetChild(i).GetComponent<Animator>().SetInteger("State", 1);
            else HPUITr.GetChild(i).GetComponent<Animator>().SetInteger("State", 0);
        }
    }

    public void PutStonePly(Collider2D collision = null)
    {
        if (rig.velocity.y < 0)
        {
            DontKeyStayMove = true;
            rig.velocity = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y+.5f).normalized * JumpPower;
        }
    }
    public void SSSS()
    {
        rig.velocity = Vector2.zero;
        DontMove = true;
    }
    float NowJumpPower;
    public void JumpU()
    {
        if (rig.mass == 1) rig.velocity = new Vector2(rig.velocity.x, NowJumpPower);
        else rig.velocity = new Vector2(rig.velocity.x, NowJumpPower *2/ 3);
    }
    void DontMoveSet(int dd)
    {
        if (dd == 0) DontMove = false;
        else if (dd == 1) DontMove = true;
    }
    void SetAniState(int aa)
    {
        ani.SetInteger("State", aa);
    }
    public void EndMyHit()
    {
        DontMove = false;
        DontKeyStayMove = true;
        if (down) ani.SetInteger("State", 1);
        else
        {
            ani.SetInteger("State", 2);
        }
    }

    public void EndDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//죽으면 씬 다시로드
    }
}
