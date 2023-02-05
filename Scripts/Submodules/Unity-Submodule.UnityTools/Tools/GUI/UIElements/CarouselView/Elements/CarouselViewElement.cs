using DG.Tweening;
using UnityEngine;

public abstract class CarouselViewElement<T> : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private RectTransform rootTransform;

    #endregion

    #region Propeties

    public RectTransform RootTransform { get => rootTransform; }

    #endregion

    #region Methods

    public abstract void DrawElement(T source);

    public virtual void SetFocused()
    {
        RootTransform.DOScale(new Vector3(1f, 1f, 1f), 0.15f);
    }

    public virtual void SetUnFocued()
    {
        RootTransform.DOScale(new Vector3(0.85f, 0.85f, 1f), 0.1f);
    }

    public virtual void OnDisable()
    {
        RootTransform.DOKill();
    }

    public void RefreshFocus(bool isFocused)
    {
        if(isFocused == true)
        {
            SetFocused();
        }
        else
        {
            SetUnFocued();
        }
    }

    #endregion

    #region Enums



    #endregion
}
