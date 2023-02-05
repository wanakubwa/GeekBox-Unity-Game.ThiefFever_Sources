using UnityEngine;

public class URLOpener : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private string urlToOpen;

    #endregion

    #region Propeties

    public string UrlToOpen {
        get => urlToOpen;
    }

    #endregion

    #region Methods

    public void OpenURL()
    {
        Application.OpenURL(UrlToOpen);
    }

    #endregion

    #region Enums



    #endregion
}
