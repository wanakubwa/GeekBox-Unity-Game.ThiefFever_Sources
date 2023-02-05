

namespace GeekBox.Utils.ShareIntent
{
    class ShareStrategy
    {
        #region Fields



        #endregion

        #region Propeties

        private ISharable CurrentSharable
        {
            get; set;
        }

        #endregion

        #region Methods

        public ShareStrategy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            CurrentSharable = new AndroidShare();
#else
            CurrentSharable = new DummyShare();
#endif
        }

        public void Share(string header, string subject, string body)
        {
            if (CurrentSharable != null)
            {
                CurrentSharable.Share(header, subject, body);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
