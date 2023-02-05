using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class PopInAnimation : InAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private RectTransform rootRect;
    [SerializeField]
    private CanvasGroup rootCanvas;
    [SerializeField]
    private Vector3 startAnimationScale;
    [SerializeField]
    private float alphaStart;
    #endregion

    #region Propeties

    public RectTransform RootRect { get => rootRect; }

    public CanvasGroup RootCanvas { get => rootCanvas; }

    public Vector3 StartAnimationScale { get => startAnimationScale; }

    public float AlphaStart { get => alphaStart; }

    #endregion

    #region Methods

    public override void Awake()
    {
        base.Awake();

        RootRect.localScale = StartAnimationScale;
        RootCanvas.alpha = AlphaStart;
    }

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        base.PlayAnimation(onCopleteHandler);

        RootCanvas.DOFade(1f, AnimationDurationS);
        RootRect.DOScale(Vector3.one, AnimationDurationS);
    }

    #endregion

    #region Handlers



    #endregion
}
