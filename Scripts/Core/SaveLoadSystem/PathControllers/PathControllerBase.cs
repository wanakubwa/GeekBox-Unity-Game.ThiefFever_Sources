using System.IO;
using UnityEngine;

public class PathControllerBase
{
    #region Fields

    public const string SAVE_FOLDER = "GameSave";
    public const string PROGRESS_SAVE_FOLDER = "Progress";

    #endregion

    #region Propeties

    public string DataSavePath { get; protected set; }
    public string ProgressDataSavePath{ get; protected set; }

    #endregion

    #region Methods

    protected virtual string GetDeviceRootPath()
    {
        return Directory.GetParent(Application.dataPath).FullName;
    }

    public void Init()
    {
        DataSavePath = GetDataPath();
        ProgressDataSavePath = GetProgressDataPath();
        TryCreatePath(DataSavePath);
        TryCreatePath(ProgressDataSavePath);
    }

    public void ResetProgressDataPathContent()
    {
        if (Directory.Exists(ProgressDataSavePath) == false)
        {
            return;
        }

        Directory.Delete(ProgressDataSavePath, true);
        TryCreatePath(ProgressDataSavePath);
    }

    private void TryCreatePath(string directoryPath)
    {
        if (Directory.Exists(directoryPath) == false)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private string GetDataPath()
    {
        return Path.Combine(GetDeviceRootPath(), SAVE_FOLDER);
    }

    private string GetProgressDataPath()
    {
        // .../GameSave/Progress/
        return Path.Combine(GetDeviceRootPath(), SAVE_FOLDER, PROGRESS_SAVE_FOLDER);
    }

    #endregion

    #region Enums



    #endregion
}
