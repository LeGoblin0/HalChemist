using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    [Range(0,30)]
    public float MinDis = 5;
    [Range(0, 30)]
    public float MaxDis = 15;

    public bool Sound2D = true;
    private void Awake()
    {
        if (GetComponent<AudioSource>() == null)
        {
            aus = gameObject.AddComponent<AudioSource>();
            aus.volume = 0;
            if (Sound2D) aus.spatialBlend = 1;
            aus.minDistance = 5; //Debug.Log(aus+"  1");
            aus.maxDistance = 15;
            aus.rolloffMode = AudioRolloffMode.Custom;
            //Invoke("OnVolum", 1);
            if (GetComponent<SoundCont>() == null) gameObject.AddComponent<SoundCont>();
        }
    }
    public void OnVolum()
    {

    }
    AudioSource aus;
    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioClip[] SSS;
    public void Sound00()
    {
        gogo(0);
    }
    public void Sound01()
    {
        gogo(1);
    }

    public void Sound02()
    {
        gogo(2);
    }
    public void Sound03()
    {
        gogo(3);
    }
    public void Sound04()
    {
        gogo(4);
    }
    public void Sound05()
    {
        gogo(5);
    }
    public void Sound06()
    {
        gogo(6);
    }
    public void Sound07()
    {
        gogo(7);
    }
    public void Sound08()
    {
        gogo(8);
    }
    public void Sound09()
    {
        gogo(9);
    }
    void gogo(int i)
    {
        if (aus == null) aus = GetComponent<AudioSource>();
        aus.PlayOneShot(SSS[i]);
    }
}
