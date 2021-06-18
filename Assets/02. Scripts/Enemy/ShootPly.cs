using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPly : Enemy01
{
    Transform ply;
    public float Speed = 2;
    protected override void Update()
    {

    }
    private void FixedUpdate()
    {
        if (ply == null)
        {
            ply = GameSystem.instance.Ply;
        }

        transform.position += (ply.position - transform.position).normalized * Speed * Time.deltaTime;
    }
    private void OnDestroy()
    {
        Hp = -1;

        if (DieItem.Length > 0)
        {
            for (int i = 0; i < DieItem.Length; i++)
            {
                Transform aa = Instantiate(DieItem[i]);
                aa.position = transform.position + new Vector3(0, 0, 1f);
                aa.parent = transform.parent;
            }
        }
    }
    protected override void  OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player")
        {
            Destroy(gameObject,.2f);
        }
    }
}
