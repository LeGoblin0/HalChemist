using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TxtInfo
{
    public int TxtNum;
    public Color TxtCol = Color.black;
    public int TxtSize = 120;
}
public class SayTxtBox : MonoBehaviour
{
    public string TextBox;
    public Text InTxt;
    public TxtInfo[] TxtInfoData;

    public bool StartText = false;
    public float DesTime = 0;

    private void Start()
    {
        transform.parent = GameObject.Find("SayCanv").transform;
        transform.localScale = new Vector3(1, 1, 1);
        if (StartText) StartTiping();
        
    }
    public GameObject[] Arr;
    public void SetOnArr(bool a = true)
    {
        Arr[0].SetActive(a);
        Arr[1].SetActive(a);
    }
    public void StartTiping()
    {
        StartCoroutine(TTipin());
    }


    int nowT = 0;
    bool SSS = false;
    int i;
    IEnumerator TTipin()
    {
        while (nowT< TextBox.Length)
        {
            SSS = false;                   // Debug.Log(TxtInfoData==null);
            if (TxtInfoData != null)
            {
                for (i = 0; i < TxtInfoData.Length; i++)
                {

                    if (nowT == TxtInfoData[i].TxtNum)
                    {
                        SSS = true;
                        break;
                    }
                }
            }
            if (SSS)
            {
                InTxt.text += "<size="+ TxtInfoData[i].TxtSize+">"+ Change_String_Color(TextBox.Substring(nowT, 1), TxtInfoData[i].TxtCol)+"</size>";
            }
            else InTxt.text += TextBox.Substring(nowT, 1);

            nowT++;
            yield return new WaitForSeconds(.05f);
        }
        if (DesTime > 0) Destroy(gameObject, DesTime);
    }
    public static string Change_String_Color(string ThisString, Color ThisColor)
    {
        string TextToReturn = "<color=#[Color_Code]>[Insert_HERE]</color>";
        TextToReturn = TextToReturn.Replace("[Color_Code]", UnityEngine.ColorUtility.ToHtmlStringRGBA(ThisColor));
        TextToReturn = TextToReturn.Replace("[Insert_HERE]", ThisString);
        return TextToReturn;
    }

    public static string Change_String_Color_ViaCode(string ThisString, string ColorCode)
    {
        string TextToReturn = "<color=[Color_Code]>[Insert_HERE]</color>";
        TextToReturn = TextToReturn.Replace("[Color_Code]", ColorCode);
        TextToReturn = TextToReturn.Replace("[Insert_HERE]", ThisString);
        return TextToReturn;
    }
}
