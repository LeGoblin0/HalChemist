using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCode : MonoBehaviour
{
    public int ItemCodeNum;
    public bool DesTrap = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool DieAni = false;
    public void Eat()
    {
        if (DieAni)
        {
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetTrigger("On");
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Collider2D>());

                return;
            }

        }
        DDD();
    }
    public void DDD()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DesTrap && collision.tag == "TrapGround")
        {
            DDD();
        }
    }
}
