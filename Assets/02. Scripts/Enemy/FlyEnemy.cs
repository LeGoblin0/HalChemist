using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy01
{
    [Header("플레이어 위치 받아옴")]
    Transform Player;
    public float Speed;
    Rigidbody2D rig;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Player = GameSystem.instance.Ply;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    protected override void Update()
    {
        base.Update();
        //Debug.Log(SenserPly);
        if (SenserPly)
        {
            rig.velocity = (Player.position - transform.position).normalized * Speed;
            //Debug.Log(rig.velocity);
        }
        
    }

}
