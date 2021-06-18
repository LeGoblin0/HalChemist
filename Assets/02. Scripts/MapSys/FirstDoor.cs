using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    Animator ani;
    // Update is called once per frame
    bool a = false;
    void Update()
    {
        if (GameSystem.instance.SaveNow() == 1 && !a)
       {
           a = true;
            ani.SetTrigger("On");
        }

    }
}
