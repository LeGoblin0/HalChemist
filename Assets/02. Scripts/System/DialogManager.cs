using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public TypeEffect talk;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;
    public int talkIndex;
    public Text talkText;

    public void Start()
    {
        //Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        talkPanel.SetActive(true);
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
       // Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim) {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        //End Talk
        if (talkData == null)
        {
            talkPanel.SetActive(false);
            GameSystem.instance.Ply.GetComponent<Player>().OnStory = false;
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        //Continue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData);
        }
        else
        {
            talk.SetMsg(talkData);
        }
        GameSystem.instance.Ply.GetComponent<Player>().OnStory = true;
        isAction = true;
        talkPanel.SetActive(false);
        talkIndex++;
    }
}
