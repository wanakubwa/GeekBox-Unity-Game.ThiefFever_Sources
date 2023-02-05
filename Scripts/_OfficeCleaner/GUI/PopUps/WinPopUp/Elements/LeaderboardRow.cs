using UnityEngine;
using System.Collections;
using TMPro;
using GeekBox.LeaderboardsSystem;

public class LeaderboardRow : MonoBehaviour
{
    #region Fields

    public const string NO_TEXT_FORMAT = "#{0}";

    [SerializeField]
    private TextMeshProUGUI positionText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI nameText;

    #endregion

    #region Propeties

    public TextMeshProUGUI PositionText { get => positionText; }
    public TextMeshProUGUI NameText { get => nameText; }
    public TextMeshProUGUI ScoreText { get => scoreText; }

    #endregion

    #region Methods

    public void SetInfo(LeaderboardRecordData record)
    {
        PositionText.SetText(string.Format(NO_TEXT_FORMAT, record.No));
        ScoreText.SetText(record.Score.ToStringKiloFormat());
        NameText.SetText(record.UserName);
    }

    #endregion

    #region Enums

    public class LeaderboardRecordData
    { 

        #region Fields

        public int No { get; set; }
        public int Score { get; set; }
        public string UserName { get; set; }
        public bool IsPlayer { get; set; } = false;

        #endregion

        #region Propeties

        public LeaderboardRecordData(int no, int score, string name)
        {
            No = no;
            Score = score;
            UserName = name;
        }

        public LeaderboardRecordData(int no, int score, string userName, bool isPlayer) : this(no, score, userName)
        {
            IsPlayer = isPlayer;
        }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }

    #endregion
}
