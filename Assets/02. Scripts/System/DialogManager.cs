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
            talk.SetMsg("이 오브젝트는" + scanObject.name + "입니다.");

        }
        talkPanel.SetActive(isAction);
    }
}
