namespace GeekBox.Utils.Toast
{
    class ToastStrategy
    {
        #region Fields



        #endregion

        #region Propeties

        private IToastable CurrentToast { get; set; }

        #endregion

        #region Methods

        public ToastStrategy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            CurrentToast = new AndroidToast();
#else
            CurrentToast = new DummyToast();
#endif
        }

        public void ShowToast(string message)
        {
            if(CurrentToast != null)
            {
                CurrentToast.ShowToast(message);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
