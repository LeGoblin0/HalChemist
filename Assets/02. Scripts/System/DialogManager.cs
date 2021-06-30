using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TypeEffect talk;
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
            talk.SetMsg("대화문 테스트를 길게길게 써야지 왼쪽에서부터 텍스트 뜨는 속도를 조정하지");
        }
        talkPanel.SetActive(isAction);
    }
}
