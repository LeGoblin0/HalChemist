using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : Enemy01
{

    public float ShootTime = 3;
    public Vector2 ShootVec;
    public float ShootSpeed;
    float nowTime = 0;
    public Transform ShootObj;

    protected override void Update()
    {
        base.Update();
        if (nowTime < 0)
        {
            nowTime = ShootTime;
            ani.SetTrigger("Shot");
        }
        else
        {
            nowTime -= Time.deltaTime;
        }
    }
   
    void Launch()
    {
        Transform att = Instantiate(ShootObj);
        att.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        att.GetComponent<Rigidbody2D>().velocity = ShootVec.normalized * ShootSpeed * transform.localScale.x;
    }
}
