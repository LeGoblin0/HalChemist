using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainButton : MonoBehaviour
{
    Animator ani;
    public Train[] TranGo;
    public MapSyS[] MapObj;
    public int TranGoNum = 0;
    public bool XRight;
    [Tooltip("참 = 하->상 좌->우")]
    public bool ForceTrue;
    public bool YSet = true;
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
        if (YSet)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 6);
        }
    }
    public bool PutNow = true;
    public bool OnTouch = true;
    [ContextMenu("버튼 누르기")]
    public void PutBut()
    {
        PutNow = true;
        ani.SetInteger("State", 1);
        for (int i = 0; TranGo != null && i < TranGo.Length; i++)
        {
            if (TranGo[i] == null) continue;
            if (OnTouch) TranGo[i].GoTrain();
            else TranGo[i].GoTrain(TranGoNum);
        }
        for (int i = 0; MapObj != null && i < MapObj.Length; i++)
        {
            if (MapObj[i] == null) continue;
            MapObj[i].SpSp();
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
            //Debug.Log(PutNow+"   "+ collision.GetComponent<Rigidbody2D>().velocity.x);
            if (XRight&& ((ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.x > 6f) || (!ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.x < -6f)))
            {
                PutBut();
                return;
                //Debug.Log(0);
            }
            else if(!XRight && ((ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.y > 1f) || (!ForceTrue && collision.GetComponent<Rigidbody2D>().velocity.y <-1f)))
            {
                PutBut();
                return;
                //Debug.Log(1);
            }
        }
        if (!PutNow && collision.GetComponent<Att>() != null)
        {
            if (XRight && ((ForceTrue && collision.GetComponent<Att>().AttArrow == 4) || (!ForceTrue && collision.GetComponent<Att>().AttArrow == 3)))
            {
                PutBut();
                return;
                //Debug.Log(0);
            }
            else if (!XRight && ((ForceTrue && collision.GetComponent<Att>().AttArrow == 0) || (!ForceTrue && collision.GetComponent<Att>().AttArrow == 1)))
            {
                PutBut();
                return;
                //Debug.Log(1);
            }
        }
    }
}
