using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MMini : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform MMM;
    public Transform MMMp;

    [ContextMenu("맵 연결 확인")]
    public void DrowLine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetChild(1) != null)
            {
                for (int j = 0; j < transform.GetChild(i).GetChild(1).childCount; j++)
                {
                    transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<MoveMap>().DrowLine();
                }
            }
        }
    }

}
