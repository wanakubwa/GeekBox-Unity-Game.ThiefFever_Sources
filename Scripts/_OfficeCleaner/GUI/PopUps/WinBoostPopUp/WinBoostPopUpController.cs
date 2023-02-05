//using GeekBox.Ads;
using GeekBox.Utils;
using UnityEngine;

[RequireComponent(typeof(WinBoostPopUpModel), typeof(WinBoostPopUpView))]
public class WinBoostPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnNormalButtonClick()
    {
        WinBoostPopUpModel model = GetModel<WinBoostPopUpModel>();
        model.AddReward(model.GetBaseReward());
        model.LoadNextLvl();
        ClosePopUp();
    }

    public void OnBoostButtonClick()
    {
        WinBoostPopUpModel model = GetModel<WinBoostPopUpModel>();
        int boostedReward = model.GetBoostedReward();

        //EasyMobileManager.Instance.ShowRewardedAD(
        //    () =>
        //        {
        //            model.AddReward(boostedReward);
        //            model.ShowMainUIPopUp();
        //            ClosePopUp();
        //        },
        //    () =>
        //        {
        //            AndroidUtils.ShowToast("Video not ready!");
        //        });
    }

    #endregion

    #region Enums



    #endregion
}
