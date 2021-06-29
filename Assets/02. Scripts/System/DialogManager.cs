using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text TalkText;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction) //Exit Action
        {
            isAction = false;
            talkPanel.SetActive(false);
        }
        else //Enter Action
        {
            isAction = true;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            TalkText.text = "대화문 테스트";
        }
        talkPanel.SetActive(isAction);
    }
}
