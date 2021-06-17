using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameSystem.instance.SetHold(this);
    }
    [HideInInspector]
    public bool HoldNow = false;
    public float Mass = 1;
    public int ThrowDamage=1;
    // Update is called once per frame
    void Update()
    {
        
    }
}
