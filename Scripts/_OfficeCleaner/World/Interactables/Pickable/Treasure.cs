using StackSystem;
using UnityEngine;

public class Treasure : StackableObject
{
    #region Fields

    [SerializeField]
    private int priceValue;

    #endregion

    #region Propeties

    public int PriceValue { get => priceValue; }

    #endregion

    #region Methods

    public override void DestroyObject()
    {
        MapsManager.Instance.CurrentMap.AddMoney(PriceValue);

        base.DestroyObject();
    }

    #endregion

    #region Enums



    #endregion
}
