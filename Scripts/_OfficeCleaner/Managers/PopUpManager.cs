using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUpManager : ManagerSingletonBase<PopUpManager>
{
    #region Fields

    [Space, Title("Game Popups")]
    [SerializeField]
    private GameObject startUIPopUp;
    [SerializeField]
    private GameObject winUIPopUp;
    [SerializeField]
    private GameObject settingsPopUp;
    [SerializeField]
    private GameObject losePopUp;

    [Title("Garbage Popups")]
    [SerializeField]
    private GameObject windowGarbagePopUp;

    #endregion

    #region Propeties

    public event Action<PopUpController> OnPopUpClosed = delegate { };
    public event Action OnOverlapPopUpOpen = delegate { };
    public event Action OnOverlapPopUpClose = delegate { };

    public PopUpController CurrentPopUp {
        get;
        private set;
    }

    public PopUpController OverlapPopUp {
        get;
        private set;
    }

    public List<PopUpController> PopUpsQueue {
        get;
        private set;
    } = new List<PopUpController>();

    // PopUps.
    public GameObject StartUIPopUp { get => startUIPopUp; }
    public GameObject WinUIPopUp { get => winUIPopUp; }
    public GameObject SettingsPopUp { get => settingsPopUp; }
    public GameObject LosePopUp { get => losePopUp; }
    public GameObject WindowGarbagePopUp { get => windowGarbagePopUp; }

    #endregion

    #region Methods

    public T ShowPopUp<T>(GameObject popUpPrefab, bool isQueued = true) where T : PopUpController
    {
        ClearReferences();

        bool isDisplayed = false;

        if(popUpPrefab == null)
        {
            return null;
        }

        T popUpController = RequestPopUp<T>(popUpPrefab);

        // TODO: Zrobic to normalnie.
        if(popUpController.Prority == PopUpPrority.OVERLAP)
        {
            popUpController.TogglePopUp();
            if(OverlapPopUp != null)
            {
                OverlapPopUp.ClosePopUp();
            }

            OverlapPopUp = popUpController;

            OnOverlapPopUpOpen();
            return popUpController;
        }

        if(CurrentPopUp == null)
        {
            CurrentPopUp = popUpController;
            popUpController.TogglePopUp();

            isDisplayed = true;
        }
        else
        {
            if(HasHigherOrEqualPriority(popUpController) == true)
            {
                SwapCurrentPopUp(popUpController);
                isDisplayed = true;
            }
        }

        if (isQueued == true)
        {
            AddPopUpToQueue(popUpController);
        }

        // Jezeli nie zostal wyswietlony ani nie jest w queue to mozna zniszczyc.
        if (isDisplayed == false && isQueued == false)
        {
            popUpController.ClosePopUp();
            popUpController = null;
        }

        return popUpController;
    }

    private void ClearReferences()
    {
        if(PopUpsQueue.IsNullOrEmpty() == true)
        {
            return;
        }

        for(int i=0; i < PopUpsQueue.Count; i++)
        {
            if (PopUpsQueue[i] == null)
            {
                PopUpsQueue.RemoveAt(i);
            }
        }
    }

    public T RequestPopUp<T>(GameObject popUpPrefab) where T : PopUpController
    {
        T requestedPopUp;

        requestedPopUp = Instantiate(popUpPrefab).GetComponent<T>();
        requestedPopUp.Initialize();
        requestedPopUp.TogglePopUp();

        return requestedPopUp;
    }

    public T RequestShowPopUpForced<T>(GameObject popUpPrefab) where T : PopUpController
    {
        T requestedPopUp = null;

        if(CurrentPopUp != null)
        {
            CurrentPopUp.TogglePopUp();
        }

        requestedPopUp = Instantiate(popUpPrefab).GetComponent<T>();
        requestedPopUp.Initialize();

        CurrentPopUp = requestedPopUp;

        return requestedPopUp;
    }

    public void RequestClosePopUp(PopUpController popUp)
    {
        TryRemovePopUpFromQueue(popUp);

        if (popUp.Prority == PopUpPrority.OVERLAP)
        {
            OverlapPopUp = null;
            OnOverlapPopUpClose();
        }
        else
        {
            if (CurrentPopUp == popUp)
            {
                CurrentPopUp = null;
                CheckQueue();
            }
        }

        OnPopUpClosed(popUp);
    }

    public bool IsPopUpDisplayed<T>() where T : PopUpController
    {
        if (CurrentPopUp is T)
        {
            return true;
        }

        return false;
    }

    public T GetPopUpController<T>() where T : PopUpController
    {
        if(CurrentPopUp is T)
        {
            return (T)CurrentPopUp;
        }

        T queuePopup = TryGetPopUpFromQueue<T>();
        if(queuePopup != null)
        {
            return queuePopup;
        }

        return null;
    }

    public bool IsPopUpInQueue(PopUpController popUpController)
    {
        if(PopUpsQueue == null)
        {
            return false;
        }

        for(int i =0; i< PopUpsQueue.Count; i++)
        {
            if (ReferenceEquals(PopUpsQueue[i], popUpController) == true)
            {
                return true;
            }
        }

        return false;
    }

    public T TryGetPopUpFromQueue<T>() where T : PopUpController
    {
        if(PopUpsQueue != null)
        {
            for(int i =0; i < PopUpsQueue.Count; i++)
            {
                if(PopUpsQueue[i] is T)
                {
                    return (T)PopUpsQueue[i];
                }
            }
        }

        return null;
    }

    #region PopUps

    public WindowGarbagePopUpController ShowWindowGarbagePopUp(int stainsAmmount, Action onCompleted)
    {
        WindowGarbagePopUpController controller = null;

        if (CheckDoublePopUp<WindowGarbagePopUpController>() == true)
        {
            controller = GetPopUpController<WindowGarbagePopUpController>();
        }

        controller = ShowPopUp<WindowGarbagePopUpController>(WindowGarbagePopUp, true);
        controller.SetData(stainsAmmount, onCompleted);

        return controller;
    }

    public LosePopUpController ShowLosePopUp()
    {
        if (CheckDoublePopUp<LosePopUpController>() == true)
        {
            return GetPopUpController<LosePopUpController>();
        }

        LosePopUpController popUpController = ShowPopUp<LosePopUpController>(LosePopUp, true);
        return popUpController;
    }

    public SettingsPopUpController ShowSettingsPopUp()
    {
        if (CheckDoublePopUp<SettingsPopUpController>() == true)
        {
            return GetPopUpController<SettingsPopUpController>();
        }

        SettingsPopUpController popUpController = ShowPopUp<SettingsPopUpController>(SettingsPopUp, true);
        return popUpController;
    }

    public StartUIPopUpController ShowStartUIPopUp()
    {
        if (CheckDoublePopUp<StartUIPopUpController>() == true)
        {
            return GetPopUpController<StartUIPopUpController>();
        }

        StartUIPopUpController popUpController = ShowPopUp<StartUIPopUpController>(StartUIPopUp, true);
        return popUpController;
    }

    public WinBoostPopUpController ShowWinUIPopUp(int wallsCounter)
    {
        if(CheckDoublePopUp<WinBoostPopUpController>() == true)
        {
            return GetPopUpController<WinBoostPopUpController>();
        }

        WinBoostPopUpController popUpController = ShowPopUp<WinBoostPopUpController>(WinUIPopUp, false);
        return popUpController;
    }

    #endregion

    private void AddPopUpToQueue(PopUpController popUpController)
    {
        PopUpsQueue.Add(popUpController);
    }

    private bool HasHigherOrEqualPriority(PopUpController popUpController)
    {
        if (popUpController.Prority >= CurrentPopUp.Prority)
        {
            return true;
        }

        return false;
    }

    private void SwapCurrentPopUp(PopUpController popUpController)
    {
        PopUpController cachedPopUp = CurrentPopUp;

        if (cachedPopUp != null)
        {
            if (IsPopUpInQueue(cachedPopUp) == true)
            {
                cachedPopUp.TogglePopUp();
            }
            else
            {
                cachedPopUp.ClosePopUp();
            }
        }

        CurrentPopUp = popUpController;
        CurrentPopUp.TogglePopUp();
    }

    private void TryRemovePopUpFromQueue(PopUpController popUpController)
    {
        if (PopUpsQueue == null)
        {
            return;
        }

        PopUpsQueue.RemoveAll(x => x == popUpController);
    }

    private PopUpController TryGetHighestProrityPopUpFromQueue()
    {
        PopUpController hiPriority = null;
        if (PopUpsQueue != null && PopUpsQueue.Count > 0)
        {
            hiPriority = PopUpsQueue.First();

            for (int i = 0; i < PopUpsQueue.Count; i++)
            {
                if (PopUpsQueue[i].Prority > hiPriority.Prority)
                {
                    hiPriority = PopUpsQueue[i];
                }
            }
        }

        return hiPriority;
    }

    private void CheckQueue()
    {
        PopUpController nextPopUp = TryGetHighestProrityPopUpFromQueue();
        if(nextPopUp != null)
        {
            nextPopUp.TogglePopUp();
            CurrentPopUp = nextPopUp;
        }
    }

    private bool CheckDoublePopUp<T>() where T : PopUpController
    {
        if(OverlapPopUp is T)
        {
            return true;
        }

        if(CurrentPopUp is T)
        {
            return true;
        }

        for(int i = 0; i < PopUpsQueue.Count; i++)
        {
            if(PopUpsQueue[i] is T)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Handlers



    #endregion

    public enum PopUpPrority
    {
        MINIMUM = 0,
        NORMAL = 5,
        MEDIUM = 10,
        HIGH = 15,
        VERY_HIGH = 20,

        OVERLAP = -1
    }
}

