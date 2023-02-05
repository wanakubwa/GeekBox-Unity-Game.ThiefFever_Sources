using GeekBox.Utils.Toast;
using GeekBox.Utils.ShareIntent;
using GeekBox.Utils.Vibrator;

namespace GeekBox.Utils
{
    class AndroidUtils
    {

        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public static void ShowToast(string message)
        {
            ToastStrategy toast = new ToastStrategy();
            toast.ShowToast(message);
        }

        public static void Share(string header, string subject, string body)
        {
            ShareStrategy share = new ShareStrategy();
            share.Share(header, subject, body);
        }

        public static void Vibrate(long amplitude)
        {
            VibratorStrategy vibrator = new VibratorStrategy();
            vibrator.Vibrate(amplitude);
        }

        #endregion

        #region Enums



        #endregion
    }
}
