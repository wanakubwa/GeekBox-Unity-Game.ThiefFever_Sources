using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LosePopUpModel), typeof(LosePopUpView))]
public class LosePopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void RetryCurrentLvl()
    {
        GetModel<LosePopUpModel>().ResetCurrentLvl();
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
