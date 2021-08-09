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
        /*talkData.Add(3000, new string[] { "뭐야 넌. 못 보던 얼굴이잖아?", "별 볼일 없는 이 곳에 온 걸 보니 너도 한량이구나?",
        "아무렴 어때. 지금 세상에 바쁜 건 꼴 보기 싫은 장사치들 뿐인걸.","사람이 말야 장사같은 거 말고 \"영웅\" 트루스처럼 당당하고 멋지게 살아야지 말야.",
        "\"영웅\" 이 누구냐고? 응? 눈 앞에 두고도 모르겠어?","레인보우 터미널에서 사악한 골렘 무리들을 몰아내고 천사들의 평화를 지키고 있는 이 몸이시지!",
        "... 이봐 벌써 갈려는 거야? 얘기 좀 더 듣고 가!"});*/
        //talkData.Add(30+3000, new string[] { "\"영웅\" 트루스는 오늘도 레인보우 터미널을 지키지.", "골렘들은 내 얼굴만 봐도 꽁지 빠지게 도망간다고." });
        //talkData.Add(30 + 3000, new string[] { "어이 파란 손짝하고 빨강 머리.그 가면 쓴 이방인하고 얘기한거야 ?", "내가 멀리서 봤는데 저 녀석은 뭔가 꺼림직해...", "뭔가... 내 \"영웅\"적 감각이 주의를 보내는 군"});
        //talkData.Add(50 + 3000, new string[] { "\"영웅\" 트루스는 오늘도 골렘, 가면쓴 천사, 미세먼지로 부터 레인보우 터미널을 지키지.", "하하하하!" });

        talkData.Add(4000, new string[] { "당신...골치 덩어리를 처치해주셨군요.", "아, 제 소개부터하자면 저는 원석 브로커\"G\"입니다.", "당신이 거대한 솜뭉치를 치워주신 덕에 거래에 있던 애로사항이 해소됐습니다.",
        "그 점에 보답하는 겸 아래 문 열쇠를 드리죠. 사양하지 마세요.","열쇠를 얻었다!","그리고 제가 보기엔 당신은 이제 막 여정을 떠난 여행자로 보이는군요.","여행자는 언제나 제게 있어서 훌륭한 고객들이었죠.","당신도 저랑 좋은 비즈니스 관계를 맺을 수 있을 것 같군요.",
        "일단 선물을 하나 더 드리도록 할까요?","탱탱원석 티켓을 얻었다!","이 티켓을 메리씨에게 보여주면 원없이 탱탱 원석을 구매할 수 있을 겁니다.","...아? 메리씨를 모르나요?","음, 그녀는 우측 하단으로 내려가면 있는 [예언자의 호수]에 있을 겁니다.",
        "여정에 차질이 안 생긴다면 그 곳부터 가보는 게 좋겠어보이군요.","아무튼 여정을 계속하시다가 저를 만나게되면, 또 다른 원석의 티켓을 거래해드리겠습니다.","이만 저는 물러나도록 하지요. 거래가 많이 밀려있어서요.","무운을 빕니다 고객님."});
        

        talkData.Add(3000, new string[] { "\"영웅\" 트루스는 오늘도 레인보우 터미널을 지키지.", "골렘들은 내 얼굴만 봐도 꽁지 빠지게 도망간다고." });


        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "안녕? 난 박스걸이야", "내가 박스안에 갇혀버려서 부탁하나만 해도 될까?","저 쪽에 있는 열쇠좀 주워줄래?" });
        talkData.Add(10 + 3000, new string[] { "뭐야 넌. 못 보던 얼굴이잖아?", "별 볼일 없는 이 곳에 온 걸 보니 너도 한량이구나?",
        "아무렴 어때. 지금 세상에 바쁜 건 꼴 보기 싫은 장사치들 뿐인걸.","사람이 말야 장사같은 거 말고 \"영웅\" 트루스처럼 당당하고 멋지게 살아야지 말야.",
        "\"영웅\" 이 누구냐고? 응? 눈 앞에 두고도 모르겠어?","레인보우 터미널에서 사악한 골렘 무리들을 몰아내고 천사들의 평화를 지키고 있는 이 몸이시지!",
        "... 이봐 벌써 갈려는 거야? 얘기 좀 더 듣고 가!"});
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
