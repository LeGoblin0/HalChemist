using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlock : MonoBehaviour
{
    Animator ani;
    public GameObject Target;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (ani == null) ani = GetComponent<Animator>();
        if (Target == null || !Target.activeSelf) BlockDie();
    }
    private void Update()
    {
        if (Target == null || !Target.activeSelf)
        {
            ani.SetInteger("State", 1);
        }
    }
    public void BlockDie()
    {
        Destroy(gameObject);
    }
}
