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
        //ScreenX = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_ScreenX;
        //ScreenY = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_ScreenY;
        //DeapZoneWidth = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneWidth;
        //DeapZoneHeight = setting.GetComponentInChildren<CinemachineFramingTransposer>().m_DeadZoneHeight;

    }
    
    //public Cinemachine.CinemachineVirtualCamera setting;
    //public Cinemachine.CinemachineConfiner bgSetting;
    //float ScreenX;
    //float ScreenY;
    //float DeapZoneWidth;
    //float DeapZoneHeight;

    private void Update()
    {
    }
    public Transform PlyCamTr;
    public Vector3 offset;
    private void FixedUpdate()
    {
        Vector3 plyoffset = ply.position + offset;
        PlyCamTr.position = new Vector3(Hold_X ? Smap.position.x : plyoffset.x, Hold_Y ? Smap.position.y : plyoffset.y, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (Smap == collision.transform)
        {
            Hold_X = false;
            Hold_Y = false;
            Smap = null;
        }
    }
    public Transform Smap;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MapCam>() != null)
        {
            Hold_X = collision.GetComponent<MapCam>().Hold_X;
            Hold_Y = collision.GetComponent<MapCam>().Hold_Y;
            Smap = collision.transform;
        }
    }



}
