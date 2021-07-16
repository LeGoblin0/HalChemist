using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDieAni : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //StonImg = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    public Transform DieObj;
    // Update is called once per frame
    void Update()
    {
        
    }
    public int Code;
    public Sprite StonImg;

    public bool DieSet = false;
    private void OnDestroy()
    {
        if (DieSet)
        {
            Transform aa = Instantiate(DieObj);
            aa.position = transform.position;
            Destroy(aa.gameObject, .5f);
        }
    }
}
