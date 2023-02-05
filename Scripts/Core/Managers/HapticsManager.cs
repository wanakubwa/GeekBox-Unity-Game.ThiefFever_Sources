using GeekBox.Utils;
using System;
using UnityEngine;

public class HapticsManager : ManagerSingletonBase<HapticsManager>
{
    #region Fields

    [SerializeField]
    private int boltAmplitude = 15;
    [SerializeField]
    private int coinAmplitude = 10;
    [SerializeField]
    private int gateAmplitude = 50;
    [SerializeField]
    private int deathAmplitude = 250;
    [SerializeField]
    private int wallAmplitude = 100;

    #endregion

    #region Propeties

    public event Action<bool> OnVibrationStateChange = delegate { };

    public int BoltAmplitude {
        get => boltAmplitude; 
    }
    public int CoinAmplitude
    {
        get => coinAmplitude;
    }
    public int GateAmplitude {
        get => gateAmplitude;
    }
    public int DeathAmplitude {
        get => deathAmplitude; 
    }
    public int WallAmplitude { 
        get => wallAmplitude;
    }

    public bool IsVibrationEnabled { get; private set; } = true;

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        SetVibrationState(UserSettingsManager.Instance.IsVibrationEnabled);
    }

    public void SetVibrationState(bool isOn)
    {
        IsVibrationEnabled = isOn;
        UserSettingsManager.Instance.IsVibrationEnabled = IsVibrationEnabled;
        OnVibrationStateChange(IsVibrationEnabled);
    }

    public void ToggleVibrationState()
    {
        SetVibrationState(!IsVibrationEnabled);
    }

    public void TryVibrate(int amplitude)
    {
        if(IsVibrationEnabled == true)
        {
            AndroidUtils.Vibrate(amplitude);
        }
    }

    #endregion

    #region Enums



    #endregion
}
