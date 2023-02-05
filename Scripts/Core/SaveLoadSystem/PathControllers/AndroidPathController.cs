using System.IO;
using UnityEngine;

public class AndroidPathController : PathControllerBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected override string GetDeviceRootPath()
    {
        return Application.persistentDataPath;
    }

    #endregion

    #region Enums



    #endregion
}
