using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEfff : MonoBehaviour
{
    [Header("카메라")]
    CamSh camsh;
    void Start()
    {
        camsh = transform.parent.parent.GetComponent<Player>().camsh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform[] EEE;
    public float OffSetRandom = .3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Transform aa = Instantiate(EEE[Random.Range(0, EEE.Length)]);
            aa.transform.position = new Vector3(collision.transform.position.x + Random.Range(-OffSetRandom, OffSetRandom), collision.transform.position.y + Random.Range(-OffSetRandom, OffSetRandom), 0);
            Destroy(aa.gameObject, 3);
            Time.timeScale = .2f;
            Invoke("TTT", .05f);
            camsh.CamMove(.5f);
        }
    }
    void TTT()
    {
        Time.timeScale = 1;
    }
}
