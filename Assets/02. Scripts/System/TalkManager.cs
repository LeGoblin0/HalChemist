using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "1번 대화문,대화문대화문대화문", "2번 대화문 ,테스트를 계속" });

        talkData.Add(10 + 1000, new string[] { "퀘스트 대화문 테스트", "퀘스트 대화문 테스트 2" });

    }
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
