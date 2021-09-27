using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngGage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AngImg = GetComponent<Image>();
    }
    public Sprite[] GG;
    Image AngImg;
    // Update is called once per frame
    void Update()
    {
        if (AngImg.fillAmount < 0.9f)
        {
            AngImg.sprite = GG[0];
        }
        else if (AngImg.fillAmount >= 1f)
        {
            AngImg.sprite = GG[2];
        }
        else if (AngImg.fillAmount >= .9f)
        {
            AngImg.sprite = GG[1];
        }
    }
}
