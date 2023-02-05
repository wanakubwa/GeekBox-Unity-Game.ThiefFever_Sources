using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class SizeButton : ButtonBase
{
    #region Fields

    [SerializeField]
    private RectTransform rootRect;

    [Space, Title("Animations settings")]
    [SerializeField]
    private Vector3 targetScale;

    #endregion

    #region Propeties

    public RectTransform RootRect { get => rootRect; }
    public Vector3 TargetScale { get => targetScale; }

    #endregion

    #region Methods

    public override void OnSelected()
    {
        base.OnSelected();

        RootRect.DOScale(TargetScale, InDurationS);
    }

    public override void ResetButton()
    {
        base.ResetButton();

        RootRect.DOScale(Vector3.one, OutDurationS);
    }

    public override void OnDisable()
    {
        RootRect.DOKill();
    }

    public override void KillAllAnimations(bool complete = false)
    {
        base.KillAllAnimations(complete);

        RootRect.DOKill();
    }

    #endregion

    #region Handlers



    #endregion
}
