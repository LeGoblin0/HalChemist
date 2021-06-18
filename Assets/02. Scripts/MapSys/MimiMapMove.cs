using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimiMapMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform MMM;
    void Start()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).transform.position = transform.position + MMM.GetChild(i).transform.position * transform.lossyScale.x;/// transform.parent.parent.localScale.x;
            transform.GetChild(i).GetComponent<Image>().SetNativeSize();
        }
    }
    public float MoveSpeed = 100;
    public float MapMaxSize = 15;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            transform.localScale += Vector3.one * Time.deltaTime;
            if (transform.localScale.x >= MapMaxSize)
            {
                transform.localScale = Vector3.one * MapMaxSize;
            }
        }
        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
            if (transform.localScale.x <= 1)
            {
                transform.localScale = Vector3.one;
            }
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.position += new Vector3(0, MoveSpeed, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.position += new Vector3(0, -MoveSpeed, 0) * Time.deltaTime;
        }
    }
}
