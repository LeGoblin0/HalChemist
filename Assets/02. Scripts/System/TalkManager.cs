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
        //Talk Data
        //BoxGirl 1000
        //Key 100
        talkData.Add(1000, new string[] {"택배는 역시 로켓 배송이야", "택배 도착했을때가 제일 짜릿해" });
        talkData.Add(2000, new string[] {"수상하게 생긴 열쇠다."});
        talkData.Add(3000, new string[] { "뭐야 넌. 못 보던 얼굴이잖아?", "별 볼일 없는 이 곳에 온 걸 보니 너도 한량이구나?",
        "아무렴 어때. 지금 세상에 바쁜 건 꼴 보기 싫은 장사치들 뿐인걸.","사람이 말야 장사같은 거 말고 \"영웅\" 트루스처럼 당당하고 멋지게 살아야지 말야.",
        "\"영웅\" 이 누구냐고? 응? 눈 앞에 두고도 모르겠어?","레인보우 터미널에서 사악한 골렘 무리들을 몰아내고 천사들의 평화를 지키고 있는 이 몸이시지!",
        "... 이봐 벌써 갈려는 거야? 얘기 좀 더 듣고 가!"});
        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "안녕? 난 박스걸이야", "내가 박스안에 갇혀버려서 부탁하나만 해도 될까?","저 쪽에 있는 열쇠좀 주워줄래?" });
        talkData.Add(11 + 2000, new string[] { "수상한 열쇠를 주웠다."});
        talkData.Add(20 + 1000, new string[] { "열쇠 가져다 줘서 고마워!" });

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                 return GetTalk(id - id % 10, talkIndex);
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
