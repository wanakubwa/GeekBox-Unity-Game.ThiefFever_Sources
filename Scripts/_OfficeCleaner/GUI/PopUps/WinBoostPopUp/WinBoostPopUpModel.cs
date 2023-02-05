using UnityEngine;

public class WinBoostPopUpModel : PopUpModel
{
    #region Fields

    [SerializeField]
    private float rewardMultiplier = 2f;

    #endregion

    #region Propeties


    public float RewardMultiplier { get => rewardMultiplier; }

    private int PlayerZombiesAlive { get; set; }

    private WinBoostPopUpView CurrentView { get; set; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentView = GetView<WinBoostPopUpView>();
        PlayerZombiesAlive = 0; // todo;
    }

    public int GetBaseReward()
    {
        return (int)(PlayerZombiesAlive * RewardMultiplier);
    }

    public int GetBoostedReward()
    {
        return GetBaseReward() * CurrentView.Slider.CurrentMultiplier;
    }

    public void ShowMainUIPopUp()
    {
        GamePlayManager.Instance.LoadNextLvl();
        PopUpManager.Instance.ShowStartUIPopUp();
    }

    public void LoadNextLvl()
    {
        GamePlayManager.Instance.LoadNextLvl();
    }

    public void AddReward(int ammount)
    {
        PlayerManager.Instance.Wallet.AddCoins(ammount);
    }

    #endregion

    #region Enums



    #endregion
}
