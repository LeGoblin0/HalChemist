using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("박스걸과 대화하기", new int[] { 1000}));
        questList.Add(20, new QuestData("열쇠를 주워다 주기", new int[] { 2000,1000 }));
        questList.Add(30, new QuestData("퀘스트 완료", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
        ControlObject();
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    void ControlObject()
    {
        switch (questId)
        {
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
