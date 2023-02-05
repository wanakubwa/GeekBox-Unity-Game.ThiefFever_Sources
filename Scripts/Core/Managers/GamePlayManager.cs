using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : ManagerSingletonBase<GamePlayManager>
{
    #region Fields



    #endregion

    #region Propeties

    public event Action OnLvlUpdate = delegate { };
    public event Action OnGameStart = delegate { };
    public event Action OnGameStop = delegate { };
    public event Action OnLvlReset = delegate { };

    public event Action<int> OnLvlSuccess = delegate { };
    public event Action OnLvlFailed = delegate { };

    public bool IsGameplayRun { get; private set; } = false;

    #endregion

    #region Methods

    public void ResetLvl()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if(manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.RestartLvl();
            }
        }

        OnLvlUpdate();
        OnLvlReset();

        StopLvlGame();
        PopUpManager.Instance.ShowStartUIPopUp();

        Debug.Log("ResetLvl");
    }

    public void LoadNextLvl()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.LoadNextLvl();
            }
        }

        OnLvlUpdate();
        Debug.Log("LoadNextLvl");

        StartLvlGame();
    }

    public void LvlSuccess(int wallsCounter = 1)
    {
        StopLvlGame();

        OnLvlSuccess(wallsCounter);
        PopUpManager.Instance.ShowWinUIPopUp(wallsCounter);
    }

    public void LvlFailed()
    {
        StopLvlGame();
        
        PopUpManager.Instance.ShowLosePopUp();
        OnLvlFailed();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        StopLvlGame();
        PopUpManager.Instance.ShowStartUIPopUp();
    }

    public void StopLvlGame()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.StopLvlGame();
            }
        }

        IsGameplayRun = false;
        OnGameStop();
        Debug.Log("StopLvlGame");
    }

    public void StartLvlGame()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.StartLvlGame();
            }
        }

        IsGameplayRun = true;
        OnGameStart();

        Debug.Log("StartLvlGame");
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SaveLoadManager.Instance.OnResetCompleted += ResetLvl;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= ResetLvl;
        }
    }

    #endregion

    #region Enums



    #endregion
}
