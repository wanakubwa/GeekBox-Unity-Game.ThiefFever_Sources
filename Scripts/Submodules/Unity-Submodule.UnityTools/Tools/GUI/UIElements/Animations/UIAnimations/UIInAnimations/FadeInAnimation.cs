using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class FadeInAnimation : InAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private CanvasGroup rootCanvas;

    #endregion

    #region Propeties

    public CanvasGroup RootCanvas { 
        get => rootCanvas; 
        private set => rootCanvas = value; 
    }

    #endregion

    #region Methods

    public override void Awake()
    {
        base.Awake();

        RootCanvas.alpha = 0f;
    }

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        base.PlayAnimation(onCopleteHandler);

        RootCanvas.alpha = 0f;
        RootCanvas.DOFade(1f, AnimationDurationS);
    }

    #endregion

    #region Handlers



    #endregion
}
