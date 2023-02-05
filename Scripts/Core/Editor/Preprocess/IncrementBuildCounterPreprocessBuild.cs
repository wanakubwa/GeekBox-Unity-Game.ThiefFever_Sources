using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class IncrementBuildCounterPreprocessBuild : IPreprocessBuildWithReport
{
    #region Fields



    #endregion

    #region Propeties

    public int callbackOrder { get { return 1; } }

    #endregion

    #region Methods

    public void OnPreprocessBuild(BuildReport report)
    {
        BuildVersionSettings.Instance.DoBuild();
        Debug.LogFormat("IncrementBuildCounterPreprocessBuild... {0}".SetColor(Color.white), "[DONE]".SetColor(Color.green));
    }

    #endregion

    #region Enums



    #endregion
}
