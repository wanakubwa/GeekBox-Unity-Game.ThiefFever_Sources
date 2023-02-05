using UnityEngine;
using System.Collections;

public abstract class LvlMap : MonoBehaviour
{
    #region Fields



    #endregion

    #region Propeties

    protected MapsManager Context { get; private set; }
    protected bool IsInteractable { get; set; } = true;

    #endregion

    #region Methods

    /// <summary>
    /// Wywolane w momencie startu poziomu.
    /// Od tego czasu gracz moze dokonywac interakcji z poziomem.
    /// </summary>
    public virtual void Init(MapsManager mapsManager)
    {
        Context = mapsManager;
        AttachEvents();
    }

    protected virtual void OnDestroy()
    {
        
    }

    protected virtual void AttachEvents()
    {

    }

    protected virtual void DetachEvents()
    {

    }

    #endregion

    #region Enums



    #endregion
}
