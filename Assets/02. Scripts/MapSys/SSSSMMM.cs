using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSSSMMM : MonoBehaviour
{
    [Range(-1, 1)]
    float Throw;
    private void Start()
    {
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        Throw = transform.localScale.x;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ply.GetComponent<Rigidbody2D>().velocity = new Vector2(Throw, -1) * 10;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        }
    }
    Player ply;
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
