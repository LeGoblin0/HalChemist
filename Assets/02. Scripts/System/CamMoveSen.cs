using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveSen : MonoBehaviour
{
    CamMove cam;
    private void Start()
    {
        cam = transform.parent.GetComponent<CamMove>();
    }
    public bool RightSen;
    public bool LeftSen;
    public bool UpSen;
    public bool DownSen;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (RightSen) cam.RightSen = true;
        if (LeftSen) cam.LeftSen = true;
        if (UpSen) cam.UpSen = true;
        if (DownSen) cam.DownSen = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (RightSen) cam.RightSen = false;
        if (LeftSen) cam.LeftSen = false;
        if (UpSen) cam.UpSen = false;
        if (DownSen) cam.DownSen = false;
    }
}
