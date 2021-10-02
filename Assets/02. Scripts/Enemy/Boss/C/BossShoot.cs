using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }
    Animator ani;
    Rigidbody2D rig;
    Transform Target;
    // Update is called once per frame
    void Update()
    {
        Target = transform.parent;
        if (Target != null)
        {
            transform.position += new Vector3(Target.position.x - transform.position.x, Target.position.y - transform.position.y).normalized * 3 * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Shhoo_Att1") && collision.tag == "Att" && collision.GetComponent<Att>() != null)
        {
            ani.SetTrigger("Die");
            rig.velocity = Vector2.zero;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Shhoo_Att1") && collision.tag == "Ground")
        {
            ani.SetTrigger("Die");
            rig.velocity = Vector2.zero;
        }
    }
    public void DesOO()
    {
        Destroy(gameObject);
    }
}
