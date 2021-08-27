using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Hide.Length; i++)
        {
            Hide[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TilemapRenderer[] Hide;
}
