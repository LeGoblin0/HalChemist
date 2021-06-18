using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHand : MonoBehaviour
{
    private void OnDestroy()
    {
        GameSystem.instance.Ply.GetComponent<Player>().DontHand = false;
        GameSystem.instance.Ply.GetComponent<Player>().HandoOn = true;
       // GameSystem.instance.
    }
}
