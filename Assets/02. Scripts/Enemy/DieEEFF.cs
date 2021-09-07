using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEEFF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float DesTi = 1;
    private void OnEnable()
    {
        Destroy(gameObject, DesTi);
    }
}
