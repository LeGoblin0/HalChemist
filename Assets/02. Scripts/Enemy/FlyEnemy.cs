using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy01
{
    [Header("플레이어 위치 받아옴")]
    Transform Player;
    public float Speed;
    public bool KnockBack = false;
    public float KnockPower = 2;
    Rigidbody2D rig;
    public float STime = 1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Player = GameSystem.instance.Ply;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (SenserPly && STime <= 0)
        {
            //Debug.Log(!UDS && !RLS);
            if (KnockBack && HHIITime > 0)
            {
                rig.velocity = -(Player.position - transform.position).normalized * KnockPower;
            }
            else
            {
                rig.velocity = new Vector2(UDS?XXX: (Player.position - transform.position).normalized.x, RLS ? YYY: (Player.position - transform.position).normalized.y) * Speed;
            }
            //Debug.Log(rig.velocity);
        }
        if (STime > 0)
        {
            STime -= Time.deltaTime;
        }
    }
    protected override void Update()
    {
        base.Update();
        //Debug.Log(SenserPly);

    }
    public void UDSen(bool t)
    {
        if (t)
        {
            if (Player.position.x > transform.position.x) XXX = 1;
            else XXX = -1;
        }
        else
        {
            XXX = 0;
        }
        UDS = t;
    }
    public void RLSen(bool t)
    {
        if (t)
        {
            if (Player.position.y > transform.position.y) YYY = 1;
            else YYY = -1;

        }
        else
        {
            YYY = 0;
        }
        RLS = t;
    }
    public bool UDS = false;
    public bool RLS = false;

   public int XXX = 0, YYY = 0;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
