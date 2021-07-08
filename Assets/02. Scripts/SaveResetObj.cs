using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResetObj : MonoBehaviour
{

    private void Start()
    {
        SaveResetObj2 a= gameObject.AddComponent<SaveResetObj2>();
        GameSystem.instance.AddSetObj(a);
    }
}
