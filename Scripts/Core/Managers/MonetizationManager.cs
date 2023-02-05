using UnityEngine;
using System.Collections;

public class MonetizationManager : ManagerSingletonBase<MonetizationManager>
{
    #region Fields

    [SerializeField]
    private int interstitialPeriod = 2;

    [SerializeField]
    private float showBannerADDelayS = 3f;

    #endregion

    #region Propeties

    public int InterstitialPeriod { 
        get => interstitialPeriod; 
    }

    private int LvlTryCounter { get; set; } = Constants.DEFAULT_VALUE;
    public float ShowBannerADDelayS { get => showBannerADDelayS; }

    #endregion

    #region Methods

    public override void AttachEvents()
    {
        base.AttachEvents();

        GamePlayManager.Instance.OnLvlSuccess += OnLvlSuccessHandler;
        GamePlayManager.Instance.OnLvlFailed += OnLvlFailedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnLvlSuccess -= OnLvlSuccessHandler;
            GamePlayManager.Instance.OnLvlFailed -= OnLvlFailedHandler;
        }
    }

    private void TryShowInterstitialAd()
    {
        LvlTryCounter++;
        if(LvlTryCounter > InterstitialPeriod)
        {
            //GeekBox.Ads.EasyMobileManager.Instance.ShowInterstitialAD();
            LvlTryCounter = Constants.DEFAULT_VALUE;
        }
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(_WaitAndShowBannerAD());
    }

    // Handlers.
    private void OnLvlFailedHandler()
    {
        TryShowInterstitialAd();
    }

    private void OnLvlSuccessHandler(int obj)
    {
        TryShowInterstitialAd();
    }

    private IEnumerator _WaitAndShowBannerAD()
    {
        yield return new WaitForSeconds(ShowBannerADDelayS);
        //GeekBox.Ads.EasyMobileManager.Instance.ShowBottomBannerAD();
    }

    #endregion

    #region Enums



    #endregion
}
