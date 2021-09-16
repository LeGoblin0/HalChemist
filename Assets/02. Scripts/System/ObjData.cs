using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    [HideInInspector]
    public int id=-1;
    [Header("NPCStoryCode")]
    public int NowStoryNum = 0;
    private void Start()
    {
        gameObject.tag = "NPCObj";
        gameObject.layer = 12;
        GetComponent<Collider2D>().isTrigger = true;
    }
    /// <summary>
    /// 플레이어가 G를 누르면 실행하게 되는 함수
    /// </summary>
    /// <returns></returns>
    public bool NPCOpen(int a=-1,bool First=false)
    {
        if (a == -1)
        {
            a = NowStoryNum;
            transform.GetChild(a).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (!First && a == 0) EndSay();
        //if (StoryList!=null && 0 < StoryList.Length) StoryList[0].SetActive(true);
        return true;
    }
    /// <summary>
    /// 말이 모두 끝남 반복 X
    /// 또는 특수한 상황에 따라서 다음 대사로 넘어감
    /// </summary>
    public void EndSay(int a = 1)
    {
        NowStoryNum = a;
        if (id != -1)
        {
            Debug.Log(id + "  " + NowStoryNum);
            GameSystem.instance.NPCSaySave(id, NowStoryNum);
        }
    }
}
