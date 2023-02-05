using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GeekBox.Utils.ShareIntent
{
    class DummyShare : ISharable
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public void Share(string header, string subject, string body)
        {
            Debug.LogFormat("{0}\n header: [{1}], subject: [{2}], body: [{3}]", "Cant share at current device!", header, subject, body);
        }

        #endregion

        #region Enums



        #endregion
    }
}
