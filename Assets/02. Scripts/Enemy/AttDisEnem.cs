using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttDisEnem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ee = transform.parent.GetComponent<ShootEnemy1>();
    }
    ShootEnemy1 ee;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ee.AttDis = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ee.AttDis = false;
    }
}
