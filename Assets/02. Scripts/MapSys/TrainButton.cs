using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainButton : MonoBehaviour
{
    Animator ani;
    public Train[] TranGo;

    void Start()
    {
        ani = GetComponent<Animator>();
    }
    bool PutNow = true;
    public void PutBut()
    {
        PutNow = true;
        ani.SetInteger("State", 1);
        for (int i = 0; i < TranGo.Length; i++)
        {
            TranGo[i].GoTrain();
        }
    }
    public void OutBut()
    {
        ani.SetInteger("State", 0);
        PutNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!PutNow && collision.GetComponent<Rigidbody2D>() != null && collision.GetComponent<Rigidbody2D>().velocity.y < -1f) 
        {
            PutBut();
        }
    }
}
