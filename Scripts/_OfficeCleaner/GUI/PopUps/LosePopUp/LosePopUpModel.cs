using UnityEngine;
using System.Collections;

public class LosePopUpModel : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void ResetCurrentLvl()
    {
        GamePlayManager.Instance.ResetLvl();
    }

    #endregion

    #region Enums



    #endregion
}
