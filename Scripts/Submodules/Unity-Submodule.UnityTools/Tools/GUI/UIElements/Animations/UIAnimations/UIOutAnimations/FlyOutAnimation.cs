using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlyOutAnimation : OutAnimation
{
    #region Fields

    [Space]
    [SerializeField]
    private RectTransform rootRect;
    [SerializeField]
    private CanvasGroup rootCanvas;
    [SerializeField]
    private float opacityEnd;
    [SerializeField]
    private Vector3 endAnimationScale;

    #endregion

    #region Propeties

    public RectTransform RootRect
    {
        get => rootRect;
    }

    public Vector3 EndAnimationScale
    {
        get => endAnimationScale;
    }

    public float OpacityEnd
    {
        get => opacityEnd;
    }

    public CanvasGroup RootCanvas
    {
        get => rootCanvas;
    }

    #endregion

    #region Methods

    public override void PlayAnimation(Action onCopleteHandler = null)
    {
        base.PlayAnimation(onCopleteHandler);

        if (onCopleteHandler == null)
        {
            onCopleteHandler = delegate { };
        }

        RootCanvas.DOFade(OpacityEnd, AnimationDurationS);
        RootRect.DOScale(EndAnimationScale, AnimationDurationS).OnComplete(onCopleteHandler.Invoke);
    }

    #endregion

    #region Handlers



    #endregion
}
