using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector3 nn;
    public Transform Ply;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + Ply.position * transform.parent.localScale.x * transform.parent.parent.parent.localScale.x + nn;

        //Debug.Log(transform.parent.position + "   " + Ply.position + "    " + transform.parent.localScale.x+ " = "+( transform.parent.position + Ply.position * transform.parent.localScale.x));
    }
}
