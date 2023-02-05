using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class FadeOutAnimation : OutAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private CanvasGroup rootCanvas;

    #endregion

    #region Propeties

    public CanvasGroup RootCanvas
    {
        get => rootCanvas;
        private set => rootCanvas = value;
    }

    #endregion

    #region Methods

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        base.PlayAnimation(onCopleteHandler);

        RootCanvas.DOFade(0f, AnimationDurationS).OnComplete(onCopleteHandler.Invoke);
    }

    #endregion

    #region Handlers



    #endregion
}
