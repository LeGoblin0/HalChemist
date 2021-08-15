using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MMini : MonoBehaviour
{
    public int RMapNum = 5;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TilemapRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<TilemapRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform MMM;
    public Transform MMMp;

    public Transform SetBG;

    [ContextMenu("맵 연결 확인")]
    public void DrowLine()
    {
        for (int i = RMapNum; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount>0&& transform.GetChild(i).GetChild(1) != null)
            {
                for (int j = 0; j < transform.GetChild(i).GetChild(0).childCount; j++)
                {
                    transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<MoveMap>().DrowLine();
                }
            }
        }
    }

}
