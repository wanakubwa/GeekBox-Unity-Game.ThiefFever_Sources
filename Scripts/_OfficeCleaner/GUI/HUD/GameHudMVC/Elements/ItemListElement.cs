using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ItemListElement : MonoBehaviour
{
    #region Fields

    [Header("Ticker")]
    [SerializeField]
    private Image tickerImg;
    [SerializeField]
    private Sprite onTickerImg;
    [SerializeField]
    private Sprite offTickerImg;

    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI ammaountText;
    [SerializeField]
    private string textFormat = "{0}/{1}";

    [Header("Icon")]
    [SerializeField]
    private Image itemImg;
    [SerializeField]
    private SerializableDictionary<GarbageType, Sprite> garbageIconMap = new SerializableDictionary<GarbageType, Sprite>();


    #endregion

    #region Propeties

    public Image TickerImg { get => tickerImg; }
    public Sprite OnTickerImg { get => onTickerImg; }
    public Sprite OffTickerImg { get => offTickerImg; }
    public TextMeshProUGUI AmmountText { get => ammaountText; }
    public string TextFormat { get => textFormat; }
    public Image ItemImg { get => itemImg; }
    public SerializableDictionary<GarbageType, Sprite> GarbageIconMap { get => garbageIconMap; }

    public GarbageType TypeOfGarbage { get; set; } = GarbageType.NONE;
    public int MaxAmmount { get; set; } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    /// <summary>
    /// Inicjalizacja wartosci.
    /// </summary>
    public void SetInfo(GarbageType garbage, int totalAmmount)
    {
        TypeOfGarbage = garbage;
        MaxAmmount = totalAmmount;

        AmmountText.SetText(string.Format(TextFormat, Constants.DEFAULT_VALUE, totalAmmount));
        TickerImg.sprite = OffTickerImg;

        RefreshIcon();
    }

    public void RefreshInfo(int currentAmmount)
    {
        AmmountText.SetText(string.Format(TextFormat, MaxAmmount - currentAmmount, MaxAmmount));
        if(currentAmmount == Constants.DEFAULT_VALUE)
        {
            TickerImg.sprite = OnTickerImg;
        }
    }

    private void RefreshIcon()
    {
        if(GarbageIconMap.TryGetValue(TypeOfGarbage, out Sprite icon) == true)
        {
            ItemImg.sprite = icon;
        }
    }
    #endregion

    #region Enums



    #endregion
}
