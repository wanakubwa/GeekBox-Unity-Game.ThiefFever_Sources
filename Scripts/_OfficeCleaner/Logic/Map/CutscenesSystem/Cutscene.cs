using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnCutsceneStart()
    {
        CutscenesEvents.Instance.NotifyOnCutsceneStarted();
    }

    public void OnCutSceneEnd()
    {
        CutscenesEvents.Instance.NotifyOnCutsceneEnded();
    }

    #endregion

    #region Enums



    #endregion
}