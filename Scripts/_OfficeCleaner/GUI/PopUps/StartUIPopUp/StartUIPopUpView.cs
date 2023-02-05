using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUIPopUpView : PopUpView
{
    #region Fields

    private const string BOOST_FORMAT = "+{0}";

    [SerializeField]
    private TextMeshProUGUI playerCoinsText;

    [Header("Formats")]
    [SerializeField]
    private string priceTextFormat = "{0}";
    [SerializeField]
    private string lvlTextFormat = "Lvl {0}";

    [Header("Left Button")]
    [SerializeField]
    private StartShopButton leftButton;
    [SerializeField]
    private TextMeshProUGUI leftPrice;
    [SerializeField]
    private TextMeshProUGUI leftLvlText;

    [Header("Right Button")]
    [SerializeField]
    private StartShopButton rightButton;
    [SerializeField]
    private TextMeshProUGUI rightPrice;
    [SerializeField]
    private TextMeshProUGUI rightLvlText;

    [Header("Vibration Button")]
    [SerializeField]
    private Button vibrationButton;
    [SerializeField]
    private Sprite vibrationOnTex;
    [SerializeField]
    private Sprite vibrationOffTex;

    [Header("Audio Button")]
    [SerializeField]
    private Button audioButton;
    [SerializeField]
    private Sprite audioOnTex;
    [SerializeField]
    private Sprite audioOffTex;

    #endregion

    #region Propeties

    private StartUIPopUpModel CurrentModel { get; set; }

    public StartShopButton LeftButton { get => leftButton; }
    public StartShopButton RightButton { get => rightButton; }
    public TextMeshProUGUI LeftPrice { get => leftPrice; }
    public TextMeshProUGUI RightPrice { get => rightPrice; }

    public TextMeshProUGUI PlayerCoinsText { get => playerCoinsText; }
    public Button VibrationButton { get => vibrationButton; }
    public Sprite VibrationOnTex { get => vibrationOnTex; }
    public Sprite VibrationOffTex { get => vibrationOffTex; }
    public Button AudioButton { get => audioButton; }
    public Sprite AudioOnTex { get => audioOnTex; }
    public Sprite AudioOffTex { get => audioOffTex; }

    public TextMeshProUGUI LeftLvlText { get => leftLvlText; }
    public TextMeshProUGUI RightLvlText { get => rightLvlText; }

    public string PriceTextFormat { get => priceTextFormat; }
    public string LvlTextFormat { get => lvlTextFormat; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<StartUIPopUpModel>();

        RefreshView();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        PlayerManager.Instance.Wallet.OnCoinsChange += OnCoinsChanedHandler;
        HapticsManager.Instance.OnVibrationStateChange += RefreshVibrationButton;
        AudioManager.Instance.OnVolumeChanged += OnVolumeChangedHandler;
        SaveLoadManager.Instance.OnResetCompleted += RefreshView;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnCoinsChange -= OnCoinsChanedHandler;
        }

        if(HapticsManager.Instance != null)
        {
            HapticsManager.Instance.OnVibrationStateChange -= RefreshVibrationButton;
        }

        if(SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= RefreshView;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnVolumeChanged -= OnVolumeChangedHandler;
        }
    }

    private void RefreshView()
    {
        RefreshFirstUpgradeButtons();
        RefreshSecondUpgradeButtons();
        RefreshCoins(CurrentModel.CachedWallet.Coins);
        RefreshVibrationButton(CurrentModel.IsVibrationsEnabled());
        RefreshAudioButton(CurrentModel.IsAudioEnabled());
    }

    private void RefreshVibrationButton(bool isOn)
    {
        VibrationButton.image.sprite = isOn == true ? VibrationOnTex : VibrationOffTex;
    }

    private void RefreshAudioButton(bool isOn)
    {
        AudioButton.image.sprite = isOn == true ? AudioOnTex : AudioOffTex;
    }

    private void RefreshCoins(int ammount)
    {
        PlayerCoinsText.SetText(ammount.ToString());
    }

    private void RefreshFirstUpgradeButtons()
    {
        bool canPlayerBuy = CurrentModel.CanPlayerBuyFirstUpgrade();
        LeftButton.RefreshView(canPlayerBuy);

        LeftPrice.SetText(string.Format(PriceTextFormat, CurrentModel.GetFirstUpgradePrice()));
        LeftLvlText.SetText(string.Format(LvlTextFormat, CurrentModel.GetFirstUpgradeLvl() + 1));
    }

    private void RefreshSecondUpgradeButtons()
    {
        bool canPlayerBuy = CurrentModel.CanPlayerBuySecondUpgrade();
        RightButton.RefreshView(canPlayerBuy);

        RightPrice.SetText(string.Format(PriceTextFormat, CurrentModel.GetSecondUpgradePrice()));
        RightLvlText.SetText(string.Format(LvlTextFormat, CurrentModel.GetSecondUpgradeLvl() + 1));
    }

    private void OnCoinsChanedHandler(int ammount)
    {
        RefreshCoins(ammount);
        RefreshFirstUpgradeButtons();
        RefreshSecondUpgradeButtons();
    }

    private void OnVolumeChangedHandler(float obj)
    {
        RefreshAudioButton(CurrentModel.IsAudioEnabled());
    }

    #endregion

    #region Enums



    #endregion
}