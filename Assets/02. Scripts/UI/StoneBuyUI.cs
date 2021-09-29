using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneBuyUI : MonoBehaviour
{
    Player ply;
    bool[] StoneMake;
    void Start()
    {
        ply = GameSystem.instance.Ply.GetComponent<Player>();
        if(StoneMake==null) StoneMake = GameSystem.instance.GiveStoneMake();
        Car = 0; Choose = false;
        NowCar();
        if (MoneyNum == null) MoneyNum = new int[StoneNum.Length];
        for (int i = 0; i < StoneNum.Length; i++)
        {
            MoneyNum[i] = GameSystem.instance.AllSton[StoneNum[i]].GetComponent<StoneDieAni>().MoneyBuy;
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = MoneyNum[i] + "G";
        }
    }
    int[] MoneyNum;
    public int[] StoneNum;
    private void OnDisable()
    {
        ply.OnStory = false;
    }
    private void OnEnable()
    {
        if (StoneMake == null) StoneMake = GameSystem.instance.GiveStoneMake();
        Car = 0; Choose = false;
        NowCar();
    }
    int Car = 0;
    bool Choose = false;
    int NowNum = 0;
    void Update()
    {
        if (Choose)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NowNum -= 10;
                if (NowNum < 1) NowNum = 1;
                Numset();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NowNum += 10;
                Numset();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NowNum += 1;
                Numset();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NowNum -= 1;
                if (NowNum < 1) NowNum = 1;
                Numset();
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                Choose = false;
                NowCar();
            }
            else if (Input.GetKeyDown(KeyCode.G) && Numset())
            {
                ply.Money -= NowNum * MoneyNum[Car];

                for (int i = 1; i < ply.MaxStoneNum && NowNum > 0; i++)
                {
                    if (ply.HaveStone[i] / 1000 == StoneNum[Car])
                    {
                        ply.HaveStone[i] += NowNum;
                        if(ply.HaveStone[i] > ply.MaxStoneNum)
                        {
                            NowNum = ply.HaveStone[i] - ply.MaxStoneNum;
                        }
                        else
                        {
                            NowNum = 0;
                        }
                    }

                }
                for (int i = 1; i < ply.MaxStoneNum && NowNum > 0; i++)
                {
                    if (ply.HaveStone[i] / 1000 == 0)
                    {
                        ply.HaveStone[i] = NowNum + StoneNum[Car] * 1000;
                    }

                }
                ply.LookMoney();
                ply.StoneUI();
                ply.PlySave();
                Choose = false;
                NowCar();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Car--;
                if (Car < 0) Car = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Car++;
                if (Car > 9) Car = 9;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Car += 5;
                if (Car > 9) Car = 9;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Car -= 5;
                if (Car > 9) Car = 9;
            }
            else if (Input.GetKeyDown(KeyCode.G) && StoneMake[Car]) 
            {
                NowNum = 1;
                Choose = true;
                Numset();
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                ply.OnStory = false;
                gameObject.SetActive(false);
            }
            //Debug.Log(Car);
            NowCar();
        }
    }
    bool Numset()
    {
        bool ss = true;
        transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = NowNum + "개";
        int aaa = 0;
        for (int i = 1; i < ply.MaxStoneNum; i++)
        {
            if (ply.HaveStone[i] / 1000 == StoneNum[Car] || ply.HaveStone[i] / 1000 == 0)
            {
                aaa += (ply.StackStone - ply.HaveStone[i] % 1000);
            }
            
        }
        if (aaa < NowNum)
        {
            transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().color = Color.red;
            ss = false;
        }
        else
        {
            transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().color = Color.yellow;
        }


        transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = (NowNum * MoneyNum[Car]) + "G";
        if (ply.Money < NowNum * MoneyNum[Car])
        {
            transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().color = Color.red;
            ss = false;
        }
        else
        {
            transform.GetChild(Car).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().color = Color.white;
        }
        return ss;
    }
    void NowCar()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(i);
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(i).GetComponent<Image>().enabled = StoneMake[i];
            transform.GetChild(i).GetChild(1).gameObject.SetActive(!StoneMake[i]);

            transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        transform.GetChild(Car).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(Car).GetChild(0).GetChild(StoneMake[Car] ? 0 : 2).gameObject.SetActive(true);
        if (Choose)
        {
            transform.GetChild(Car).GetChild(0).GetComponent<Image>().enabled = false;
            transform.GetChild(Car).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(Car).GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(Car).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
}
