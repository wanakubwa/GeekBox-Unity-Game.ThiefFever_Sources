using UnityEngine;
using System.Collections;
using PlayerData;
//using GeekBox.Ads;

public class WinPopUpModel : PopUpModel
{
    #region Fields

    [SerializeField]
    private int adMultiplayFactor = 5;

    #endregion

    #region Propeties

    public int ADMultiplayFactor { get => adMultiplayFactor;  }
    public PlayerWallet Wallet { get; private set; }

    public int WallsAmmount { get; private set; }

    #endregion

    #region Methods

    public void SetData(int walls)
    {
        WallsAmmount = walls;
    }

    public override void Initialize()
    {
        base.Initialize();

        Wallet = PlayerManager.Instance.Wallet;
    }

    public int GetADReward()
    {
        return GetNormalReward() * ADMultiplayFactor;
    }

    public int GetNormalReward()
    {
        return Wallet.CompletedLvls[Wallet.CompletedLvls.Count - 1].CoinsRaw;
    }

    public void ShowMainUIPopUp()
    {
        GamePlayManager.Instance.LoadNextLvl();
        PopUpManager.Instance.ShowStartUIPopUp();
    }

    public void AddReward(int ammount)
    {
        PlayerManager.Instance.Wallet.AddCoins(ammount);
    }

    public int GetTotalScore()
    {
        return Wallet.GetTotalScore();
    }

    #endregion

    #region Enums



    #endregion
}
