using UnityEngine;

namespace GeekBox.Utils.Vibrator
{
    class DummyVibrator : IVibratable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void Vibrate(long amplitude)
        {
            Debug.LogFormat("{0}\n amplitude: [{1}]", "Cant vibrate at current device!", amplitude);
        }

        #endregion

        #region Enums



        #endregion
    }
}
