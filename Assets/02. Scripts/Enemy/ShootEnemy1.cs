using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy1 : Enemy01
{
    public float ShootTime = 3;
    public Vector2 ShootVec;
    public float ShootSpeed;
    float nowTime = 2;
    public Transform ShootObj;

    public bool AttDis = false;

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
        if (nowTime < 0 && AttDis)
        {
            nowTime = ShootTime;
            Transform att = Instantiate(ShootObj);
            att.position = transform.position;
            att.GetComponent<Rigidbody2D>().velocity = ShootVec.normalized * ShootSpeed;
            Destroy(att.gameObject, 0.3f);
        }
        else
        {
            nowTime -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (go)
        {
            Debug.Log(rig.velocity);
            if (rig.velocity.x == 0)
            {
                Debug.Log(0);
                xmove = -xmove;
            }
            rig.velocity = new Vector2(xmove, rig.velocity.y);
        }
    }
    public override void DieHit()
    {
        base.DieHit();
        //Debug.Log(0);
        if (GetComponent<Rigidbody2D>() != null)
        {
            //Debug.Log(1);
            Transform ply = GameSystem.instance.Ply;
            GetComponent<Rigidbody2D>().velocity = -new Vector2(transform.position.x - ply.position.x, transform.position.y - ply.position.y).normalized * 5;//벽과 충돌시 다시 돌아옴
        }
    }
    bool go = false;
    float xmove = 0;
    Rigidbody2D rig;
}
