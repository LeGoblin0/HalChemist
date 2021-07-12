using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSh : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera setting;
    Cinemachine.CinemachineConfiner bgSetting;
    Collider2D savepo;
    public Transform CamPly;
    public Transform Cam;
    Transform ply;
    void Start()
    {
        Debug.Log(CamPly);
        ply = GameSystem.instance.Ply;
        setting = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        bgSetting = GetComponent<Cinemachine.CinemachineConfiner>();
    }

    int MoveTrue = 0;
    float MoveTime = 0;
    Vector3 HitPos;

    public float MovePower = 3;
    public void CamMove(float Time)
    {
        MoveTrue = 1;
        MoveTime = Time;
    }
    void Update()
    {
        if (MoveTrue == 0)
        {
            CamPly.position = ply.position;
        }
        else if (MoveTrue == 1) 
        {
            MoveTrue = 2;
            savepo = bgSetting.m_BoundingShape2D;
            HitPos = Cam.position;
            CamPly.position = HitPos;
            Invoke("ss", 0f);
        }
        else if (MoveTrue == 2 && MoveTime >= 0)
        {
            MoveTime -= Time.deltaTime;
            CamPly.position = HitPos + new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f)) * MovePower;
        }
        else if(MoveTrue==2)
        {
            MoveTrue = 0;
            bgSetting.m_BoundingShape2D = savepo;
        }
    }
    void ss()
    {
        if (MoveTrue == 2 && MoveTime >= 0) bgSetting.m_BoundingShape2D = null;
    }
}
