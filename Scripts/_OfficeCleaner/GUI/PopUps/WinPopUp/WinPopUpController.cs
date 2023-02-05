using UnityEngine;
using System.Collections;
//using GeekBox.Ads;
using GeekBox.Utils;

[RequireComponent(typeof(WinPopUpModel), typeof(WinPopUpView))]
public class WinPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties

    private WinPopUpModel CurrentModel { get; set; }

    #endregion

    #region Methods

    public void SetData(int wallsBreaked)
    {
        CurrentModel.SetData(wallsBreaked);
    }

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<WinPopUpModel>();
    }

    public void OnADRewardClick()
    {
        //EasyMobileManager.Instance.ShowRewardedAD(
        //    () => {
        //        CurrentModel.AddReward(CurrentModel.GetADReward());
        //        CurrentModel.ShowMainUIPopUp();
        //        ClosePopUp();
        //},
        //    () => {
        //        AndroidUtils.ShowToast("Video not ready!");
        //    });
    }

    public void OnNormalRewardClick()
    {
        CurrentModel.AddReward(CurrentModel.GetNormalReward());
        CurrentModel.ShowMainUIPopUp();
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
