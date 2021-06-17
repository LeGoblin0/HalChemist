using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChoose : MonoBehaviour
{
    [System.Serializable]
    public class StoryTT
    {
        [TextArea]
        public string TxtIn;
        public Color color = Color.black;
        public TxtInfo[] Info;
    }
    public StoryTT[] StoryCh;
    int Nowchoose = 0;
    public Transform TxtPos;
    public Transform TxtPre;

    void Start()
    {
        NextTxt();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(tt.gameObject);
            for (int i = 0; i < Nowchoose + 1; i++)
            {
                transform.parent.GetComponent<StoryOnOFf>().EndStory();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Nowchoose--;
            if (Nowchoose < 0) Nowchoose = StoryCh.Length - 1;
            NextTxt();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Nowchoose++;
            if (Nowchoose >= StoryCh.Length) Nowchoose = 0;
            NextTxt();
        }
    }
    void NextTxt()
    {
        if (tt != null) Destroy(tt.gameObject);
        tt = Instantiate(TxtPre, TxtPos);
        tt.GetComponent<SayTxtBox>().TextBox = StoryCh[Nowchoose].TxtIn;
        tt.GetComponent<SayTxtBox>().SetOnArr();
        tt.GetComponent<SayTxtBox>().InTxt.color = StoryCh[Nowchoose].color;
        if (StoryCh[Nowchoose].Info != null)
        {
            tt.GetComponent<SayTxtBox>().TxtInfoData = StoryCh[Nowchoose].Info;
        }
        tt.GetComponent<SayTxtBox>().StartTiping();
    }
    Transform tt;
}
