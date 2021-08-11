using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    Transform cam;
    public float Num = 1;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.position.x * Num, transform.position.y, transform.position.z);
    }
}
