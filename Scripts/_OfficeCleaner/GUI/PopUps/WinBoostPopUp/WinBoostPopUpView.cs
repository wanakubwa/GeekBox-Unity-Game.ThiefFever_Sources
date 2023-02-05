using System.Collections;
using TMPro;
using UnityEngine;

public class WinBoostPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private BoostSlider slider;
    [SerializeField]
    private GameObject normalButton;
    [SerializeField]
    private TextMeshProUGUI coinsAmmountText;
    [SerializeField]
    private float delayNormalButtonS = 8f;

    #endregion

    #region Propeties

    public BoostSlider Slider { 
        get => slider;
    }
    public TextMeshProUGUI CoinsAmmountText {
        get => coinsAmmountText; 
    }
    public float DelayNormalButtonS { 
        get => delayNormalButtonS;
    }
    public GameObject NormalButton { 
        get => normalButton;
    }

    private WinBoostPopUpModel CurrentModel { get; set; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<WinBoostPopUpModel>();
        OnMultiplierChangedHandler(Slider.CurrentMultiplier);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        Slider.OnMultiplierChanged += OnMultiplierChangedHandler;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        Slider.OnMultiplierChanged -= OnMultiplierChangedHandler;
    }

    private void OnMultiplierChangedHandler(int value)
    {
        CoinsAmmountText.SetText(CurrentModel.GetBoostedReward().ToString());
    }

    public override void CustomStart()
    {
        base.CustomStart();

        StartCoroutine(_WaitAndEnable(NormalButton, DelayNormalButtonS));
    }

    private IEnumerator _WaitAndEnable(GameObject target, float delay)
    {
        target.SetActive(false);
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }

    #endregion

    #region Enums



    #endregion
}
