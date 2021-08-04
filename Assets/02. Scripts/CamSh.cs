using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSh : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera setting;
    Cinemachine.CinemachineConfiner bgSetting;
    public Transform Cam;
    public bool Sub;
    public Cinemachine.CinemachineConfiner bgSettingset;
    public float Power = 1;
    
    Transform ply;
    void Start()
    {
        ply = GameSystem.instance.Ply;
        setting = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        bgSetting = GetComponent<Cinemachine.CinemachineConfiner>();
    }

    int MoveTrue = 0;
    float MoveTime = 0;

    public float MovePower = 3;
    public void CamMove(float Time)
    {
        MoveTrue = 1;
        MoveTime = Time;
    }
    void Update()
    {
        if (Sub)
        {
            bgSetting.m_BoundingShape2D = bgSettingset.m_BoundingShape2D;
        }
        if (MoveTrue == 0)
        {
        }
        else if (MoveTrue == 1) 
        {
            MoveTrue = 2;
            setting.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Power;
        }
        else if (MoveTrue == 2 && MoveTime >= 0)
        {
            MoveTime -= Time.deltaTime;
        }
        else if(MoveTrue==2)
        {
            MoveTrue = 0;
            setting.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        }
    }
}
