using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SizeColorButton : ButtonBase
{
    #region Fields

    [SerializeField]
    private RectTransform rootRect;
    [SerializeField]
    private Image rootImage;

    [Space, Title("Animations settings")]
    [SerializeField]
    private float animationDurationS;
    [SerializeField]
    private float resetDurationS;
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Vector3 targetScale;

    #endregion

    #region Propeties

    public RectTransform RootRect { get => rootRect; }
    public Image RootImage { get => rootImage; }
    public float AnimationDurationS { get => animationDurationS; }
    public float ResetDurationS { get => resetDurationS; }
    public Color SelectedColor { get => selectedColor; }
    public Vector3 TargetScale { get => targetScale; }

    private Vector3 DefaultScale
    {
        get;
        set;
    }

    private Color DefaultColor
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void OnSelected()
    {
        base.OnSelected();

        RootImage.DOColor(SelectedColor, AnimationDurationS);
        RootRect.DOScale(TargetScale, AnimationDurationS);
    }

    public override void ResetButton()
    {
        base.ResetButton();

        RootImage.DOColor(DefaultColor, ResetDurationS);
        RootRect.DOScale(DefaultScale, ResetDurationS);
    }

    public override void OnDisable()
    {
        KillAllAnimations();
    }

    public override void KillAllAnimations(bool complete = false)
    {
        base.KillAllAnimations(complete);

        if(RootImage != null)
        {
            RootImage.DOKill();
        }

        if(RootRect != null)
        {
            RootRect.DOKill();
        }
    }

    private void Start()
    {
        DefaultColor = RootImage.color;
        DefaultScale = RootRect.localScale;
    }

    #endregion

    #region Handlers



    #endregion
}
