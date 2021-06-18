using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTxtSay : MonoBehaviour
{
    [System.Serializable]
    public class StoryTT
    {
        public int who;
        [TextArea]
        public string TxtIn;
        public Color color = Color.black;
        public TxtInfo[] Info;
    }
    public StoryTT[] Story;
    int NowStory = 0;
    public Transform[] TxtPos;
    public Transform TxtPre;

    public int StorySkip = 1;
    void Start()
    {
        NextTxt();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NowStory++;
            if (NowStory >= Story.Length)
            {
                Destroy(tt.gameObject);
                for (int i = 0; i < StorySkip; i++)
                {
                    transform.parent.GetComponent<StoryOnOFf>().EndStory();
                }
                return;
            }
            else
            {
                Destroy(tt.gameObject);
                NextTxt();
            }
        }
    }
    Transform tt;
    void NextTxt()
    {
        tt= Instantiate(TxtPre, TxtPos[Story[NowStory].who]);
        tt.GetComponent<SayTxtBox>().TextBox = Story[NowStory].TxtIn;
        tt.GetComponent<SayTxtBox>().InTxt.color = Story[NowStory].color;
        if (Story[NowStory].Info != null)
        {
            tt.GetComponent<SayTxtBox>().TxtInfoData = Story[NowStory].Info;
        }
        tt.GetComponent<SayTxtBox>().StartTiping();
    }
}
