using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    string targetMsg;
    Text msgText;
    int index;
    float interval;
    private void Awake()
    {
        msgText = GetComponent<Text>();
    }
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {

    }
}
