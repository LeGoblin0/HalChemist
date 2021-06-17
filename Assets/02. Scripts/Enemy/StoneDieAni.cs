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

    public Sprite StonImg;
    private void OnDestroy()
    {
        Transform aa = Instantiate(DieObj);
        aa.position = transform.position;
        Destroy(aa.gameObject,.5f);
    }
}
