using UnityEngine;

[RequireComponent(typeof(StartUIPopUpModel), typeof(StartUIPopUpView))]
public class StartUIPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnGameStart()
    {
        GetModel<StartUIPopUpModel>().StartGame();
        ClosePopUp();
    }

    public void BuyLeftUpgrade()
    {
        GetModel<StartUIPopUpModel>().TryBuyLeftUpgrade();
    }

    public void BuyRightUpgrade()
    {
        GetModel<StartUIPopUpModel>().TryBuyRightUpgrade();
    }

    public void OnSkinsButtonClick()
    {
        GetModel<StartUIPopUpModel>().ShowSkinsShop();
    }

    public void OnOptionButtonClick()
    {
        GetModel<StartUIPopUpModel>().ShowSettingsPopUp();
    }

    public void ToggleVibrationState()
    {
        GetModel<StartUIPopUpModel>().ToggleVibrationsState();
    }

    public void ToggleAudioState()
    {
        GetModel<StartUIPopUpModel>().ToggleAudioState();
    }

    public void OnOneRunUnitsBoostClick()
    {
        GetModel<StartUIPopUpModel>().TryOneRunUnitsBoost();
    }

    #endregion

    #region Enums



    #endregion
}
