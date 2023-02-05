using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class IDScriptableObject : ScriptableObject, IIDEquatable
{
    #region Fields

    [SerializeField, ReadOnly]
    private int id = Guid.NewGuid().GetHashCode();

    #endregion

    #region Propeties

    public int ID => id;

    #endregion

    #region Methods

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

#if UNITY_EDITOR

    [Button]
    private void RefreshID()
    {
        id = Guid.NewGuid().GetHashCode();
    }

#endif

#endregion

    #region Enums



    #endregion
}
