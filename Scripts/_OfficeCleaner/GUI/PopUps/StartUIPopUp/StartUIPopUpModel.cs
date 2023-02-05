using GeekBox.Utils;
using PlayerData;
using UnityEngine;

public class StartUIPopUpModel : PopUpModel
{
    #region Fields

    [SerializeField]
    private int startUnitsMod;
    [SerializeField]
    private int incomeMod;
    [SerializeField]
    private int unitsBoostMod = 5;

    [Space]
    [SerializeField]
    private int startUnitsPriceMod;
    [SerializeField]
    private int incomePriceMod;

    #endregion

    #region Propeties

    public PlayerWallet CachedWallet { get => PlayerManager.Instance.Wallet; }

    private bool WasSpeedLimitAD { get; set; } = false;
    private bool WasStartSpeedAD { get; set; } = false;
    private bool WasBoostUnitsAD { get; set; } = false;
    public int UnitsBoostMod { get => unitsBoostMod; }

    #endregion

    #region Methods

    public void StartGame()
    {
        GamePlayManager.Instance.StartLvlGame();
    }

    public int GetCurrentLvlNo()
    {
        return CachedWallet.CurrentLvlNo;
    }

    public int GetFirstUpgradeLvl()
    {
        return CachedWallet.StartUnitsBoost / startUnitsMod;
    }

    public int GetSecondUpgradeLvl()
    {
        return CachedWallet.IncomeBoost / incomeMod;
    }

    public int GetFirstUpgradePrice()
    {
        return GetFirstUpgradeLvl() * startUnitsPriceMod + startUnitsPriceMod;
    }

    public int GetSecondUpgradePrice()
    {
        return GetSecondUpgradeLvl() * incomePriceMod + incomePriceMod;
    }

    public int GetNextSpeedLimitBoost()
    {
        return CachedWallet.StartUnitsBoost + startUnitsMod;
    }

    public int GetNextStartSpeedBoost()
    {
        return CachedWallet.IncomeBoost + incomePriceMod;
    }

    public bool CanPlayerBuyFirstUpgrade()
    {
        return CachedWallet.CanAfford(GetFirstUpgradePrice());
    }

    public bool CanPlayerBuySecondUpgrade()
    {
        return CachedWallet.CanAfford(GetSecondUpgradePrice());
    }

    public void TryBuyLeftUpgrade()
    {
        if(CanPlayerBuyFirstUpgrade() == true)
        {
            int price = GetFirstUpgradePrice();
            CachedWallet.AddStartUnitsBoost(startUnitsMod);
            CachedWallet.AddCoins(-price);
        }
        else if(WasSpeedLimitAD == false)
        {
            //GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() =>
            //{
            //    WasSpeedLimitAD = true;
            //    CachedWallet.AddStartUnitsBoost(startUnitsMod);
            //});
        }
    }

    public void TryBuyRightUpgrade()
    {
        if (CanPlayerBuySecondUpgrade() == true)
        {
            int price = GetSecondUpgradePrice();
            CachedWallet.AddIncomeBoost(incomeMod);
            CachedWallet.AddCoins(-price);
        }
        else if(WasStartSpeedAD == false)
        {
            //GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() =>
            //{
            //    WasStartSpeedAD = true;
            //    CachedWallet.AddIncomeBoost(incomePriceMod);
            //});
        }
    }

    public void TryOneRunUnitsBoost()
    {
        //GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() =>
        //{
        //    WasBoostUnitsAD = true;
        //    PlayerManager.Instance.CurrentPlayer.AddBalls(UnitsBoostMod);
        //});
    }

    public void ShowSkinsShop()
    {
        //PopUpManager.Instance.ShowShopPopUp();
    }

    public void ShowSettingsPopUp()
    {
        PopUpManager.Instance.ShowSettingsPopUp();
    }

    public bool IsVibrationsEnabled()
    {
        return HapticsManager.Instance.IsVibrationEnabled;
    }

    public bool IsAudioEnabled()
    {
        return !AudioManager.Instance.IsAudioMute;
    }

    public void ToggleVibrationsState()
    {
        HapticsManager.Instance.ToggleVibrationState();
    }

    public void ToggleAudioState()
    {
        AudioManager.Instance.ToggleIsAudioMute();
    }

    #endregion

    #region Enums



    #endregion
}
