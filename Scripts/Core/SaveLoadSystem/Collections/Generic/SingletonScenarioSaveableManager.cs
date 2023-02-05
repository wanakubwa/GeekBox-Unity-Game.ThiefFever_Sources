using SaveLoadSystem;
using UnityEngine;

public abstract class SingletonScenarioSaveableManager<T, U> : SingletonSaveableManager<T, U>, IScenarioSaveable where T : MonoBehaviour where U : MementoBase, new()
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public abstract void CreateNewScenario();

    #endregion

    #region Enums



    #endregion
}
