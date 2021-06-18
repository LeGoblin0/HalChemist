using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy01
{
    [Header("플레이어 위치 받아옴")]
    Transform Player;
    public float Speed;

    // Start is called before the first frame update
    protected override void Start()
    {
        ani = GetComponent<Animator>();
        Player = GameSystem.instance.Ply;
    }

    // Update is called once per frame
    
    protected override void Update()
    {
        base.Update();
        if (SenserPly)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        
    }

}
