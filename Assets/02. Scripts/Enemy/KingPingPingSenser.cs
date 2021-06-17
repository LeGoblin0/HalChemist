using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPingPingSenser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            transform.parent.parent.gameObject.GetComponent<KingPingPing>().StST();

        }
    }
}
