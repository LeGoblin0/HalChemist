using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : Enemy01
{
    public float JumpPower=3f;
    Rigidbody2D rig;
    protected override void Start()
    {
        base.Start();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("JumpEnemy_Jump_01") && rig.velocity.y <= 0)
        {
            ani.SetInteger("State", 2);
        }
    }

    public void JumpGo()
    {
        rig.velocity = new Vector2(0, JumpPower);
        ani.SetInteger("State", 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            ani.SetInteger("State", 0);
        }
    }
}
