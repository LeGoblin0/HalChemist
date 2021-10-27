using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCode : MonoBehaviour
{
    public int ItemCodeNum;
    public bool DesTrap = true;
    public float DesTime = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    public RectTransform targetRect = null;
    float cool = 1000;
    public Transform DeshTr;
    public Color DeshCol;
    void Update()
    {
        DesTime -= Time.deltaTime;
        if (DesTime < 0)
        {
            DDD();
        }

        if (cool <= 0)
        {
            cool = 0.05f;
            Transform aa = Instantiate(DeshTr);
            aa.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
            aa.localScale = transform.localScale;
            aa.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f) + (transform.GetChild(0).position - transform.position);

            aa.GetComponent<SpriteRenderer>().material.SetColor("_Desh", DeshCol);
            Destroy(aa.gameObject, 0.1f);
        }
        cool -= Time.deltaTime;

    }
    private void FixedUpdate()
    {
        if (GoUI)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity - GetComponent<Rigidbody2D>().velocity.normalized * 3 * Time.deltaTime;
            Vector3 result = Camera.main.ScreenToWorldPoint(targetRect.position);
            transform.position -= new Vector3(transform.position.x - result.x, transform.position.y - result.y, transform.position.z).normalized * 10 * Time.deltaTime;

            //Debug.Log(result+"  "+transform.position);
            if (new Vector3(transform.position.x - result.x, transform.position.y - result.y, 0).sqrMagnitude < 0.01f)
            {
                DDD();
            }
        }
    }
    public int GoUIcode = -1;//ui로 간다음 사라지기
    bool GoUI = false;//준비다됬으면 ui로 가라
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
        if (GoUIcode >= 0)
        {
            targetRect = GameSystem.instance.ItemGoUITr[GoUIcode];
            Destroy(GetComponent<Collider2D>());
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).GetComponent<Collider2D>());
            }
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, 7f), Random.Range(-7, 7f));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
            GoUI = true;
            if (DeshTr != null) cool = 0;
             DesTime = 100;
        }
        else
        {
            DDD();
        }
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
