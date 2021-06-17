using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ply = GameSystem.instance.Ply;
        rig = GetComponent<Rigidbody2D>();
    }
    Transform ply;
    public float Speed = 3;
    Rigidbody2D rig;
    // Update is called once per frame
    void Update()
    {
        rig.velocity = -new Vector2(transform.position.x - ply.position.x, transform.position.y - ply.position.y).normalized * Speed;
    }
}
