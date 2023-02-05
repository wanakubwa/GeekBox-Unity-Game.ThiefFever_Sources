using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GeekBox.Utils.ShareIntent
{
    class AndroidShare : ISharable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void Share(string header, string subject, string body)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass intentTemplate = new AndroidJavaClass("android.content.Intent");
                AndroidJavaObject intentShare = new AndroidJavaObject("android.content.Intent");

                //set action to that intent object   
                intentShare.Call<AndroidJavaObject>("setAction", intentTemplate.GetStatic<string>("ACTION_SEND"));
                intentShare.Call<AndroidJavaObject>("putExtra", intentTemplate.GetStatic<string>("EXTRA_SUBJECT"), subject);
                intentShare.Call<AndroidJavaObject>("putExtra", intentTemplate.GetStatic<string>("EXTRA_TEXT"), body);
                intentShare.Call<AndroidJavaObject>("setType", "text/plain");

                AndroidJavaObject chooser = intentTemplate.CallStatic<AndroidJavaObject>("createChooser", intentShare, header);
                unityActivity.Call("startActivity", chooser);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
