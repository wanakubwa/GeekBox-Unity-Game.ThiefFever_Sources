using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SettingsPopUpModel), typeof(SettingsPopUpView))]
public class SettingsPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnGdprShow()
    {
        //GeekBox.Ads.EasyMobileManager.Instance.ShowGdprDialog(true);
    }

    public void OnResetButtonClick()
    {
        GetModel<SettingsPopUpModel>().ResetGame();
    }

    #endregion

    #region Enums



    #endregion
}
