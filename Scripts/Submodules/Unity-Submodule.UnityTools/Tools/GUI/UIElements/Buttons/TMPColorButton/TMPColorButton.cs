using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TMPColorButton : ButtonBase
{
    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private Image targetImage;

    [Space, Title("Colors settings")]
    [SerializeField]
    private float durationTime = 0.15f;
    [SerializeField]
    private float resetTime = 0.10f;
    [SerializeField]
    private Color selectColor;

    #endregion

    #region Propeties

    public TextMeshProUGUI ButtonText { 
        get => buttonText; 
        private set => buttonText = value; 
    }

    public Color SelectColor { 
        get => selectColor; 
        private set => selectColor = value;
    }

    public float DurationTime { 
        get => durationTime; 
        private set => durationTime = value; 
    }

    public float ResetTime { 
        get => resetTime; 
        private set => resetTime = value; 
    }

    public Image TargetImage { 
        get => targetImage;
        private set => targetImage = value; 
    }

    #endregion

    #region Methods

    public override void OnSelected()
    {
        base.OnSelected();

        ButtonText.DOColor(SelectColor, DurationTime);
        TargetImage.DOColor(SelectColor, DurationTime);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        ButtonText.DOColor(SelectColor, DurationTime);
        TargetImage.DOColor(SelectColor, DurationTime);
        gameObject.transform.DOScale(0.95f, DurationTime);
    }

    public override void ResetButton()
    {
        ButtonText.DOColor(Color.white, ResetTime);
        TargetImage.DOColor(Color.white, ResetTime);
        gameObject.transform.DOScale(1, ResetTime);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        ButtonText.DOKill();
        TargetImage.DOKill();
        gameObject.transform.DOKill();
    }

    #endregion

    #region Handlers

    #endregion
}
