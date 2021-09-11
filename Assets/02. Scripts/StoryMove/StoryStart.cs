using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStart : MonoBehaviour
{
    int nowStory = 0;
    [HideInInspector]
    public int StoryCode = -1;
    [Header("시작할떄 스토리 지연시간")]
    public float DelT = 0;

    [Header("반복")]
    public bool Loop = false;
    Player ply;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        ply = GameSystem.instance.Ply.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ply.OnStory = true;
            Invoke("NextStory", DelT);
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void NextStory()
    {
        if (transform.childCount <= nowStory)
        {
            //end

            if (nowStory - 1 >= 0) transform.GetChild(nowStory - 1).gameObject.SetActive(false);
            ply.OnStory = false;

            if (StoryCode >= 0) GameSystem.instance.StorySave(StoryCode);


            ply.OnStory = false;


            if (!Loop) gameObject.SetActive(false);
            
            
                nowStory = 0;
            
            GetComponent<Collider2D>().enabled = true;
            return;
        }
        if (nowStory - 1 >= 0) transform.GetChild(nowStory - 1).gameObject.SetActive(false);
        transform.GetChild(nowStory).gameObject.SetActive(true);
        nowStory++;
    }
}
