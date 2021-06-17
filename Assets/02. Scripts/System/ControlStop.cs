using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStop : MonoBehaviour
{
    public GameObject Player;
    public Vector2 GetUpPos;
    public GameObject GetUp;
    public Vector2 PlayerPos;
    public float Distance;
    public float Distance2;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = Player.transform.position;
        GetUpPos = GetUp.transform.position;
        Distance = Vector3.Distance(PlayerPos, GetUpPos);

        if(PlayerPos.x > GetUpPos.x)
        {

            Distance2 = PlayerPos.x - GetUpPos.x;
        }
        else
        {
            Distance2 =  GetUpPos.x- PlayerPos.x;

        }

        if (Distance < 8)
        {
            GameObject.Find("Player").GetComponent<Player>().OnStory = true;
        }
        
       
    }
   
}
