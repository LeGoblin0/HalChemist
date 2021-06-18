using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCont2 : MonoBehaviour
{
    public int aniState = 0;

    [Header("특수한 애니메이션 실행")]
    public bool StoryAni = false;
    public string ParametersName = "";

    [Header("플레이어 다음 애니메이션으로 넘어감 0보다 작으면 시간 무제한")]
    public float EndStoryTime = -1;
    public GameObject Hando;
    public GameObject Effect;
    public Animator ani;
    public bool BuddhaHand = true;
    void Start()
    {
        // if (plyTr == null) plyTr = GameSystem.instance.Ply;
        //if (ply == null) ply = plyTr.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BuddhaHand)
        {

        }
    }
    public bool MovePlayer = true;
    public void EndS()
    {
        // if (plyTr == null) plyTr = GameSystem.instance.Ply;
        //if (ply == null) ply = plyTr.GetComponent<Player>();
        transform.parent.GetComponent<StoryOnOFf>().EndStory();
    }
    public void HandoOn()
    {
        Hando.gameObject.SetActive(true);
    }
    public void HandoOff()
    {
        Hando.gameObject.SetActive(false);
    }
    
    public void BuddhaHandOff()
    {
        BuddhaHand = false;
    }
    public void EffectOn()
    {
        Effect.gameObject.SetActive(true);
    }
    public void EffectOff()
    {
        Effect.gameObject.SetActive(false);
    }
    public void HandoDifficultyOn()
    {
        ani.SetInteger("State", 1);
    }
    public void HandoDifficultyOff()
    {
        ani.SetInteger("State", 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (plyTr == null) plyTr = GameSystem.instance.Ply;
            //if (ply == null) ply = plyTr.GetComponent<Player>();

            if (StoryAni)
            {
                ani.SetTrigger(ParametersName);
                ani.SetInteger("State", aniState);
            }
            else
            {
                ani.SetInteger("State", aniState);
            }

            if (EndStoryTime > 0)
            {
                Invoke("EndS", EndStoryTime);

            }
        }
    }
}
