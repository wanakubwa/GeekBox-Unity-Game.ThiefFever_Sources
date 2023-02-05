using UnityEngine;

namespace GeekBox.Utils.Vibrator
{
    class AndroidVibrator : IVibratable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void Vibrate(long amplitude)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer?.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibratorService = unityActivity?.Call<AndroidJavaObject>("getSystemService", "vibrator");

            vibratorService?.Call("vibrate", amplitude);
        }

        #endregion

        #region Enums



        #endregion
    }
}
