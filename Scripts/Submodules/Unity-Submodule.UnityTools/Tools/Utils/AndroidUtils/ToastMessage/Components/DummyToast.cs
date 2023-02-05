using UnityEngine;

namespace GeekBox.Utils.Toast
{
    class DummyToast : IToastable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void ShowToast(string message)
        {
            Debug.LogFormat("{0}\n msg: [{1}]", "Cant show toast at current device!", message);
        }

        #endregion

        #region Enums



        #endregion
    }
}
