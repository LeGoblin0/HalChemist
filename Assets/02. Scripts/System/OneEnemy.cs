using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform Enemy;
    Transform eee;
    // Update is called once per frame
    float DTime = 3;

    public int MaxNum = 2;
    public float SpawnTime = 3;
    void Update()
    {
        if (eee == null && transform.childCount < MaxNum) 
        {
            if (DTime < 0)
            {
                eee = Instantiate(Enemy, transform);
                eee.position = transform.position;
                DTime = SpawnTime;
            }
            else DTime -= Time.deltaTime;
        }
    }
}
