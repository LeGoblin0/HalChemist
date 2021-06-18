using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }
    float ss = 5;
    void Update()
    {
        ss -= Time.deltaTime;
        transform.localScale = new Vector3(ss / 5, ss / 5, 1)*2;
    }
}
