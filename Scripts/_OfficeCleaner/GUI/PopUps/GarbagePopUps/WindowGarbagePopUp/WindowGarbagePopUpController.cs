using System;
using UnityEngine;

[RequireComponent(typeof(WindowGarbagePopUpModel), typeof(WindowGarbagePopUpView))]
public class WindowGarbagePopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void SetData(int stainsAmmount, Action onCompleted)
    {
        WindowGarbagePopUpModel currentModel = GetComponent<WindowGarbagePopUpModel>();
        currentModel.TotalStains = stainsAmmount;
        currentModel.OnCompletedCallback = onCompleted;
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GamePlayManager.Instance.OnLvlFailed += OnLvlFailedHandler;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnLvlFailed += OnLvlFailedHandler;
        }
    }

    private void OnLvlFailedHandler()
    {
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
