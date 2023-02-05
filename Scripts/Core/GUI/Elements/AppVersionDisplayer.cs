using TMPro;
using UnityEngine;

public class AppVersionDisplayer : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI versionText;

    #endregion

    #region Propeties

    public TextMeshProUGUI VersionText { 
        get => versionText;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        VersionText.SetText(GetCurrentAppVersionInfo());
    }

    private string GetCurrentAppVersionInfo()
    {
        return string.Format("v. {0}", BuildVersionSettings.Instance.BuildVersion);
    }

    #endregion

    #region Enums



    #endregion
}
