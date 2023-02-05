using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler
{
    #region Fields

    [SerializeField]
    private UnityEvent onTouch;

    #endregion

    #region Propeties

    public UnityEvent OnTouch { get => onTouch; }

    #endregion

    #region Methods

    public void OnPointerDown(PointerEventData eventData)
    {
        if(OnTouch != null)
        {
            OnTouch.Invoke();
        }
    }

    #endregion

    #region Enums



    #endregion
}
