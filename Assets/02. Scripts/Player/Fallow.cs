using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallow : MonoBehaviour
{
    public Transform Target;
    public Rigidbody2D Targetrig;
    Rigidbody2D rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position;
    }
    private void FixedUpdate()
    {
        if (Targetrig != null)
        {
            rig.velocity = Targetrig.velocity;
        }
    }
}
