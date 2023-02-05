namespace GeekBox.Utils.Vibrator
{
    class VibratorStrategy
    {
        #region Fields



        #endregion

        #region Propeties

        private IVibratable CurrentVibrator { get; set; }

        #endregion

        #region Methods

        public VibratorStrategy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            CurrentVibrator = new AndroidVibrator();
#else
            CurrentVibrator = new DummyVibrator();
#endif
        }

        public void Vibrate(long amplitude)
        {
            if(CurrentVibrator != null)
            {
                CurrentVibrator.Vibrate(amplitude);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
