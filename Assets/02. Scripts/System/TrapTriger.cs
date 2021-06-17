using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Transform set;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Att" && collision.gameObject.GetComponent<ShootEnemy>() != null)
        {
            Debug.Log(0);
            int dd = collision.gameObject.GetComponent<Att>().AttDamage;
            set.GetComponent<Att>().AttDamage = dd;
            Transform Tra = Instantiate(set);
            Tra.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Att")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Att" && collision.gameObject.GetComponent<ShootEnemy>() != null)
        {
            Debug.Log(0);
            int dd = collision.gameObject.GetComponent<Att>().AttDamage;
            set.GetComponent<Att>().AttDamage = dd;
            Transform Tra = Instantiate(set);
            Tra.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Att")
        {
            Destroy(collision.gameObject);
        }
    }
}
