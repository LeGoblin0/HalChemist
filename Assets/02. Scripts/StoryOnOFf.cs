using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryOnOFf : MonoBehaviour
{
    public int Codess = -1;
    public float EndDTime = .2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int NowStory = 0;
    public bool Loop = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameSystem.instance.Ply.GetComponent<Player>().OnStory = true;
            GameSystem.instance.Ply.GetComponent<Player>().DontMove = false;
            Destroy(GetComponent<Collider2D>());
            OpenStory();
        }
    }
    void OpenStory()
    {
       //Debug.Log(NowStory + "  " + transform.childCount);
        if (NowStory >= transform.childCount)
        {
            Invoke("SSEnd", EndDTime);
            return;
        }
        transform.GetChild(NowStory).gameObject.SetActive(true);
    }
    public void EndStory()
    {
     
        transform.GetChild(NowStory).gameObject.SetActive(false);
        NowStory++;
        OpenStory();

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
        return;
    }
}
