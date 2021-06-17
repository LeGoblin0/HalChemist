using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DrowHold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<RectTransform>();
        offset = tran.position.x;

    }
    float offset = 0;
    public int Ivent = 0;
    RectTransform tran;
    // Update is called once per frame
    void Update()
    {
        tran.position += new Vector3((-Ivent * 100 - tran.position.x + offset)*10 * Time.deltaTime, 0, 0);
    }
}
