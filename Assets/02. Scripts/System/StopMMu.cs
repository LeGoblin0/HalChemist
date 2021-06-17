using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopMMu : MonoBehaviour
{
    int num = 0;
    int MaxNum = -1;


    bool set = true;
    Text[] MMMu;
    private void OnEnable()
    {
        if (set)
        {
            set = false;
            MaxNum = transform.GetChild(0).childCount;
            MMMu = new Text[MaxNum];
            for (int i = 0; i < MaxNum; i++)
            {
                MMMu[i] = transform.GetChild(0).GetChild(i).GetComponent<Text>();
            }
            MMMu[1].text = "배경음악 " + (GameSystem.instance.gameData.BGSound) + "%";
            MMMu[2].text = "효과음 " + (GameSystem.instance.gameData.Sound) + "%";
        }
        DrowMu();
        //num = 0;
    }
    private void OnDisable()
    {
        GameSystem.instance.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            num++;
            if (num >= MaxNum) num = 0;
            DrowMu();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            num--;
            if (num < 0) num = MaxNum - 1;
            DrowMu();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (num == 1)
            {
                GameSystem.instance.gameData.BGSound -= 10;
                if (GameSystem.instance.gameData.BGSound < 0) GameSystem.instance.gameData.BGSound = 0;
                MMMu[1].text = "배경음악 " + (GameSystem.instance.gameData.BGSound) + "%";
            }
            else if (num == 2)
            {
                GameSystem.instance.gameData.Sound-=10;
                if (GameSystem.instance.gameData.Sound < 0) GameSystem.instance.gameData.Sound = 0;
                MMMu[2].text = "효과음 " + (GameSystem.instance.gameData.Sound) + "%";
            }
            GameSystem.instance.ChangeS();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (num == 1)
            {
                GameSystem.instance.gameData.BGSound += 10;
                if (GameSystem.instance.gameData.BGSound > 100) GameSystem.instance.gameData.BGSound = 100;
                MMMu[1].text = "배경음악 " + (GameSystem.instance.gameData.BGSound) + "%";
            }
            else if (num == 2)
            {
                GameSystem.instance.gameData.Sound+=10;
                if (GameSystem.instance.gameData.Sound > 100) GameSystem.instance.gameData.Sound = 100;
                MMMu[2].text = "효과음 " + (GameSystem.instance.gameData.Sound) + "%";
            }
            GameSystem.instance.ChangeS();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (num == 0) GameSystem.instance.StopMu();
            else if (num == 3)
            {
                Time.timeScale = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMamu");
            }
        }
    }
    

    void DrowMu()
    {
        for (int i = 0; i < MaxNum; i++)
        {
            MMMu[i].color = Color.gray;
        }
        MMMu[num].color = Color.white;
    }
}
