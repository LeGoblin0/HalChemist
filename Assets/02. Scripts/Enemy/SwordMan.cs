using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Enemy01
{
    float flag = -1;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void Att()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        transform.GetChild(0).localScale = new Vector3(flag, 1, 1);
    }
    private void AttEnd()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        flag *= -1;
    }
}
