using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidBlock : MonoBehaviour
{
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ani.SetInteger("State", 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ani.SetInteger("State", 0);
        }
    }
}
