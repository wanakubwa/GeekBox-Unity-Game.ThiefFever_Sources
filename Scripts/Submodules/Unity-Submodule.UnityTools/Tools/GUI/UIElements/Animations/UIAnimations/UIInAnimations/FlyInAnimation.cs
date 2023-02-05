using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class FlyInAnimation : InAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private RectTransform rootRect;
    [SerializeField]
    private CanvasGroup rootCanvas;
    [SerializeField]
    private float opacityStart;
    [SerializeField]
    private Vector3 startAnimationScale;

    #endregion

    #region Propeties

    public RectTransform RootRect
    {
        get => rootRect;
    }

    public Vector3 StartAnimationScale
    {
        get => startAnimationScale;
    }

    public float OpacityStart
    {
        get => opacityStart;
    }

    public CanvasGroup RootCanvas
    {
        get => rootCanvas;
    }

    #endregion

    #region Methods

    public override void Awake()
    {
        base.Awake();

        RootRect.ForceUpdateRectTransforms();

        RootCanvas.alpha = OpacityStart;
        RootRect.localScale = StartAnimationScale;
    }

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        base.PlayAnimation(onCopleteHandler);

        if (onCopleteHandler == null)
        {
            onCopleteHandler = delegate { };
        }

        RootCanvas.DOFade(1, AnimationDurationS);
        RootRect.DOScale(Vector3.one, AnimationDurationS).OnComplete(onCopleteHandler.Invoke);
    }

    #endregion

    #region Handlers



    #endregion
}
