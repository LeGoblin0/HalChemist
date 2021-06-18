using DataInfo;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainMu : MonoBehaviour
{
    public int Choose = 0;

    public class SystemSave
    {
        public int BGSound = 100;
        public int Sound = 100;
    }

    public SystemSave gamesystemdata;

    private string dataPathSys;//파일저장위치
    public void InitializeSys()    // 저장경로 파일명
    {
        if (!Directory.Exists(Application.persistentDataPath + "SavesDir/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "SavesDir/");
        }
        dataPathSys = Application.persistentDataPath + "SavesDir/Info.Sy";
        Debug.Log(Application.persistentDataPath);
        //Debug.Log(dataPath);
    }
    private void Start()
    {
        //InitializeSys();
        //Load1();
    }
    public SystemSave Load1()
    {
        if (File.Exists(dataPathSys))
        {
            //파일존재하면데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPathSys, FileMode.Open);
            //데이터의 기록
            gamesystemdata = (SystemSave)bf.Deserialize(file);
            file.Close();

            return gamesystemdata;
        }
        else
        {
            gamesystemdata = new SystemSave();
            gamesystemdata.BGSound = 100;
            gamesystemdata.Sound = 100;

            return gamesystemdata;
        }

    }//파일에서 데이터를 추출하는 함수
    public void Save1()
    {
        BinaryFormatter bf = new BinaryFormatter();//바이러니 포맷을위해생성
        FileStream file = File.Create(dataPathSys);//데이터 저장을 위한 파일 생성

        bf.Serialize(file, gamesystemdata);
        file.Close();
    }
    public bool Setting = false;
    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == Choose) transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
            else transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Choose--;
            if (Choose < 0) Choose = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Choose++;
            if (Choose >= transform.childCount) Choose = transform.childCount - 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            int f = 0;
            if (Choose == f++) PlayNext();
            else if (Choose == f++) UnityEngine.SceneManagement.SceneManager.LoadScene("Stage01");
            
            else if (Choose == f++) Application.Quit();
        }
    }



    GameData gameData; 
    private string dataPath;//파일저장위치
    public void Initialize()// 저장경로 파일명
    {
        if (!Directory.Exists(Application.persistentDataPath + "SavesDir/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "SavesDir/");
        }
        dataPath = Application.persistentDataPath + "SavesDir/Info.GD";
        Debug.Log(Application.persistentDataPath);
        //Debug.Log(dataPath);
    }
    void PlayNext()
    {
        Initialize();
        gameData = new GameData();
        gameData.AllSavePoint = new int[1000]; gameData.AllSavePoint[0] = 1;
        gameData.AllSavePoint = new int[1000]; gameData.AllSavePoint[1] = 1;
        gameData.MapObj = new int[1000];
        gameData.Dest = new int[1000];

        gameData.BGSound = 100;
        gameData.Sound = 100;

        BinaryFormatter bf = new BinaryFormatter();//바이러니 포맷을위해생성
        FileStream file = File.Create(dataPath);//데이터 저장을 위한 파일 생성

        bf.Serialize(file, gameData);
        file.Close();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage01");
    }
}
