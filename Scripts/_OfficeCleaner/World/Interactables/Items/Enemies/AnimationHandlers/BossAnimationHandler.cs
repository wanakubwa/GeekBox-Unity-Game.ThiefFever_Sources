using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossAnimationHandler : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private UnityEvent onSuprieseEnded = new UnityEvent();

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnSupriesedAnimationEnded()
    {
        onSuprieseEnded.Invoke();
    }

    #endregion

    #region Enums



    #endregion
}