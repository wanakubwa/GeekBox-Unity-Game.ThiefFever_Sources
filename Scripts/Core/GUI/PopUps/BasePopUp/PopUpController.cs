using System;
using System.Collections;
using UnityEngine;

public class PopUpController : UIMonoBehavior
{
    #region Fields

    [Space]
    [SerializeField]
    PopUpManager.PopUpPrority prority;

    [Space]
    [SerializeField]
    private PopUpModel model;
    [SerializeField]
    private PopUpView view;

    [Space(5)]
    [SerializeField]
    private Canvas popUpCanvas;
    [SerializeField]
    private CanvasGroup popUpCanvasGroup;

    [SerializeField]
    private float showDelayS = Constants.DEFAULT_VALUE;

    #endregion

    #region Propeties

    public event Action OnPopUpClose = delegate { };

    internal PopUpModel Model
    {
        get => model;
    }

    internal PopUpView View
    {
        get => view;
    }

    public Canvas PopUpCanvas
    {
        get => popUpCanvas;
        private set => popUpCanvas = value;
    }

    public CanvasGroup PopUpCanvasGroup { get => popUpCanvasGroup; }

    public PopUpManager.PopUpPrority Prority
    {
        get => prority;
    }

    public float ShowDelayS { get => showDelayS; }

    #endregion

    #region Methods

    public override void OnDisable()
    {
        base.OnDisable();

        DettachEvents();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if (ShowDelayS > Constants.DEFAULT_VALUE)
        {
            PopUpCanvas.enabled = false;
            StartManagedCoroutine(_WaitAndShowPopup());
        }
        else
        {
            TryPlayEnterAnimation();
        }

        AttachEvents();
    }

    public void Start()
    {
        View.CustomStart();
    }

    public virtual void AttachEvents()
    {
        Model.AttachEvents();
        View.AttachEvents();
    }

    public virtual void DettachEvents()
    {
        Model.DettachEvents();
        View.DettachEvents();
    }

    public virtual void Initialize()
    {
        gameObject.SetActive(true);

        Model.Initialize();
        View.Initialize();
        InitializeCanvasCamera(Camera.main);
    }

    public void ClosePopUp()
    {
        PopUpManager.Instance.RequestClosePopUp(this);

        OnPopUpClose();

        SetPopUpInteractable(false);

        Model.ClosePopUp();
        View.ClosePopUp();

        OutAnimation inAnimation = GetComponent<OutAnimation>();
        if (inAnimation != null)
        {
            inAnimation.PlayAnimation(() => Destroy(gameObject));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TogglePopUp()
    {
        if (gameObject.activeInHierarchy == true)
        {
            Model.HidePopUp();
        }
        else
        {
            Model.ShowPopUp();
        }
    }

    public void SetPopUpInteractable(bool isInteractable)
    {
        if(PopUpCanvasGroup != null)
        {
            PopUpCanvasGroup.interactable = isInteractable;
        }
    }

    public T GetModel<T>() where T : PopUpModel
    {
        T model = GetComponent<PopUpModel>() as T;
        return model;
    }

    public T GetView<T>() where T : PopUpView
    {
        T model = GetComponent<PopUpView>() as T;
        return model;
    }

    private void InitializeCanvasCamera(Camera mainCamera)
    {
        if (PopUpCanvas != null)
        {
            PopUpCanvas.worldCamera = mainCamera;
        }
    }

    private void TryPlayEnterAnimation()
    {
        InAnimation inAnimation = GetComponent<InAnimation>();
        if (inAnimation != null)
        {
            inAnimation.PlayAnimation();
        }
    }

    // Coroutines.
    private IEnumerator _WaitAndShowPopup()
    {
        yield return new WaitForSeconds(ShowDelayS);
        PopUpCanvas.enabled = true;
        TryPlayEnterAnimation();
    }

    #endregion

    #region Handlers



    #endregion
}
