using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEnemy : Enemy01
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();//체력감소하면 사망
        if (SenserPly)
        {
            ani.SetInteger("state", 1);
        }
        if (SenserPly == false)
        {
            ani.SetInteger("state", 0);
        }
    }
}

