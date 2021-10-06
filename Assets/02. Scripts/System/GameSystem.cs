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
    public StoryStart[] Story;
    public ObjData[] NPC;
    public GameObject TextSay;
    public GameObject StoneButUI;

    public bool PlyAttSlow = true;
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
    public bool GiveMoneyBag()
    {
        return gameData.LostMoneyBag;
    }
    public void GiveMoneyBag(bool a)
    {
        gameData.LostMoneyBag = a;
        Save();
    }
    public int[] MoneyInt()
    {
        return gameData.Money;
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
    public void QuestSave(int questActionIndex)
    {
        gameData.Quest[questActionIndex] = questActionIndex;
        Save();
    }
    public void StorySave(int code)
    {
        //Debug.Log(code);
        gameData.Story[code] = true;
        Save();
    }
    public bool TEST_BOOL = true;
    public void NPCSaySave(int code,int num)
    {
        if (gameData.NpcSayNum == null) gameData.NpcSayNum = new int[1000];
        gameData.NpcSayNum[code] = num;
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
                if (MapObjS[i] != null && gameData.MapObj[i] != 0)
                {
                    //MapObjS[i].SaveOn = true;
                    MapObjS[i].MapTrue();
                }
            }
            for (int i = StoryTr.childCount - 1; i >= 0; i--)
            {
                if (gameData.Story == null) { gameData.Story = new bool[1000]; Save(); }
                if (gameData.Story[i]) StoryTr.GetChild(i).gameObject.SetActive(false);
            }
            if (gameData.NpcSayNum == null) gameData.NpcSayNum = new int[1000];
            if (gameData.StoneMake == null) gameData.StoneMake = new bool[1000];
            if (TEST_BOOL)
            {
                gameData.StoneMake[0] = true;
            }
            for (int i = 0; i < NPC.Length; i++)
            {
                NPC[i].id = i;
                NPC[i].NowStoryNum = gameData.NpcSayNum[i];
            }


            MonsterSSS();
            if (gameData.Money == null) gameData.Money = new int[2];
            //Ply.GetComponent<Player>().EndDie();
            Vector3 GG = SavePos[gameData.SavePoint].transform.position;
            Sond(SavePos[gameData.SavePoint].BGSound);
            Ply.position = new Vector3(GG.x, GG.y, Ply.position.z);
            Ply.GetComponent<Player>().trapsavepoint = new Vector3(GG.x, GG.y, Ply.position.z);
            //Ply.GetComponent<Player>().Hand.position = new Vector3(GG.x, GG.y, Ply.GetComponent<Player>().Hand.position.z);
            if (SavePos[gameData.SavePoint].MovePos == null)
            {
                SavePos[gameData.SavePoint].MovePos = SavePos[gameData.SavePoint].transform.parent.GetChild(0).GetChild(0);
            }
            //GG = SavePos[gameData.SavePoint].MovePos.GetChild(1).position;

            //cam.transform.position = new Vector3(Ply.position.x, Ply.position.y, cam.transform.position.z);
            Invoke("CamsetTr", .1f);
            //Debug.Log(cam.transform.position);
            SavePos[gameData.SavePoint].transform.parent.gameObject.SetActive(true);
            //cam.m_BoundingShape2D = SavePos[gameData.SavePoint].transform.parent.GetChild(0).GetComponent<PolygonCollider2D>();
            

            SavePos[gameData.SavePoint].GetComponent<Animator>().SetTrigger("On");//세이브 폰인트 이미지 설정
            SavePos[gameData.SavePoint].transform.parent.GetComponent<MapManager>().MakeEEE();

            return gameData;
        }
        else
        {
            //파일이 없으면 새로생성
            ResetMap();

            return gameData;
        }
    }//파일에서 데이터를 추출하는 함수
    void CamsetTr()
    {
        cam.transform.position = new Vector3(Ply.position.x, Ply.position.y, cam.transform.position.z);
    }
    public class SystemSave
    {
        public int BGSound = 100;
        public int Sound = 100;
    }
    public bool[] GiveStoneMake()
    {
        return gameData.StoneMake;
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
        gameData.MapObj = new int[1000];
        gameData.Dest = new int[1000];
        gameData.Story = new bool[1000];
        gameData.BGSound = 100;
        gameData.Sound = 100;
        gameData.Money = new int[2];

        gameData.MaxHp = 3;

        gameData.SavePoint=0;
        gameData.LostMoneyBag = false;
        gameData.Stone = null;
        Save();

    }
    [ContextMenu("게임초기화")]
    public void ResetMap01()
    {
        ResetMap();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public Vector3 DiePos()
    {
        return SavePos[gameData.SavePoint].transform.position;
    }
    public bool ChangeSave(int ii)
    {
        if (gameData.SavePoint == ii)
        {
            return false;
        }
        for (int i = 0; i < SavePos.Length; i++)
        {
            if (SavePos[i] != null) SavePos[i].GetComponent<Animator>().SetInteger("State", 1);
        }
        Ply.GetComponent<Player>().SaveHp();
        gameData.SavePoint = ii;


        for (int j = 0; j < savePointObj.Count; j++)
        {
            savePointObj[j].makeClone();
        }
        Save();
        return true;
    }
    public void MapSSS(int i,int ii)
    {
        gameData.MapObj[i] = ii;
        Save();
    }
    public int GiveMaxHp(int a = -1)
    {
        if (a > 0)
        {
            gameData.MaxHp = a;
            Save();
        }
        return gameData.MaxHp;
    }
    public int MapSSS(int i)
    {
        return gameData.MapObj[i];
    }
    public int GiveNPCSayNum(int i)
    {
        return gameData.NpcSayNum[i];
    }
    public bool GiveStory(int i)
    {
        return gameData.Story[i];
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

    public Transform[] ItemPre;
    public GameObject[] AllSton;

    public List<SaveResetObj2> savePointObj;

    public Material EnemyMaterial;
    public Material OutLineMaterial;
    public void AddSetObj(SaveResetObj2 me)
    {
        if (savePointObj == null) savePointObj = new List<SaveResetObj2>();
        savePointObj.Add(me);
        me.saveCode = savePointObj.Count - 1;
        Destroy(me.GetComponent<SaveResetObj>());
        me.gameObject.SetActive(false);
        me.makeClone();
    }
    //Cinemachine.CinemachineConfiner cam;
    Mcam cam;

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
        for (int i = 0; StoryTr != null && i < StoryTr.childCount; i++) 
        {
            if (StoryTr.GetChild(i) != null) StoryTr.GetChild(i).GetComponent<StoryStart>().StoryCode = i;
        }


        //cam = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineConfiner>();
        cam = Camera.main.GetComponent<Mcam>();

        Screen.SetResolution(1920, 1080, true);

        for (int i = 0; i < MapPar.childCount; i++)
        {
            if (MapPar.GetChild(i).childCount > 0) MapPar.GetChild(i).gameObject.SetActive(false);
        }
        Load();
        //Load1();
        //alls = new List<SoundCont>();
    }
    public Transform MapPar;
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

    public void GiveStone(int[] ss)
    {
        gameData.Stone = new int[ss.Length];
        for (int i = 0; i < ss.Length; i++)
        {
            gameData.Stone[i] = ss[i];
        }
        Save();
    }
    public int[] GiveStone()
    {
        return gameData.Stone;
    }
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
        //Ply.GetComponent<Player>().StopGame = StSTop;
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
        //Save();
    }
}
