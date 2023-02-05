using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class UISwitch : ButtonBase
{
    #region Fields

    [Space]
    [SerializeField]
    private BoolUnityEvent onStateChanged;
    [SerializeField]
    private RectTransform handle;
    [SerializeField]
    private RectTransform indicator;
    [SerializeField]
    private float animationTimeS;

    [Space]
    [SerializeField]
    private TextMeshProUGUI onText;
    [SerializeField]
    private TextMeshProUGUI offText;

    #endregion

    #region Propeties

    private float OnPosition
    {
        get;
        set;
    } = 0f;

    private float OffPosition
    {
        get;
        set;
    } = 0f;

    public bool IsOn {
        get;
        private set;
    } = true;

    public BoolUnityEvent OnStateChanged { 
        get => onStateChanged;
    }

    public RectTransform Handle { 
        get => handle; 
    }

    public float AnimationTimeS { 
        get => animationTimeS; 
    }

    public RectTransform Indicator { 
        get => indicator; 
    }

    public TextMeshProUGUI OnText { 
        get => onText; 
    }

    public TextMeshProUGUI OffText {
        get => offText;
    }

    #endregion

    #region Methods

    protected void Start()
    {
        base.OnEnable();
        StartManagedCoroutine(WaitFrameAndUpdate());
    }

    private IEnumerator WaitFrameAndUpdate()
    {
        yield return new WaitForEndOfFrame();
        OffPosition = Indicator.rect.width - Handle.rect.width;
        OnPosition = 0f;

        SetIsOnWithoutNotify(IsOn);
    }

    protected override void OnActionExecute(PointerEventData eventData)
    {
        base.OnActionExecute(eventData);

        SetIsOn(!IsOn);
    }

    public void SetIsOn(bool isActive)
    {
        SetIsOnWithoutNotify(isActive);
        OnStateChanged?.Invoke(IsOn);
    }

    public void SetIsOnWithoutNotify(bool isActive)
    {
        if (isActive == true)
        {
            OnText.DOFade(1f, AnimationTimeS);
            OffText.DOFade(0f, AnimationTimeS);
            Handle.DOAnchorPosX(OnPosition, AnimationTimeS);
        }
        else
        {
            OnText.DOFade(0f, AnimationTimeS);
            OffText.DOFade(1f, AnimationTimeS);
            Handle.DOAnchorPosX(OffPosition, AnimationTimeS);
        }

        IsOn = isActive;
    }

    #endregion

    #region Enums

    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool>
    {
    }

    #endregion
}
