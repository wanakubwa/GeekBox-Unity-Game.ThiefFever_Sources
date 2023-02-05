using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "BuildVersionSettings.asset", menuName = "Settings/BuildVersionSettings")]
public class BuildVersionSettings : ScriptableObject
{
    #region Fields

    private static BuildVersionSettings instance;

    [Space]
    [SerializeField]
    private bool isDebugBuild;

    [Space]
    [SerializeField, ReadOnly]
    private string buildVersion;
    [Space]
    [SerializeField, ReadOnly]
    private string lastBuildDate;
    [SerializeField, ReadOnly]
    private int todayBuildsCounter;

    #endregion

    #region Propeties

    public static BuildVersionSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<BuildVersionSettings>("Settings/BuildVersionSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public string BuildVersion { 
        get => buildVersion; 
        private set => buildVersion = value; 
    }

    public int TodayBuildsCounter { 
        get => todayBuildsCounter; 
        private set => todayBuildsCounter = value; 
    }

    public string LastBuildDate { 
        get => lastBuildDate; 
        private set => lastBuildDate = value; 
    }

    public bool IsDebugBuild { 
        get => isDebugBuild; 
        private set => isDebugBuild = value; 
    }

    #endregion

    #region Methods

    private void IncreaseBuildVersion()
    {
        string dateString = DateTime.Now.Date.ToShortDateString();
        dateString = dateString.Replace(".", "");

        if (LastBuildDate.Equals(dateString) == true)
        {
            TodayBuildsCounter++;
        }
        else
        {
            TodayBuildsCounter = 1;
        }

        string dateVersionString = string.Format("{0}.{1}", dateString, TodayBuildsCounter);
        BuildVersion = dateVersionString;
        lastBuildDate = dateString;
    }

#if UNITY_EDITOR

    public void DoBuild()
    {
        IncreaseBuildVersion();
        SaveThisAsset();
    }

    [Button(ButtonSizes.Large)]
    private void Build()
    {
        IncreaseBuildVersion();
    }

    [Button]
    private void ResetBuildVersion()
    {
        string dateString = DateTime.Now.Date.ToShortDateString();
        dateString = dateString.Replace(".", "");

        lastBuildDate = dateString;
        TodayBuildsCounter = 0;

        Build();
    }

    public void SaveThisAsset()
    {
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }

#endif

    #endregion

    #region Handlers


    #endregion
}
