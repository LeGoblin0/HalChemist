using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlyCam : MonoBehaviour
{
    public bool Hold_X;
    public bool Hold_Y;

    Transform ply;
    private void Start()
    {
        ply = GameSystem.instance.Ply;
    }
    private void Awake()
    {
        gameObject.layer = 21;
        //ScreenX=
        ScreenX = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_ScreenX;
        ScreenY = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_ScreenY;
        DeapZoneWidth = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneWidth;
        DeapZoneHeight = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneHeight;

    }
    
    public Cinemachine.CinemachineVirtualCamera setting;
    public Cinemachine.CinemachineConfiner bgSetting;
    float ScreenX;
    float ScreenY;
    float DeapZoneWidth;
    float DeapZoneHeight;

    private void Update()
    {
        if (Hold_X)
        {
            setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
        }
        else
        {
            setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneWidth = DeapZoneWidth;
        }
        if (Hold_Y)
        {
            setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
        }
        else
        {
            setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneHeight = DeapZoneHeight;
        }
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(Hold_X ? Smap.position.x : ply.position.x, Hold_Y ? Smap.position.y : ply.position.y, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Hold_X = false;
        Hold_Y = false;
        Smap = null;
    }
    Transform Smap;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.GetComponent<MapCam>() != null)
        {
            Hold_X = collision.GetComponent<MapCam>().Hold_X;
            Hold_Y = collision.GetComponent<MapCam>().Hold_Y;
            Smap = collision.transform;
        }
    }



}
