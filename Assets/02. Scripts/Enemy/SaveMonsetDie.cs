using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMonsetDie : MonoBehaviour
{
    public int Code;
    private void OnDestroy()
    {
        
    }
    public void SaveMonster()
    {
        if (Off) gameObject.SetActive(false);
        GameSystem.instance.DieMonset(Code);
    }
    public bool Off = false;
}
