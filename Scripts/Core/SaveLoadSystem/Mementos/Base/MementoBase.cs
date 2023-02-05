using SaveLoadSystem;
using System;

[Serializable]
public abstract class MementoBase : IMemento
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public abstract void CreateMemento(IManager sourceManager);

    #endregion

    #region Enums



    #endregion
}
