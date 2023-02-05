using System;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public class LvlData
    {
        #region Fields

        [SerializeField]
        private int lvlIndex;
        [SerializeField]
        private int breakedWalls;
        [SerializeField]
        private int coinsRaw;
        [SerializeField]
        private int score;

        #endregion

        #region Propeties

        public int LvlIndex { get => lvlIndex; set => lvlIndex = value; }
        public int BreakedWalls { get => breakedWalls; set => breakedWalls = value; }
        public int CoinsRaw { get => coinsRaw; set => coinsRaw = value; }
        public int Score { get => score; set => score = value; }


        #endregion

        #region Methods

        public LvlData()
        {
        }

        public LvlData(int lvlIndex, int breakedWalls, int coinsRaw, int score)
        {
            LvlIndex = lvlIndex;
            BreakedWalls = breakedWalls;
            CoinsRaw = coinsRaw;
            Score = score;
        }

        #endregion

        #region Enums



        #endregion
    }
}
