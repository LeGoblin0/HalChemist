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


    [Header("이동 속도")]
    public float MaxSpeed = 1.8f;
    void PlySound(int i)
    {
        aus.PlayOneShot(SoundPly[i]);
    }

    [Header("공격 속도")]
    public float AttSpeed = 1.8f;

    [Tooltip("평타 쿨남은시간")]
    float nowAttTime = 0;

    [Tooltip("플레이어가 보고 있는 방향 [1: 우 ]  [2: 좌 ]")]
    int PlyLook = 1;
    private void FixedUpdate()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ani.SetInteger("State", 1);
            PlyLook = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(0);
            ani.SetInteger("State", 1);
            PlyLook = -1;
        }
        else if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            ani.SetInteger("State", 0);
        }
        transform.GetChild(0).localScale = new Vector3(PlyLook, 1, 1);
        AniMove();

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
