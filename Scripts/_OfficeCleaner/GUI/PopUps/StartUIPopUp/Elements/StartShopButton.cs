using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class StartShopButton : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image backgroundImg;
    [SerializeField]
    private Sprite normalBackgournSprite;
    [SerializeField]
    private Sprite adBackgroundSprite;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI priceText;

    #endregion

    #region Propeties

    public Image BackgroundImg { get => backgroundImg; }
    public Sprite NormalBackgournSprite { get => normalBackgournSprite; }
    public Sprite AdBackgroundSprite { get => adBackgroundSprite; }
    public TextMeshProUGUI TitleText { get => titleText; }
    public TextMeshProUGUI PriceText { get => priceText; }

    #endregion

    #region Methods

    public void RefreshView(bool canBuy)
    {
        if(canBuy == true)
        {
            SetNormalView();
        }
        else
        {
            SetRewardedView();
        }
    }

    private void SetNormalView()
    {
        BackgroundImg.sprite = NormalBackgournSprite;
        PriceText.gameObject.SetActive(true);
    }

    private void SetRewardedView()
    {
        BackgroundImg.sprite = AdBackgroundSprite;
        PriceText.gameObject.SetActive(false);
    }

    #endregion

    #region Enums



    #endregion
}
