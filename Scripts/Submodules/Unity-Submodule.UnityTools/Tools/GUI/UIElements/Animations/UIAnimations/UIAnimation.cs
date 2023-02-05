using UnityEngine;
using System.Collections;
using System;

public class UIAnimation : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private float animationDurationS;

    #endregion

    #region Propeties

    public float AnimationDurationS { 
        get => animationDurationS; 
    }

    #endregion

    #region Methods

    public virtual void PlayAnimation(Action onCopleteHandler = null)
    {

    }

    #endregion

    #region Handlers



    #endregion
}
