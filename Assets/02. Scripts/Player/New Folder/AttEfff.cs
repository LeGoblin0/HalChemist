using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEfff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

        }
    }
}
