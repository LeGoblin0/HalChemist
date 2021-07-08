using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResetObj2 : MonoBehaviour
{

    public int saveCode = 0;

    public Transform clone;

    public void makeClone()
    {
        if (clone != null) Destroy(clone.gameObject);
        clone = Instantiate(transform);
        clone.parent = transform.parent;
        clone.position = transform.position;
        clone.gameObject.SetActive(true);

        //Debug.Log(0);
    }
}
