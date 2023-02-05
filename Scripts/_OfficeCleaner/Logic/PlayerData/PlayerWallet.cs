using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public class PlayerWallet
    {
        #region Fields

        [SerializeField]
        private int coins;
        [SerializeField]
        private int currentLvlNo = Constants.DEFAULT_NO_VALUE;

        [SerializeField]
        private int startUnitsBoost;
        [SerializeField]
        private int incomeBoost;

        [SerializeField]
        private List<LvlData> completedLvls = new List<LvlData>();
        [SerializeField]
        private List<int> unlockedSkinsIds = new List<int>();

        #endregion

        #region Propeties

        /// <summary>
        /// int - aktualna liczba monet.
        /// </summary>
        public event Action<int> OnCoinsChange = delegate { };

        /// <summary>
        /// int - aktualny nr poziomu.
        /// </summary>
        public event Action<int> OnLvlNoChange = delegate { };

        /// <summary>
        /// int - o ile nastapila zmiana wartosci.
        /// </summary>
        public event Action<int> OnStartUnitsChanged = delegate { };

        public event Action OnUnlockedSkinsChanged = delegate { };

        public int Coins { 
            get => coins;
            private set => coins = value;
        }
        public int CurrentLvlNo {
            get => currentLvlNo;
            private set => currentLvlNo = value;
        }
        public int StartUnitsBoost { get => startUnitsBoost; private set => startUnitsBoost = value; }
        public int IncomeBoost { get => incomeBoost; private set => incomeBoost = value; }
        public List<LvlData> CompletedLvls { get => completedLvls; private set => completedLvls = value; }
        public List<int> UnlockedSkinsIds { get => unlockedSkinsIds; private set => unlockedSkinsIds = value; }

        // Accessors
        public int NextLvlNo { get => CompletedLvls.Count + 1; }

        #endregion

        #region Methods

        public PlayerWallet() { }

        public void SetDefaultData()
        {
            SetCoins(Constants.DEFAULT_VALUE);
            SetCurrentLvlNo(Constants.DEFAULT_NO_VALUE);
            this.StartUnitsBoost = Constants.DEFAULT_VALUE;
            this.IncomeBoost = Constants.DEFAULT_VALUE;
            this.CompletedLvls = new List<LvlData>();
            this.UnlockedSkinsIds = new List<int>();
        }

        public void Load(PlayerWallet source)
        {
            SetCoins(source.coins);
            SetCurrentLvlNo(source.CurrentLvlNo);
            this.StartUnitsBoost = source.StartUnitsBoost;
            this.IncomeBoost = source.IncomeBoost;
            this.CompletedLvls = new List<LvlData>(source.CompletedLvls);
            this.UnlockedSkinsIds = new List<int>(source.UnlockedSkinsIds);
        }

        public void SetCoins(int ammount)
        {
            Coins = ammount;
            OnCoinsChange(Coins);
        }

        public void AddUnlockedSkin(int id)
        {
            UnlockedSkinsIds.Add(id);
            OnUnlockedSkinsChanged();
        }

        public void AddLvlData(LvlData data)
        {
            CompletedLvls.Add(data);
        }

        public void AddCoins(int value)
        {
            if(value > Constants.DEFAULT_VALUE)
            {
                value += Mathf.FloorToInt(value * IncomeBoost * Constants.PERCENT_TO_DECIMAL_FACTOR);
            }
            
            SetCoins(Coins + value);
        }

        public void SetCurrentLvlNo(int no)
        {
            CurrentLvlNo = no;
            OnLvlNoChange(CurrentLvlNo);
        }

        public void AddStartUnitsBoost(int value)
        {
            StartUnitsBoost += value;
            OnStartUnitsChanged(value);
        }

        public void AddIncomeBoost(int value)
        {
            IncomeBoost += value;
        }

        public BallParameters GetBallParameters()
        {
            return new BallParameters(StartUnitsBoost);
        }

        public bool CanAfford(int coinsValue)
        {
            return Coins - coinsValue >= Constants.DEFAULT_VALUE;
        }

        public int GetTotalScore()
        {
            int score = 0;
            CompletedLvls.ForEach(x => score += x.Score);
            return score;
        }

        #endregion

        #region Enums



        #endregion
    }
}
