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
    [HideInInspector]
    [Tooltip("0 : 기본 \n1:뛰기\n2:점프")]
    public Animator ani;


    [Header("핸도 위치")]
    public Transform Hand;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        cam = Camera.main;
        tag = "Player";

        rig.gravityScale = gravityScale;
    }


    [Header("움직임제한")]
    public bool DontMove = false;
    [Header("무적모드")]
    public bool GodMove = false;
    [Header("스토리 실행")]
    public bool OnStory = false;


    [Header("센서")]
    public bool right = false;
    public bool left = false;
    public bool down = false;
    public bool airAtt = false;
    public bool Jump01 = false;


    [Header("이동 속도")]
    public float MaxSpeed = 1.8f;
    [Header("점프 파워")]
    public float JumpPower = 3;
    [Header("낙하 파워")]
    public float gravityScale = 2;
    void PlySound(int i)
    {
        aus.PlayOneShot(SoundPly[i]);
    }

    [Header("공격 속도")]
    public float AttSpeed = 1.8f;
    [Header("공격 쿨타임")]
    public float AttCoolTime = 2f;
    [Tooltip("평타 쿨남은시간")]
    float nowAttTime = 0;

    [Tooltip("평타 불가시간")]
    float DontAttTime = 0;


    [Tooltip("플레이어가 보고 있는 방향 [1: 우 ]  [2: 좌 ]")]
    int PlyLook = 1;
    private void FixedUpdate()
    {

    }

    void Update()
    {

        if (!DontMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (down) ani.SetInteger("State", 1);
                else
                {
                    ani.SetInteger("State", 2);
                }
                PlyLook = -1;
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
            rig.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.A) && DontAttTime <= 0) 
        {
            if (down && nowAttTime <= 0 && !DontMove) 
            {
                DontMove = true;
                ani.SetInteger("State", 4);
                nowAttTime = AttCoolTime;
                ani.SetFloat("AttSpeed", AttSpeed);
                rig.bodyType = RigidbodyType2D.Static;
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
                rig.bodyType = RigidbodyType2D.Static;
            }
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Ground_Att_1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= .95f && ani.GetInteger("State") != 5)
        {
            DontMove = false;
        }
        if (nowAttTime > 0) nowAttTime -= Time.deltaTime;
        if (DontAttTime > 0) DontAttTime -= Time.deltaTime;


        transform.GetChild(0).localScale = new Vector3(PlyLook, 1, 1);
        AniMove();

    }

    public void STop(int i)
    {
        if(i==0) rig.bodyType = RigidbodyType2D.Dynamic;
        if(i==1) rig.bodyType = RigidbodyType2D.Static;
    }
    public void JumpU()
    {
        rig.bodyType = RigidbodyType2D.Dynamic;
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
    /// <summary>
    /// 애니메이션 행동
    /// </summary>
    void AniMove()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Idle"))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            ani.SetFloat("RunSpeed", MaxSpeed * 1.2f);
            rig.velocity = new Vector2(MaxSpeed * PlyLook, rig.velocity.y);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ply_Jump"))
        {
            if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                ani.SetFloat("RunSpeed", MaxSpeed * 1.2f);
                rig.velocity = new Vector2(MaxSpeed * PlyLook, rig.velocity.y);
            }
            else
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
    }
    public void EndDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//죽으면 씬 다시로드
    }
}
