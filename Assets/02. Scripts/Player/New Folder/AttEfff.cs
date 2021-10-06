using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEfff : MonoBehaviour
{
    [Header("카메라")]
    Mcam cam;
    public float DesTime=.2f;
    Transform lmg;
    void Start()
    {
        cam = Camera.main.GetComponent<Mcam>();
        lmg = GameSystem.instance.Ply.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform[] EEE;
    public float OffSetRandom = .3f;
    public Vector3 OFFSET;
    public Vector3 Scall=Vector3.one;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Transform aa = Instantiate(EEE[Random.Range(0, EEE.Length)]);
            aa.transform.position = (new Vector3(transform.position.x + Random.Range(-OffSetRandom, OffSetRandom), transform.position.y + Random.Range(-OffSetRandom, OffSetRandom), 0) + OFFSET * lmg.localScale.x);
            aa.localScale = Scall;
            Destroy(aa.gameObject, DesTime);

            if (GameSystem.instance.PlyAttSlow) Time.timeScale = .2f;
            Invoke("TTT", .05f);
            //camsh.CamMove(.5f);
        }
    }
    void TTT()
    {
        if (GameSystem.instance.PlyAttSlow) Time.timeScale = 1;
    }
}
