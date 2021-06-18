using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_1 : MonoBehaviour
{
     Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public float TimeLate = 1.5f;
    public bool SaveBoss = false;
    public int MonsterSNum = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void Update()
    {
        if (SaveBoss & GameSystem.instance.GiveMonster(MonsterSNum) == 1)
        {
            Destroy(gameObject);
        }
    }
    void SET()
    {
        ani.SetTrigger("On");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Boss_1_2>() != null)
        {
            Invoke("SET", TimeLate);
        }
    }
}
