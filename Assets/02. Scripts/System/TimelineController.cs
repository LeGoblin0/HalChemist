using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineController : MonoBehaviour
{
    public PlayableDirector HadoGetTimeline;
    public PlayableDirector PlayableDirector2;
    public PlayableDirector PlayableDirector3;
    public PlayableDirector PlayableDirector4;
    public GameObject Player;
    public GameObject GetUp;
    public GameObject Throw;
    public GameObject Door;

    public Vector2 PlayerPos;
    public Vector2 HandoGetPos;
    public Vector2 GetUpPos;
    public Vector2 ThrowPos;
    public Vector2 DoorPos;

    public float Distance;
    public float Distance2;
    public float Distance3;
    public float Distance4;


    public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = Player.transform.position;
        HandoGetPos = this.gameObject.transform.position;
        GetUpPos = GetUp.transform.position;
        ThrowPos = Throw.transform.position;
        DoorPos = Door.transform.position;


        Distance = Vector3.Distance(PlayerPos, HandoGetPos);
        Distance2 = Vector3.Distance(PlayerPos, GetUpPos);
        Distance3 = Vector3.Distance(PlayerPos, ThrowPos);
        Distance4 = Vector3.Distance(PlayerPos, DoorPos);

        if (Distance <= 2f)
        {
            HadoGetTimeline.Play();
        }
        else{
            //GameObject.Find("Player").GetComponent<Player>().HandoOn = true;
        }
        if (Distance2 <= 2f)
        {
            //PlayableDirector2.gameObject.SetActive(true);
            PlayableDirector2.Play();
        }
        if (Distance3 <= 2f)
        {
            //PlayableDirector2.gameObject.SetActive(true);
            PlayableDirector3.Play();
        }
        if (Distance4 <= 2f)
        {
            //PlayableDirector2.gameObject.SetActive(true);
            PlayableDirector4.Play();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            HadoGetTimeline.Stop();
            PlayableDirector2.Stop();
            PlayableDirector3.Stop();
            PlayableDirector4.Stop();
        }

        //else if (Distance <= 2f)
        // { 
        // Hando.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().OnStory = false;
      //  GameObject.Find("Player").GetComponent<Player>().HandoOn = false;
        // GameObject.Find("Player").GetComponent<Player>().DontHand = false;
        //  }
    }
    
}
