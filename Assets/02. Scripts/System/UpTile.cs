using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
            //Debug.Log(collision.tag);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
