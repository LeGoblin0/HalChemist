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
    BoxCollider2D col;
    public AudioClip[] SoundPly;
    [HideInInspector]
    [Tooltip("0 : 기본 \n1:뛰기\n2:점프")]
    public Animator ani;
    public Animator Handani;


    [Header("핸도 위치")]
    public Transform Hand;

    public void SaveHp()
    {
        Hp = MaxHP;
        HPUI();
    }
    void Start()
    {
        Hp = MaxHP;

        rig = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        cam = Camera.main;
        tag = "Player";
        Handani = Hand.GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        rig.gravityScale = gravityScale;
        HaveStone[0] = 1;
        StoneUI();
        HPUI();
        
        ani.SetFloat("HitThrowTime", HitThrowTime);
    }


    [Header("스토리 실행")]
    public bool OnStory = false;


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
    bool DontKeyStayMove = false;

    void PlySound(int i)
    {
        aus.PlayOneShot(SoundPly[i]);
    }

    [Header("데쉬")]
    public float DeshCoolTime = 2;
    float nowDeshCoolTime = 0;
    public float DeshSpeedd = 5;


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
    [HideInInspector]
    public int NowChoose = 0;
    [Tooltip("스톤 겹치는 수량")]
    public int StackStone = 3;//스톤겹친갯수
    Transform ThrowStone;//던진 스톤 정보 
    [Tooltip("던질파워")]
    public float ThrowPower = 5;
    [Tooltip("던지고 없어지는 시간")]
    public float DesTimeStone = 5;
    public Transform StoneUITr;
    [Tooltip("기본 원석 생성 주기")]
    public float BaseStoneCoolTime = 5;
    [Tooltip("던질때 회전")]
    public float ThrowRollPower = 720;


    [Tooltip("플레이어가 보고 있는 방향 [1: 우 ]  [2: 좌 ]")]
    int PlyLook = 1;


    [Header("시스템")]
    public GameObject PutSton;
    public Transform HPUITr;

    [Header("카메라")]
    public CamSh camsh;

    [Header("대화문")]
    public DialogManager manager;
    GameObject scanObject;

    Vector3 dirVec;
    
    private void FixedUpdate()
    {
        AniMove();
        NpcCheck();

    }

    bool GGGodMod;
    void GGGGG()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    void Update()
    {
        //Debug.Log(SaveTrtr);
        Ply_Move();
        Ply_Desh();
        Ply_Att();

        Ply_Throw();
        InputTest();

        if (Input.GetKey(KeyCode.DownArrow) && SaveTrtr != null) 
        {
            SaveTrtr.SaveOn();
        }

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
    void NpcCheck()
    {
        Debug.DrawRay(rig.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rig.position, dirVec, 0.7f, LayerMask.GetMask("ObjLayer"));
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
       
    }
    void InputTest()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dirVec = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirVec = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.G) && scanObject != null)
        {
            Debug.Log("This is : " + scanObject.name);
            manager.Action(scanObject);
        }
    }

    Vector3 trapsavepoint;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "TrapSavePoint")
        {
            trapsavepoint = transform.position+new Vector3(0,0.2f);
        }
        if (collision.tag == "TrapGround")
        {
            GoTrapSavePoint();
        }
    }

    void GoTrapSavePoint()
    {
        if (Hp > 1)
        {
            GameSystem.instance.CanversUI.GetChild(1).GetComponent<Animator>().SetTrigger("On");
            transform.position = trapsavepoint;
            Hp--; HPUI();
        }
        else { Hp--; HPUI(); }
    }


    public SaveTrTr SaveTrtr;
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Att" && collision.GetComponent<Att>() != null && collision.GetComponent<Att>().Set && nowGodTime <= 0 )
        {
            //            Debug.Log(collision.name);
            Hp -= collision.GetComponent<Att>().AttDamage;
            nowGodTime = GodTime;
            ani.SetTrigger("Hit");
            DontMove = true;
            rig.velocity = new Vector2(ThrowF.x * PlyLook, ThrowF.y);

            camsh.CamMove(.5f);

            HPUI();
            //Debug.Log(rig.velocity);
            //Debug.Log(new Vector2(ThrowF.x * PlyLook, ThrowF.y));

        }
        if (collision.tag == "Ston" && ThrowStone != collision.transform) 
        {
            //Debug.Log(0);
            if (collision.GetComponent<StoneDieAni>() != null)
            {
                int Stonecode = collision.GetComponent<StoneDieAni>().Code;
                for (int i = 1; i < HaveStone.Length; i++)
                {
                    if (HaveStone[i] / 1000 == Stonecode && HaveStone[i] % 1000 < StackStone) //코드가 같으면 최대 스텍갯수가 적으면
                    {
                        HaveStone[i]++;
                        Destroy(collision.gameObject);
                        StoneUI();
                        return;
                    }
                }
                for (int i = 1; i < HaveStone.Length; i++)
                {
                    if (HaveStone[i] == 0) //비어있으면
                    {
                        HaveStone[i] = (Stonecode * 1000 + 1);
                        Destroy(collision.gameObject);
                        StoneUI();
                        return;
                    }
                }
            }
        }

        if (collision.tag == "Item")
        {
            if (collision.GetComponent<ItemCode>().ItemCodeNum == 0)
            {
                if(Money<=9999)
                {
                    Destroy(collision.gameObject);
                    Money++;

                    LookMoney();
                }
            }
            else if (collision.GetComponent<ItemCode>().ItemCodeNum == 1)
            {
                Destroy(collision.gameObject);

                if (Hp < MaxHP) Hp++;
                HPUI();
            }
        }

        if (collision.tag == "Save" && collision.GetComponent<SaveTrTr>() != null) 
        {
            SaveTrtr = collision.GetComponent<SaveTrTr>();
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Save" && SaveTrtr != null)
        {
            SaveTrtr = null;
        }
    }

    public Text MoneyInt;
    public int Money;
    void LookMoney()
    {
        MoneyInt.text = Money + "";
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
            if (Input.GetKey(KeyCode.DownArrow) && down)
            {
                ani.SetInteger("State", 3);
                PutSton.SetActive(true);
                this.Hand.GetComponent<Hand>().offset = new Vector3(-1, 0f, 0);
                col.offset = new Vector2(col.offset.x, 0.2747405f);
                col.size = new Vector2(col.size.x, 0.5542418f);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                col.offset = new Vector2(col.offset.x, 0.4747405f);
                col.size = new Vector2(col.size.x, 0.9542418f);
                PutSton.SetActive(false);
                this.Hand.GetComponent<Hand>().offset = new Vector3(-1, .5f, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = 1;
                DontKeyStayMove = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = -1;
                DontKeyStayMove = false;
            }
            else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                if (down) ani.SetInteger("State", 0);
                else
                {
                    ani.SetInteger("State", 2);
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && down)
            {
                ani.SetBool("RL", true);
                ani.SetInteger("State", 2);
                DontAttTime = .2f;
                Jump01 = false;
                DontMove = true;
            }
            else
            {
                ani.SetBool("RL", false);
            }
            rig.gravityScale = gravityScale;
        }
        else
        {

        }
        transform.GetChild(0).localScale = new Vector3(PlyLook, 1, 1);
    }
    void Ply_Desh()
    {

        if (Input.GetKeyDown(KeyCode.Space) && nowDeshCoolTime <= 0) 
        {
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
        if (Input.GetKeyDown(KeyCode.A) && DontAttTime <= 0)
        {
            if (down && nowAttTime <= 0 && !DontMove)
            {
                DontMove = true;
                ani.SetInteger("State", 4);
                nowAttTime = AttCoolTime;
                ani.SetFloat("AttSpeed", AttSpeed);

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
    /// <summary>
    /// 애니메이션 행동
    /// </summary>
    void AniMove()
    {
          
        
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_2") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Air_Att_1")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down01") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Down02"))
        {
            rig.velocity = Vector2.zero;
            rig.gravityScale = 0;
            return;
        }
        else
        {
            rig.gravityScale = gravityScale;
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle"))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            ani.SetFloat("RunSpeed", MaxSpeed * 1.2f);
            rig.velocity = new Vector2(MaxSpeed * PlyLook, rig.velocity.y);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump") && !DontKeyStayMove) 
        {
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
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

    int Tcode;
    bool StopStone = false;
    float StopStoneTime = 0;
    void Ply_Throw()
    {
        if (Input.GetKeyDown(KeyCode.S))
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
            Destroy(ThrowStone.gameObject, DesTimeStone);
            ThrowStone = null;
            StopStone = false;
        }
        else if(Hand.GetChild(0).childCount==0 && !StopStone)
        {
            ThrowStone = null;
        }
       
        if (Input.GetKeyDown(KeyCode.E))
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
        if (ThrowStone.GetComponent<StoneDieAni>() != null) ThrowStone.GetComponent<StoneDieAni>().DieSet = true;
        ThrowStone.GetComponent<Animator>().SetInteger("Set", 1);
        ThrowStone.position = Hand.GetChild(0).position + new Vector3(0, 0.5f, -.1f);
        ThrowStone.parent = Hand.GetChild(0);
        ThrowStone.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void TStone()
    {
        ThrowStone.parent = GameObject.Find("NowMapEnemy").transform;
        ThrowStone.gameObject.layer = 8;
        ThrowStone.GetComponent<Att>().Set = true;
        ThrowStone.GetComponent<Att>().GroundDes = true;
        ThrowStone.GetComponent<Rigidbody2D>().velocity = new Vector2(PlyLook * ThrowPower, 0);
        ThrowStone.GetComponent<Rigidbody2D>().freezeRotation = false;
        ThrowStone.GetComponent<Rigidbody2D>().angularVelocity = ThrowRollPower * PlyLook;

        int codes = ThrowStone.GetComponent<StoneDieAni>().Code;
        if (codes == 3)
        {
            ThrowStone.GetComponent<Att>().GroundDes = false;
            ThrowStone.GetComponent<Att>().HitNum = 2;
            ThrowStone.GetComponent<Att>().HitDesT = true;
        }
        else
        {
            Destroy(ThrowStone.gameObject, DesTimeStone);
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
            if (i < Hp) HPUITr.GetChild(i).GetChild(0).gameObject.SetActive(true);
            else HPUITr.GetChild(i).GetChild(0).gameObject.SetActive(false);
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
    public void JumpU()
    {
        rig.velocity = new Vector2(rig.velocity.x, JumpPower);
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
