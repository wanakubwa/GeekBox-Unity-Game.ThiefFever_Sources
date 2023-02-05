using System;
using UnityEngine;

public class GameplayEvents : ManagerSingletonBase<GameplayEvents>
{
    #region Fields

    #endregion

    #region Propeties

    public event Action<ToolType> OnToolPickup = delegate { };
    public event Action<float> OnMapTimeSUpdate = delegate { };
    public event Action OnFurnitureCollapsed = delegate { };
    public event Action<string> OnKeyColected = delegate { };

    /// <summary>
    /// vector3 - worldposition; int - ilosc
    /// </summary>
    public event Action<Vector3, int> OnCollectMoney = delegate { };

    #endregion

    #region Methods

    public void NotifyOnKeyCollected(string keyLabel)
    {
        OnKeyColected(keyLabel);
    }

    public void NotifyOnFurnitureCollapse()
    {
        OnFurnitureCollapsed();
    }

    public void NotifyPickupTool(ToolType toolType)
    {
        OnToolPickup(toolType);
    }

    public void NotifiMapTimeUpdate(float leftTimeS)
    {
        OnMapTimeSUpdate(leftTimeS);
    }

    #endregion

    #region Enums



    #endregion
}
