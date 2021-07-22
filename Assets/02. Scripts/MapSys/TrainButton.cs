using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainButton : MonoBehaviour
{
    Animator ani;
    public Train[] TranGo;
    public int TranGoNum = 0;
    public bool XRight;
    [Tooltip("참 = 하->상 좌->우")]
    public bool ForceTrue;
    void Start()
    {
        ani = GetComponent<Animator>();
        if ((int)(transform.eulerAngles.z + .5f + 720) % 360 == 0)
        {
            XRight = false;
            ForceTrue = false;
        }
        else if ((int)(transform.eulerAngles.z + .5f + 720) % 360 == 90)
        {
            XRight = true;
            ForceTrue = true;
        }
        else if ((int)(transform.eulerAngles.z + .5f + 720) % 360 == 180)
        {
            XRight = false;
            ForceTrue = true;
        }
        else if ((int)(transform.eulerAngles.z + .5f + 720) % 360 == 270)
        {
            XRight = true;
            ForceTrue = false;
        }
    }
    bool PutNow = true;
    public bool OnTouch = true;
    [ContextMenu("버튼 누르기")]
    public void PutBut()
    {
        PutNow = true;
        ani.SetInteger("State", 1);
        for (int i = 0; i < TranGo.Length; i++)
        {
            if (OnTouch) TranGo[i].GoTrain();
            else TranGo[i].GoTrain(TranGoNum);
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
        if (!PutNow && collision.GetComponent<Rigidbody2D>() != null ) 
        {
            if (XRight&& ((ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.x > 6f) || (!ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.x < -6f)))
            {
                PutBut();
                //Debug.Log(0);
            }
            else if(!XRight && ((ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.y > 1f) || (!ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.y <-1f)))
            {
                PutBut();
                //Debug.Log(1);
            }
        }
    }
}
