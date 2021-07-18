using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("퀘스트 테스트", new int[] { 1000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId;
    }
}
