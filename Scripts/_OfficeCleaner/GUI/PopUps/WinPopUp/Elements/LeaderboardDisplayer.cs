using UnityEngine;
using GeekBox.LeaderboardsSystem;
using System.Collections.Generic;
using static LeaderboardRow;
using System.Linq;

public class LeaderboardDisplayer : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private LeaderboardRow rowPrefab;
    [SerializeField]
    private LeaderboardRow rowPrefabPlayer;
    [SerializeField]
    private RectTransform rowsParent;
    [SerializeField]
    private int recordsDisplayAbove = 5;
    [SerializeField]
    private int recordsDisplayBelow = 5;

    #endregion

    #region Propeties

    public LeaderboardRow RowPrefab
    {
        get => rowPrefab;
    }
    public LeaderboardRow RowPrefabPlayer { 
        get => rowPrefabPlayer;
    }

    public int RecordsDisplayAbove { get => recordsDisplayAbove; }
    public int RecordsDisplayBelow { get => recordsDisplayBelow; }
    public RectTransform RowsParent { get => rowsParent; }

    //Variables.
    private List<LeaderboardRow> SpawnedRows { get; set; } = new List<LeaderboardRow>();
    private GameLeaderboardManager CachedManager { get; set; }

    #endregion

    #region Methods

    public void Init(int playerScore)
    {
        SpawnedRows.ClearDestroy();

        CachedManager = GameLeaderboardManager.Instance;
        SpawnRows(playerScore);
    }

    private void SpawnRows(int playerScore)
    {
        int playerPosition = CachedManager.GetPositionForScore(playerScore);
        List<LeaderboardRecordData> recordsUp = new List<LeaderboardRecordData>();
        List<LeaderboardRecordData> recordsDown = new List<LeaderboardRecordData>();

        for (int i = 1; i <= RecordsDisplayAbove; i++)
        {
            // Wyniki lepsze niz gracza.
            ScoreRecord record = CachedManager.GetRecordAtPosition(playerPosition - i);
            if (record != null)
            {
                recordsUp.Add(new LeaderboardRecordData(playerPosition - i, record.Score, record.UserName));
            }

            // Wyniki gorsze.
            record = CachedManager.GetRecordAtPosition(playerPosition + i);
            if (record != null)
            {
                recordsDown.Add(new LeaderboardRecordData(playerPosition + i, record.Score, record.UserName));
            }
        }

        // Za malo wynikow z gory - doloz na dol.
        if (recordsUp.Count < RecordsDisplayAbove)
        {
            int delta = RecordsDisplayAbove - recordsUp.Count;

            for (int i = 1; i <= delta; i++)
            {
                int position = recordsDown.Last().No + 1;
                ScoreRecord record = CachedManager.GetRecordAtPosition(position);
                if (record != null)
                {
                    recordsDown.Add(new LeaderboardRecordData(position, record.Score, record.UserName));
                }
            }
        }

        // Za malo jest wynikow z dolu - doloz do gory.
        if (recordsDown.Count < recordsDisplayBelow)
        {
            int delta = recordsDisplayBelow - recordsDown.Count;
            for (int i = 1; i <= delta; i++)
            {
                int position = recordsUp.Last().No - 1;
                ScoreRecord record = CachedManager.GetRecordAtPosition(position);
                if (record != null)
                {
                    recordsUp.Add(new LeaderboardRecordData(position, record.Score, record.UserName));
                }
            }
        }

        recordsUp.Reverse();

        List<LeaderboardRecordData> recordsToDisplay = new List<LeaderboardRecordData>();
        recordsToDisplay.AddRange(recordsUp);
        recordsToDisplay.Add(new LeaderboardRecordData(playerPosition, playerScore, "YOU", true));
        recordsToDisplay.AddRange(recordsDown);

        foreach (LeaderboardRecordData recordDisplay in recordsToDisplay)
        {
            LeaderboardRow spawnedRow;
            if (recordDisplay.IsPlayer == false)
            {
                spawnedRow = Instantiate(RowPrefab);
            }
            else
            {
                spawnedRow = Instantiate(RowPrefabPlayer);
            }

            spawnedRow.transform.ResetParent(RowsParent);
            spawnedRow.SetInfo(recordDisplay);

            SpawnedRows.Add(spawnedRow);
        }
    }

    #endregion

    #region Enums



    #endregion
}
