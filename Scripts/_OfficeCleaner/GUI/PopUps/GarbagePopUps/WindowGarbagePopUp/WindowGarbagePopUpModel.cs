using UnityEngine;
using GarbagePopUps.WindowGarbagePopUp;
using System;

public class WindowGarbagePopUpModel : PopUpModel
{
    #region Fields

    [SerializeField]
    private SpawnStainWaypoint[] stainsWaypoints;

    #endregion

    #region Propeties

    public SpawnStainWaypoint[] StainsWaypoints { 
        get => stainsWaypoints;
    }

    public int TotalStains { get; set; } = Constants.DEFAULT_VALUE;
    public Action OnCompletedCallback { get; set; } = delegate { };

    #endregion

    #region Methods

    public void OnStainsRemoved()
    {
        OnCompletedCallback();
        GetController<WindowGarbagePopUpController>().ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
