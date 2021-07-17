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
    public bool isAction;
    public int talkIndex;
    public Text talkText;
    public void Action(GameObject scanObj)
    {
        talkPanel.SetActive(true);
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null)
        {
            talkPanel.SetActive(false);
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);
        }
        else
        {
            talk.SetMsg(talkData);
        }
        isAction = true;
        talkPanel.SetActive(false);
        talkIndex++;


    }
}
