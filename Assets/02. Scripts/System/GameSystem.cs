using DataInfo;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource aus;
    public AudioClip[] BGClip;
    public void Sond(int i)
    {
        if (aus == null) aus = GetComponent<AudioSource>();
        if (BGClip[i] == null)
        {
            aus.Stop();
            return;
        }
        aus.clip = BGClip[i];
        aus.Play();
    }
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
    public GameData gameData;

    public SoundCont[] alls;
    public void ChangeS()
    {
        for (int i = 0; i < alls.Length; i++)
        {
            if (alls[i] != null) alls[i].ChangeSound();
        }
    }
    public void AddSound(SoundCont aus)
    {
        for (int i = 0; i < alls.Length; i++)
        {
            if (alls[i] == null)
            {
                alls[i] = aus;
                break;
            }
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();//바이러니 포맷을위해생성
        FileStream file = File.Create(dataPath);//데이터 저장을 위한 파일 생성

        bf.Serialize(file, gameData);
        file.Close();
        //Save1();
    }
    private void Start()
    {
        CanversUI = GameObject.Find("CanvasUUU").transform;
        ChangeS();
        Invoke("ChangeS", .1f);
        Invoke("ChangeS", .5f);
        Invoke("ChangeS", 1.0f);
    }
    public SaveMonsetDie[] Monster;
    public void DieMonset(int co)
    {
        MonsterSSS();
        //Debug.Log(co);
        gameData.Dest[co] = 1;
        Save();
    }
    public void StorySave(int code)
    {
        //Debug.Log(code);
        gameData.Story[code] = true;
        Save();
    }
    public GameData Load()
    {

        if (File.Exists(dataPath)) 
        {
            //파일존재하면데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            //데이터의 기록
            gameData = (GameData)bf.Deserialize(file);
            file.Close();


            for (int i = 0; i < MapObjS.Length; i++)
            {
                if (gameData.MapObj[i] != 0) MapObjS[i].MapTrue();
            }
            for (int i = 0; i < SavePos.Length; i++)
            {
                if (gameData.AllSavePoint[i] != 0) SavePos[i].NowState = gameData.AllSavePoint[i];
                //Debug.Log(i + "   " + gameData.AllSavePoint[i]);
            }
            for (int i = StoryTr.childCount - 1; i >= 0; i--)
            {
                if (gameData.Story == null) { gameData.Story = new bool[1000]; Save(); }
                if (gameData.Story[i]) Destroy(StoryTr.GetChild(i).gameObject);
            }

            MonsterSSS();


            //Ply.GetComponent<Player>().EndDie();
            Vector3 GG = SavePos[gameData.SavePoint].transform.position;
            Sond(SavePos[gameData.SavePoint].BGSound);
            Ply.position = new Vector3(GG.x, GG.y, Ply.position.z);
            Ply.GetComponent<Player>().Hand.position = new Vector3(GG.x, GG.y, Ply.GetComponent<Player>().Hand.position.z);
            if (SavePos[gameData.SavePoint].MovePos == null)
            {
                SavePos[gameData.SavePoint].MovePos = SavePos[gameData.SavePoint].transform.parent.Find("MoveMap").GetChild(0);
            }
            GG = SavePos[gameData.SavePoint].MovePos.GetChild(1).position;
            Camera.main.transform.position = new Vector3(GG.x, GG.y, Camera.main.transform.position.z);
            Camera.main.GetComponent<CamMove>().XLock = SavePos[gameData.SavePoint].MovePos.parent.parent.GetComponent<MapManager>().XLock;
            Camera.main.GetComponent<CamMove>().YLock = SavePos[gameData.SavePoint].MovePos.parent.parent.GetComponent<MapManager>().YLock;

            return gameData;
        }
        else
        {
            //파일이 없으면 새로생성
            ResetMap();


            return gameData;
        }
        
    }//파일에서 데이터를 추출하는 함수
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
    public void MonsterSSS()
    {
        for (int i = 0; i < Monster.Length; i++)
        {
            if (Monster[i] != null && gameData.Dest[i] != 0) Monster[i].gameObject.SetActive(false);
        }
    }
    public void ResetMap()
    {
        gameData = new GameData();
        gameData.AllSavePoint = new int[1000];
        gameData.AllSavePoint[0] = 1;
        gameData.AllSavePoint[1] = 1;
        gameData.MapObj = new int[1000];
        gameData.Dest = new int[1000];
        gameData.Story = new bool[1000];
        gameData.BGSound = 100;
        gameData.Sound = 100;

    }
    [ContextMenu("게임초기화")]
    public void ResetMap01()
    {
        gameData = new GameData();
        gameData.AllSavePoint = new int[1000];
        gameData.AllSavePoint[0] = 1;
        gameData.AllSavePoint[1] = 1;
        gameData.MapObj = new int[1000];
        gameData.Dest = new int[1000];
        gameData.Story = new bool[1000];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public Vector3 DiePos()
    {
        return SavePos[gameData.SavePoint].transform.position;
    }
    public void SaveSET(int i)
    {
        gameData.AllSavePoint[i] = 1;
        Save();
    }
    public int SaveMeNow(int i)
    {
        return gameData.AllSavePoint[i];
    }
    public void ChangeSave(int ii)
    {
        //for (int i = 0; i < SaveTran.childCount; i++)
        //{
        //    if (gameData.AllSavePoint[i] != 0) gameData.AllSavePoint[i] = 1;
        //}
        Ply.GetComponent<Player>().FullHp();
        gameData.SavePoint = ii;
        Save();
    }
    public void MapSSS(int i)
    {
        gameData.MapObj[i] = 1;
        Save();
    }
    public SaveTrTr[] SavePos;
    public MapSyS[] MapObjS;
    [HideInInspector]
    public List<Hold> AllHold = new List<Hold>();
    public static GameSystem instance = null;
    public Transform Ply;
    public Transform BuddhaHand;
    public Transform CanversUI;
    public Transform StoryTr;
    private void Awake()
    {
        //Time.timeScale = .1f;
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
        //AllHold = new List<Hold>();
        Initialize();
        InitializeSys();
        for (int i = 0; i < Monster.Length; i++)
        {
            if (Monster[i] != null)
            {
                Monster[i].Code = i;
            }
        }
        for (int i = 0; i < SavePos.Length; i++)
        {
            if (SavePos[i] != null)
            {
                SavePos[i].ObjCode = i;
            }
        }
        for (int i = 0; i < MapObjS.Length; i++)
        {
            if (MapObjS[i] != null)
            {
                MapObjS[i].ObjCode = i;
            }
        }
        for (int i = 0; i < StoryTr.childCount; i++)
        {
            StoryTr.GetChild(i).GetComponent<StoryOnOFf>().Codess = i;
        }

        Screen.SetResolution(1920, 1080, true);
        Load();
        //Load1();
        //alls = new List<SoundCont>();
    }
    public int GiveMonster(int i)
    {
        return gameData.Dest[i];
    }
    public int SaveNow()
    {
        return gameData.SavePoint;
    }
    bool StSTop = false;
    public GameObject StopUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            StopMu();
        }
    }
    public void StopMu()
    {
        StSTop = !StSTop;
        StopUI.SetActive(StSTop);
        Ply.GetComponent<Player>().StopGame = StSTop;
        Time.timeScale = StSTop ? 0 : 1;
    }
    public void SetHold(Hold me)
    {
        for (int i = 0; i < AllHold.Count; i++)
        {
            if (AllHold[i] == null)
            {
                AllHold[i] = me;
                return;
            }
        }
        AllHold.Add(me);
    }
    private void OnDisable()
    {
        Save();
    }
}
