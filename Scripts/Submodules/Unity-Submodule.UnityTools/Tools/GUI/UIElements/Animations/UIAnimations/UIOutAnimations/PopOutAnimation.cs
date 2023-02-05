using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class PopOutAnimation : OutAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private RectTransform rootRect;
    [SerializeField]
    private CanvasGroup rootCanvas;
    [SerializeField]
    private Vector3 endAnimationScale;
    [SerializeField]
    private float alphaEnd;
    #endregion

    #region Propeties

    public RectTransform RootRect { get => rootRect; }

    public CanvasGroup RootCanvas { get => rootCanvas; }

    public Vector3 EndAnimationScale { get => endAnimationScale; }

    public float AlphaEnd { get => alphaEnd; }

    #endregion

    #region Methods

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        if (onCopleteHandler == null)
        {
            onCopleteHandler = delegate { };
        }

        base.PlayAnimation(onCopleteHandler);

        RootCanvas.DOFade(AlphaEnd, AnimationDurationS);
        RootRect.DOScale(EndAnimationScale, AnimationDurationS).OnComplete(onCopleteHandler.Invoke);
    }

    #endregion

    #region Handlers



    #endregion
}
