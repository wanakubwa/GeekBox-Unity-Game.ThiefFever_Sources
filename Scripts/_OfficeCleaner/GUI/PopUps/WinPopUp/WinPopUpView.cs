using UnityEngine;
using System.Collections;
using TMPro;

public class WinPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI adCoinsRewardText;
    [SerializeField]
    private TextMeshProUGUI normalCoinsRewardText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private LeaderboardDisplayer leaderboard;

    #endregion

    #region Propeties

    public TextMeshProUGUI AdCoinsRewardText { get => adCoinsRewardText; }
    public TextMeshProUGUI NormalCoinsRewardText { get => normalCoinsRewardText; }
    public TextMeshProUGUI ScoreText { get => scoreText; }
    public LeaderboardDisplayer Leaderboard { get => leaderboard; }
    private WinPopUpModel CachedModel { get; set; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CachedModel = GetModel<WinPopUpModel>();

        ScoreText.SetText(CachedModel.GetTotalScore().ToString());
        AdCoinsRewardText.SetText(CachedModel.GetADReward().ToString());
        NormalCoinsRewardText.SetText(CachedModel.GetNormalReward().ToString());
        Leaderboard.Init(CachedModel.GetTotalScore());
    }

    #endregion

    #region Enums



    #endregion
}
