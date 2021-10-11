using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anemonelime : Enemy01
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }
    public float ShootTime=5;
    float NST;
    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (NST <= 0)
        {
            NST = ShootTime;
            ani.SetTrigger("On");
        }
        else
        {
            NST -= Time.deltaTime;
        }
    }
    public Transform ShhotTr;//쏠것
    public Transform STr;//쏘는 위치
    public float ShootSpeed = 3;
    public void Shoot()
    {
        Transform s = Instantiate(ShhotTr);
        s.position = STr.position;
        s.parent = transform.parent;
        s.GetComponent<Rigidbody2D>().gravityScale = 0;
        s.GetComponent<Rigidbody2D>().velocity = (Vector3)((Vector2)(STr.position - transform.position)).normalized * ShootSpeed;
    }
}
