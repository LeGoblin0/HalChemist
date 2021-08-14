using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove2 : MonoBehaviour
{
    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        int yyy = (int)(cam.position.y / 2) * 2;
        if (yyy > -13) yyy = -13;
        transform.position = new Vector3((int)(cam.position.x / 2) * 2, (int)(yyy / 2) * 2, transform.position.z);
    }
}
