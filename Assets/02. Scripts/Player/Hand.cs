using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Player ply;
    Animator ani;
    void Start()
    {
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        transform.parent = null;
        ani = GetComponent<Animator>();
    }

    public Vector3 offset;
    
    public Vector3 SubOffset ;
    public Transform DeshTr;
    public Color DeshCol;
    float cool = 0;
    public float DeshPrTime = 0.03f;
    public float DeshPrStayTime = 0.5f;

    public float Speed = 3;

    float PlyLook = 0;
    void Update()
    {
        transform.position += (new Vector3((offset.x + SubOffset.x) * PlyLook, offset.y + SubOffset.y, offset.z + SubOffset.z) + ply.transform.position - transform.position) * Speed * Time.deltaTime;
        if (transform.position.x > ply.transform.position.x)
        {
            transform.localScale=new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }



        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ang_Hit") || ani.GetCurrentAnimatorStateInfo(0).IsName("Hand_Hit")) 
        {
            SubOffset = new Vector3(-.5f, -.5f, 0);
        }
        else 
        {
            SubOffset = Vector3.zero;
            PlyLook = ply.transform.GetChild(0).localScale.x;
        }

        if (cool <= 0 && (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ang_Hit") || ani.GetCurrentAnimatorStateInfo(0).IsName("Hand_Hit") || ani.GetCurrentAnimatorStateInfo(0).IsName("Ang_Att") || ani.GetCurrentAnimatorStateInfo(0).IsName("Hand_Att")))
        {
            cool = DeshPrTime;
            Transform aa = Instantiate(DeshTr);
            aa.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            aa.localScale = transform.localScale;
            aa.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f) + (transform.GetChild(0).position - transform.position);

            aa.GetComponent<SpriteRenderer>().material.SetColor("_Desh", DeshCol);
            Destroy(aa.gameObject, DeshPrStayTime);
        }
        cool -= Time.deltaTime;
    }
    public void EndHit()
    {
        transform.position += new Vector3(3f * PlyLook, 0, 0);
        if (transform.position.x > ply.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MakeStone()
    {
        ply.MakeStone(); 
    }
    public void ThrowStone()
    {
        ply.TStone();
    }
}
