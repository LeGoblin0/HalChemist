using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public int SavePoint;
        public int[] MapObj;
        public int[] Dest;
        public bool[] Story;
        public int BGSound;
        public int Sound;
        public int[] Quest;

        public bool LostMoneyBag;
        public int[] Money;
        public int[] Stone;

        public int MaxHp;
    }


}
