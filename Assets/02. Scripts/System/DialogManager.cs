using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TalkManager talkManager;
    public TypeEffect talk;
    public GameObject scanObject;
    public GameObject talkPanel;
    public Text talkText;
    public bool isAction;
    public int talkIndex;

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
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNpc);
            //talk.SetMsg("이 오브젝트는" + scanObject.name + "입니다.");

        }
        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (isNpc)
        {
            talkText.text = talkData;
        }

        else
        {
            talkText.text = talkData;
        }
    }
}
