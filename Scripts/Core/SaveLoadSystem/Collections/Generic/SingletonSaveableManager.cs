using UnityEngine;
using SaveLoadSystem;
using System;
using System.Runtime.CompilerServices;

public abstract class SingletonSaveableManager<T, U> : ManagerSingletonBase<T>, ISaveable where T : MonoBehaviour where U : MementoBase, new()
{
    #region Fields

    [SerializeField]
    private string saveFileName = string.Empty;

    #endregion

    #region Propeties

    public string SaveFileName { 
        get => saveFileName; 
    }

    public bool IsLoaded {
        get;
        private set;
    } = false;

    #endregion

    #region Methods

    public virtual void Load(string directoryPath)
    {
        U memento = SaveLoadManager.Instance.GetSavedManagerMemento<U>(SaveFileName, directoryPath);
        if(memento != null)
        {
            LoadManager(memento);
        }
        else
        {
            Debug.LogFormat("[LOAD] Manager {0} has no saved memento and contains default data".SetColor(Color.yellow), this.GetType());
        }

        IsLoaded = true;
    }

    public virtual void Save(string directoryPath)
    {
        U memento = new U();
        memento.CreateMemento(this);
        SaveLoadManager.Instance.SaveManager(memento, SaveFileName, PathFacade.CurrentPath.ProgressDataSavePath);
    }

    public virtual void ResetGameData()
    {

    }

    public abstract void LoadManager(U memento);

    protected virtual void Reset()
    {
        saveFileName = GetType().Name;
    }

    #endregion

    #region Enums



    #endregion
}
