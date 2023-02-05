using UnityEngine;
using System.Collections;
using GeekBox.LeaderboardsSystem;
using System.Collections.Generic;

public class GameLeaderboardManager : ManagerSingletonBase<GameLeaderboardManager>
{
    #region Fields



    #endregion

    #region Propeties

    private LeaderboardDatabase Data { get; set; }
    private ReverseComparer ScoreComparer { get; set; } = new ReverseComparer();

    #endregion

    #region Methods

    public ScoreRecord GetRecordAtPosition(int position)
    {
        return Data.Records.GetElementAtIndexSafe(position - 1);
    }

    public int GetPositionForScore(int score)
    {
        return GetIndexForScore(score) + 1;
    }

    public override void Initialize()
    {
        base.Initialize();

        Data = LeaderboardDatabase.Instance;
    }

    private int GetIndexForScore(int score)
    {
        ScoreRecord record = new ScoreRecord(score, string.Empty);
        int index = Data.Records.BinarySearch(record, ScoreComparer);

        return index < 0 ? ~index : index;
    }

    #endregion

    #region Enums

    public class ReverseComparer : IComparer<ScoreRecord>
    {
        public int Compare(ScoreRecord x, ScoreRecord y)
        {
            return y.CompareTo(x);
        }
    }

    #endregion
}
