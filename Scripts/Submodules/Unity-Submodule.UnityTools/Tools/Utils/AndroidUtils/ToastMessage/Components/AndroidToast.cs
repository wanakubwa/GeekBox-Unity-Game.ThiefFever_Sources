using UnityEngine;

namespace GeekBox.Utils.Toast
{
    class AndroidToast : IToastable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        // Source: https://stackoverflow.com/questions/52590525/how-to-show-a-toast-message-in-unity-similar-to-one-in-android
        public void ShowToast(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
