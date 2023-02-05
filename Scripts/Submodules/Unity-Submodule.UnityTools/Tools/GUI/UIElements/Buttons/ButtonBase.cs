using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBase : UIMonoBehavior, IPointerClickHandler, IMoveHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    [Space]
    [SerializeField]
    private bool isInteractable = true;
    [SerializeField]
    private CanvasGroup rootCanvasGroup;

    [Space]
    [SerializeField]
    private float inDurationS;
    [SerializeField]
    private float outDurationS;

    [Space]
    [SerializeField]
    private UnityEvent onClick;

    #endregion

    #region Propeties

    public UnityEvent OnClick { 
        get => onClick; 
        private set => onClick = value;
    }

    public bool IsAnimated
    {
        get;
        protected set;
    } = false;

    public bool IsInteractable { 
        get => isInteractable; 
        private set => isInteractable = value; 
    }

    public CanvasGroup RootCanvasGroup { 
        get => rootCanvasGroup; 
    }

    public float InDurationS { 
        get => inDurationS; 
        private set => inDurationS = value; 
    }

    public float OutDurationS { 
        get => outDurationS; 
        private set => outDurationS = value; 
    }

    private bool IsClicked {
        get;
        set;
    } = false;

    private bool IsSelected {
        get;
        set;
    } = false;

    #endregion

    #region Methods

    public void SetInteractable(bool isInteractable)
    {
        IsInteractable = isInteractable;
        RefreshInteractable();
    }

    public virtual void OnMove(AxisEventData eventData) { }

    /// <summary>
    /// Call when user ,,clicked'' on button in this method can be declared what this button should do.
    /// </summary>
    public virtual void OnClickHandler() { }

    public virtual void RefreshInteractable()
    {
        if (RootCanvasGroup == null)
        {
            return;
        }

        if (IsInteractable == true)
        {
            RootCanvasGroup.alpha = 1f;
        }
        else
        {
            RootCanvasGroup.alpha = 0.65f;
        }
    }

    /// <summary>
    /// Klasa zatrzymuje wszystkie animacje, ktore znajduja sie na obiekcie.
    /// </summary>
    public virtual void KillAllAnimations(bool complete = false)
    {
        this.DOKill(complete);

        if(RootCanvasGroup != null)
        {
            RootCanvasGroup.DOKill();
        }  
    }

    /// <summary>
    /// Akcje, ktore maja sie rozpoczac po wcisnieciu i przytrzymaniu danego przycisku.
    /// Automatycznie zatrzymuje wszystkie obecne animacje.
    /// </summary>
    public virtual void OnSelected()
    {
        IsSelected = true;
        KillAllAnimations();
    }

    /// <summary>
    /// Strata fokusu na przycisku, odpuszczenie przytrzymania 
    /// np. podniesiony palec, zwolnienie przycisku myszy lub wyjscie poza obszar obiektu.
    /// </summary>
    public virtual void OnDeselected() 
    {
        IsSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || IsInteractable == false)
        {
            ResetButton();
            return;
        }

        IsClicked = true;
        OnDeselected();
        OnActionExecute(eventData);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || IsInteractable == false)
        {
            ResetButton();
            return;
        }

        IsAnimated = true;
        OnSelected();
    }

    public virtual void OnPointerUp(PointerEventData eventData) 
    {
        //OnDeselected();
    }

    public virtual void OnPointerEnter(PointerEventData eventData) { }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!IsClicked || IsSelected)
        {
            OnDeselected();
            ResetButton();
        }
    }

    /// <summary>
    /// Reset button state to return to default position and look. 
    /// Reverting current running animations.
    /// </summary>
    public virtual void ResetButton()
    {
        KillAllAnimations(true);
        IsAnimated = false;
        IsClicked = false;
    }

    /// <summary>
    /// Funkcja wywolywana gdy funkcja przycisku zostanie wywolana po jego wcisnieciu.
    /// </summary>
    protected virtual void OnActionExecute(PointerEventData eventData)
    {
        OnClick.Invoke();
        OnClickHandler();
        ResetButton();
    }

    #endregion

    #region Handlers



    #endregion
}
