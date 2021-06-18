using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPingPing : Enemy01
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public float Speed=5f;
    public float SturnTime = 3;

    public bool MoveN = false;
    protected override void Update()
    {
        if (SenserPly)
        {
            if (!MoveN)
            {

                MoveN = true;
                ani.SetInteger("State", 0);
            }
        }
        if (!MoveN) return;
        base.Update();
        if (Hp <= 0)
        {
            Debug.Log(Hp);
            GameSystem.instance.DieMonset(GetComponent<SaveMonsetDie>().Code);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("End");
            return;
        }
        if (gogo)
        {
            transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime * posX;
        }
    }
    int posX = -1;
    bool gogo = false;
    public void GoN()
    {
        gogo = true;
    }
    public void Stops()
    {
        gogo = false;

    }
    public void TTurn()
    {
        posX *= -1;
        transform.GetChild(0).localScale = new Vector3(-posX, 1, 1);
        ani.SetInteger("State", 0);
    }
    public void StST()
    {
        ani.SetInteger("State", 1);
        Invoke("StST2", SturnTime);
    }
    public void StST2()
    {
        ani.SetInteger("State", 0);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "TurnPoint")
        {
            ani.SetInteger("State", 2);
        }
    }
  
}
