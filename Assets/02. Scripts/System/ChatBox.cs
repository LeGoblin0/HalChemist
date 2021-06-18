using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    // Start is called before the first frame update

    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    void Start()
    {
        
    }
        
    // Update is called once per frame
   

    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }
    private void OnMouseDown()
    {
        TalkNpc();
    } 


}
