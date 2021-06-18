using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Player ply;
    public Transform plyPos;
    public float Speed;
    public Vector3 Offset;
    private void Start()
    {
        transform.parent = null;
        transform.position = plyPos.position;
    }
    private void FixedUpdate()
    {
        if (transform.position.x < plyPos.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
        Vector3 goPos = plyPos.position + new Vector3(Offset.x * plyPos.GetChild(0).localScale.x, Offset.y, 0);
        //Debug.Log(new Vector3(transform.position.x - goPos.x, transform.position.y - goPos.y, 0) * Speed);       
        //Debug.Log(transform.position.x +"   "+ goPos.x + "   " + transform.position.y + "   " + goPos.y + "   " + Speed);
        transform.position -= new Vector3(transform.position.x - goPos.x, transform.position.y - goPos.y, 0) * Speed * Time.deltaTime;
    }
    private void Update()
    {
    }
    public void ThrowEnd()
    {
        ply.ThrowEnd();
    }
    public void Throw()
    {
        ply.Throw();
    }
}
