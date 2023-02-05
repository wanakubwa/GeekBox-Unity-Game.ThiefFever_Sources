using System;

public class CutscenesEvents : ManagerSingletonBase<CutscenesEvents>
{
    #region Fields

    #endregion

    #region Propeties

    public event Action OnCutsceneStarted = delegate { };
    public event Action OnCutsceneEnded = delegate { };

    #endregion

    #region Methods

    public void NotifyOnCutsceneStarted()
    {
        OnCutsceneStarted();
    }

    public void NotifyOnCutsceneEnded()
    {
        OnCutsceneEnded();
    }

    #endregion

    #region Enums



    #endregion
}
