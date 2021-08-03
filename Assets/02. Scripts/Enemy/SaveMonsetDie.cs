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
        GameSystem.instance.DieMonset(Code);
    }
}
