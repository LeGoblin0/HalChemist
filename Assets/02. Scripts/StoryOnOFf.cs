using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryOnOFf : MonoBehaviour
{
    [HideInInspector]
    public int Codess = -1;
    [Header("시작할떄 스토리 지연시간")]
    public float DelT = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int NowStory = 0;
    [Header("스토리 반복 여부 세이브하면 소용 없음")]
    public bool Loop = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameSystem.instance.Ply.GetComponent<Player>().OnStory = true;
            GameSystem.instance.Ply.GetComponent<Player>().DontMove = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("OpenStory", DelT);
        }
    }
    void OpenStory()
    {
       //Debug.Log(NowStory + "  " + transform.childCount);
        if (NowStory >= transform.childCount)
        {
            SSEnd();
            return;
        }
        transform.GetChild(NowStory).gameObject.SetActive(true);
    }
    public void EndStory(float DelTime=0)
    {
     
        transform.GetChild(NowStory).gameObject.SetActive(false);
        NowStory++;
        Invoke("OpenStory", DelTime);

    }
    private void OnDestroy()
    {
        
    }
    void SSEnd()
    {

        //스토리 끝
        if (Codess >= 0)
        {
            GameSystem.instance.StorySave(Codess);
            GameSystem.instance.Ply.GetComponent<Player>().OnStory = false;
        }
        if(!Loop) Destroy(gameObject);
        else
        {
            NowStory = 0;
            GetComponent<Collider2D>().enabled = true;
        }
        return;
    }
}
