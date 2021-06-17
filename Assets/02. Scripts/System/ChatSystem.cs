using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro text;
    public GameObject TextBox;

    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach(var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(chatPoint));
    }

    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while (sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();
            text.text = currentSentence;

            float x = text.preferredWidth;

           // x = (x>2f)? 2f: x + 0.3f;
            TextBox.SetActive(true);
            TextBox.transform.localScale = new Vector2(2 , text.preferredHeight);
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + 1f + text.preferredHeight);
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
   
}
