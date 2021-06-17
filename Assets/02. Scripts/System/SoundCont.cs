using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCont : MonoBehaviour
{
    AudioSource aus;
    private void Awake()
    {
    }
    void Start()
    {
        aus = GetComponent<AudioSource>();
        GameSystem.instance.AddSound(this);
        ChangeSound();
    }

    public bool BGSound = false;
    public void ChangeSound()
    {
        aus.volume = (BGSound ? GameSystem.instance.gameData.BGSound : GameSystem.instance.gameData.Sound) / 100f;
    }
}
