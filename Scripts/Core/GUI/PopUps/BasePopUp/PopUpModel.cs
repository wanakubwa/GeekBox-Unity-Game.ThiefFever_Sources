using System;
using System.Collections;
using UnityEngine;


public class PopUpModel : UIMonoBehavior
{

    #region Fields

    [Space]
    [SerializeField]
    private bool isPauseOnEnable = false;
    [SerializeField]
    private float autoCloseTime = 0f;

    #endregion

    #region Propeties

    public bool IsPauseOnEnable
    {
        get => isPauseOnEnable;
        private set => isPauseOnEnable = value;
    }

    public float AutoCloseTime { 
        get => autoCloseTime; 
        private set => autoCloseTime = value; 
    }

    #endregion

    #region Methods

    public void SetClosingTime(float closeTimeMs)
    {
        AutoCloseTime = closeTimeMs;

        RefreshAutoClose(AutoCloseTime);
    }

    public virtual void ClosePopUp()
    {
        if (IsPauseOnEnable == true)
        {
            //InGameEvents.Instance.NotifyPauseTime(false);
        }
    }

    public virtual void Initialize()
    {

    }

    public override void OnEnable()
    {
        base.OnEnable();

        if (IsPauseOnEnable == true)
        {
            //InGameEvents.Instance.NotifyPauseTime(true);
        }

        if (AutoCloseTime > 0f)
        {
            RefreshAutoClose(AutoCloseTime * 0.001f);
        }
    }

    public virtual void AttachEvents()
    {
        PopUpManager.Instance.OnOverlapPopUpOpen += SuspendManagedCorutines;
        PopUpManager.Instance.OnOverlapPopUpClose += RefreshAllManagedCoroutines;
    }

    public virtual void DettachEvents()
    {
        if(PopUpManager.Instance != null)
        {
            PopUpManager.Instance.OnOverlapPopUpOpen -= SuspendManagedCorutines;
            PopUpManager.Instance.OnOverlapPopUpClose -= RefreshAllManagedCoroutines;
        }
    }

    public virtual void HidePopUp()
    {
        if (IsPauseOnEnable == true)
        {
            //InGameEvents.Instance.NotifyPauseTime(false);
        }

        gameObject.SetActive(false);
    }

    public virtual void ShowPopUp()
    {
        if (IsPauseOnEnable == true)
        {
            //InGameEvents.Instance.NotifyPauseTime(true);
        }

        gameObject.SetActive(true);
    }

    public T GetView<T>() where T : PopUpView
    {
        T view = GetComponent<PopUpView>() as T;
        return view;
    }

    public T GetController<T>() where T : PopUpController
    {
        T controller = GetComponent<PopUpController>() as T;
        return controller;
    }

    public IEnumerator WaitAndClose(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        PopUpController controller = GetController<PopUpController>();
        controller.ClosePopUp();

        yield return null;
    }

    private void RefreshAutoClose(float timeMs)
    {
        StartManagedCoroutine(WaitAndClose(timeMs));
    }

    #endregion

    #region Handlers



    #endregion
}
