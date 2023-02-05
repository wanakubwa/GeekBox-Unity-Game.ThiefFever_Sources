using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class ScreenshotCapturer : MonoBehaviour
{
    #region Fields

    private const string FILE_EXTENSION = ".png";

    [SerializeField]
    private int screenshotScale = 1;
    [SerializeField]
    private string fileName = "screenshot";

    #endregion

    #region Propeties

    public int ScreenshotScale
    {
        get => screenshotScale;
        private set => screenshotScale = value;
    }

    public string FileName {
        get => fileName;
        private set => fileName = value;
    }

    #endregion

    #region Methods

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11) == true)
        {
            // Generate short only valid letters identifiers.
            var uid = Regex.Replace(System.Convert.ToBase64String(System.Guid.NewGuid().ToByteArray()), "[/+=]", "");
            ScreenCapture.CaptureScreenshot($"{FileName}_{uid}{FILE_EXTENSION}", ScreenshotScale);
        }
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        GameObject go = new GameObject("EDITOR_ScreenshotCapturer");
        ScreenshotCapturer ss = go.AddComponent<ScreenshotCapturer>();
        DontDestroyOnLoad(go);
        Debug.Log("ScreenshotCapturer - INITIALIZED IN EDITOR!");
    }
#endif

#endregion

    #region Enums



    #endregion
}
