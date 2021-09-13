using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOnOff : MonoBehaviour
{
    public GameObject Target;
    [Space]
    [Header("==================================")]
    [Header("스토리로 활성화")]
    public int StoryNum_On = -1;
    [Header("엔피시 대화 상태로 활성화")]
    public int NPCsaNum_On = -1;
    public int NPCsaNum_On_Num = -1;
    [Header("맵오브젝트 세이브로 활성화")]
    public int ObjNum_On = -1;
    [Header("몬스터 세이브로 활성화")]
    public int EmenyNum_On = -1;
    [Header("스토리로 활성화")]
    [Space]
    [Header("==================================")]
    public int StoryNum_Off = -1;
    [Header("엔피시 대화 상태로 활성화")]
    public int NPCsaNum_Off = -1;
    public int NPCsaNum_Off_Num = -1;

    [Header("맵오브젝트 세이브로 활성화")]
    public int ObjNum_Off = -1;
    [Header("몬스터 세이브로 활성화")]
    public int EmenyNum_Off = -1;


    bool Off_No = false;
    private void Start()
    {

        if (EmenyNum_Off == -1 && ObjNum_Off == -1 && NPCsaNum_Off == -1 && StoryNum_Off == -1) Off_No = true;
    }
    void Update()
    {
        //Debug.Log(GameSystem.instance.GiveMonster(EmenyNum_On));
        if (EmenyNum_Off != -1 && GameSystem.instance.GiveMonster(EmenyNum_Off) != 0)
        {
            Target.SetActive(false);
            Destroy(this);
        }
        else if (ObjNum_Off != -1 && GameSystem.instance.MapSSS(ObjNum_Off) != 0)
        {
            Target.SetActive(false);
            Destroy(this);
        }
        else if (NPCsaNum_Off != -1 && GameSystem.instance.GiveNPCSayNum(NPCsaNum_Off) == NPCsaNum_Off_Num)
        {
            Target.SetActive(false);
            Destroy(this);
        }
        else if (ObjNum_Off != -1 && GameSystem.instance.GiveStory(ObjNum_Off))
        {
            Target.SetActive(false);
            Destroy(this);
        }

        //위 비활성화 
        //아래 활성화

        else if (EmenyNum_On != -1 && GameSystem.instance.GiveMonster(EmenyNum_On) != 0)
        {
            Target.SetActive(true);
            if (Off_No) Destroy(this);
        }
        else if (ObjNum_On != -1 && GameSystem.instance.MapSSS(ObjNum_On) != 0)
        {
            Target.SetActive(true);
            if (Off_No) Destroy(this);
        }
        else if (NPCsaNum_On != -1 && GameSystem.instance.GiveNPCSayNum(NPCsaNum_On) == NPCsaNum_On_Num)
        {
            Target.SetActive(true);
            if (Off_No) Destroy(this);
        }
        else if (ObjNum_On != -1 && GameSystem.instance.GiveStory(ObjNum_On))
        {
            Target.SetActive(true);
            if (Off_No) Destroy(this);
        }
    }
}
