using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDRLS : MonoBehaviour
{
    public FlyEnemy eee;
    public bool uds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(1);
        if (collision.tag == "Ground")
        {
            //Debug.Log(0);
            if (uds) eee.UDSen(true);
            else eee.RLSen(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {
            if (uds) eee.UDSen(false);
            else eee.RLSen(false);
        }
    }
}
