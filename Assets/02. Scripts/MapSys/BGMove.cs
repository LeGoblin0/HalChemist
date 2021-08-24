﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    Transform cam;
    public float offset;
    public float Num = 1;
    void Start()
    {
        cam = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.position.x * Num + offset, transform.position.y, transform.position.z);
        if (cam.position.x - (transform.position.x) > 26.5f * 2)
        {
            offset += 26.5f * 3;
        }
        if (cam.position.x - (transform.position.x) < -26.5f * 2)
        {
            offset -= 26.5f * 3;
        }
    }
}
