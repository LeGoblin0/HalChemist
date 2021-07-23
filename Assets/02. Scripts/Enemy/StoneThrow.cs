using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrow : MonoBehaviour
{
    Rigidbody2D rig;
    public float Speed = 0;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rig != null)
        {
            Vector2 rign = rig.velocity.normalized;
            if (rign.x >= 0.92387f)
            {
                rig.velocity = new Vector2(1, 0).normalized * Speed;
            }
            else if (rign.x <= -0.92387f)
            {
                rig.velocity = new Vector2(-1, 0).normalized * Speed;
            }
            else if (rign.y >= 0.92387f)
            {
                rig.velocity = new Vector2(0, 1).normalized * Speed;
            }
            else if (rign.y <= -0.92387f)
            {
                rig.velocity = new Vector2(0, -1).normalized * Speed;
            }
            else if (rign.x <= 0.92387f && rign.x >= 0)
            {
                if (rign.y > 0)
                {
                    rig.velocity = new Vector2(1, 1).normalized * Speed;
                }
                else
                {
                    rig.velocity = new Vector2(1, -1).normalized * Speed;
                }
            }
            else if (rign.x >= -0.92387f && rign.x <= 0)
            {
                if (rign.y > 0)
                {
                    rig.velocity = new Vector2(-1, 1).normalized * Speed;
                }
                else
                {
                    rig.velocity = new Vector2(-1, -1).normalized * Speed;
                }
            }
            //Debug.Log(rig.velocity);
        }
    }
}
