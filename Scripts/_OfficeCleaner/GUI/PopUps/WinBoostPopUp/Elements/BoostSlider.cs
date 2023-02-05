using System;
using UnityEngine;

public class BoostSlider : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private int defaultMultiplier = 2;

    #endregion

    #region Propeties

    public event Action<int> OnMultiplierChanged = delegate { };

    public int DefaultMultiplier { get => defaultMultiplier; }

    public int CurrentMultiplier { get; set; }

    #endregion

    #region Methods

    public void SetMultiplier(int value)
    {
        CurrentMultiplier = value;
        OnMultiplierChanged(CurrentMultiplier);
    }

    private void Awake()
    {
        CurrentMultiplier = DefaultMultiplier;
    }

    #endregion

    #region Enums



    #endregion
}
