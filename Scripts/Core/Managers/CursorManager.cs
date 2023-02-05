using UnityEngine;
using CursorSystem.Interfaces;

public class CursorManager : ManagerSingletonBase<CursorManager>
{
    #region Fields



    #endregion

    #region Propeties

    public bool IsActiveFollowerObject { get => CurrentFollowerObject != null; }

    private ICursorFollowerObject CurrentFollowerObject { get; set; } = null;

    #endregion

    #region Methods

    public void SetFollowerObject(ICursorFollowerObject target)
    {
        TryDestroyFollowerObject();

        target.Init(transform);
        CurrentFollowerObject = target;
    }

    private void Update()
    {
        if(CurrentFollowerObject != null)
        {
            CurrentFollowerObject.CustomUpdate(Time.deltaTime);
        }
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SelectingManager.Instance.OnSelectionUpdate += OnSelectionUpdateHandler;
        UserInputManager.Instance.OnMouseRelease += OnMouseReleaseHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        SelectingManager.Instance.OnSelectionUpdate -= OnSelectionUpdateHandler;
        UserInputManager.Instance.OnMouseRelease -= OnMouseReleaseHandler;
    }

    private void TryDestroyFollowerObject()
    {
        if (CurrentFollowerObject != null)
        {
            CurrentFollowerObject.CleanData();
            CurrentFollowerObject = null;
        }
    }

    // Handlers.
    private void OnSelectionUpdateHandler(Touch touch, Vector3 worldPosition)
    {
        if (CurrentFollowerObject != null)
        {
            CurrentFollowerObject.Refresh(worldPosition);
        }
    }

    private void OnMouseReleaseHandler()
    {
        if (CurrentFollowerObject != null)
        {
            CurrentFollowerObject.OnMouseRelease();
            TryDestroyFollowerObject();
        }
    }

    #endregion

    #region Enums



    #endregion
}
