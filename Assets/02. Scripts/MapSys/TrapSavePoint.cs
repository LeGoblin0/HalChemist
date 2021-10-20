using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SpriteRenderer>() != null) Destroy(GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
