
using System;

public interface IInitializable
{

    #region Fields



    #endregion

    #region Propeties

    event Action OnInitialized;

    #endregion

    #region Methods

    void Initialize();

    #endregion

    #region Enums



    #endregion
}
