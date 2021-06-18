using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy2 : Enemy01
{
    public float ShootTime = 3;
    public Vector2 ShootVec;
    public float ShootSpeed;
    float nowTime = 0;
    public Transform ShootObj;
    public Transform AttPos;


    public float ChangeShootPosTime = 5;
    float chanigTime = 0;
    public Vector2[] shootpos;

    protected override void Update()
    {
        base.Update();
        if (Hp <= 0)
        {
            if (!Die)
            {
                Die = true;
                gameObject.layer = 18;
                //gameObject.layer = 8;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                if (GetComponent<Hold>() != null)
                {
                    GetComponent<Hold>().enabled = true;
                }
            }
            return;
        }
        if (chanigTime <= 0)
        {
            chanigTime = ChangeShootPosTime;
            ShootVec = shootpos[Random.Range(0, shootpos.Length)];
        }
        else
        {
            chanigTime -= Time.deltaTime;
        }
        if (nowTime < 0 )
        {
            nowTime = ShootTime;
            Transform att = Instantiate(ShootObj);
            att.position = AttPos.position;
            att.parent = transform;
            att.GetComponent<Rigidbody2D>().velocity = ShootVec.normalized * ShootSpeed+ GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            nowTime -= Time.deltaTime;
        }
    }
}
