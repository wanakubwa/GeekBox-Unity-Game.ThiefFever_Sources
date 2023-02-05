using System;
using UnityEngine;

namespace GeekBox.LeaderboardsSystem
{
    [Serializable]
    public class ScoreRecord : IEquatable<ScoreRecord>, IComparable<ScoreRecord>
    {
        #region Fields

        [SerializeField]
        private int score;
        [SerializeField]
        private string userName;

        public int Score { get => score; }
        public string UserName { get => userName; }

        #endregion

        #region Propeties



        #endregion

        #region Methods

        public ScoreRecord()
        {
        }

        public ScoreRecord(int score, string userName)
        {
            this.score = score;
            this.userName = userName;
        }

        public bool Equals(ScoreRecord other)
        {
            return Score == other.score;
        }

        public int CompareTo(ScoreRecord other)
        {
            return Score.CompareTo(other.Score);
        }

        #endregion

        #region Enums



        #endregion
    }
}
