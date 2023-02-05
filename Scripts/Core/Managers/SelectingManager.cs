using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class SelectingManager : ManagerSingletonBase<SelectingManager>, IGameEvents
{
    #region Fields

    private Ray _reusableRaycast = new Ray();
    private RaycastHit[] _reusableRaycastHitArray = new RaycastHit[30];

    #endregion

    #region Propeties

    /// <summary>
    /// Touch - Info o aktualnym dotyku, vector3 - zetkniecie z plaszczyzna mapy swiata.
    /// </summary>
    public event Action<Touch, Vector3> OnSelectionUpdate = delegate { };

    public bool IsSelectionAvailable { get; set; } = false;
    private Camera WorldCamera { get => CameraController.Instance.CurrentCamera; }
    private List<ISelectable> ReusableSelectablesList { get; set; } = new List<ISelectable>();
    private ISelectable LastSelectedObject { get; set; } = null;
    private bool WasEmptyTouchBegan { get; set; } = false;
    public TouchInfo LastBeganTouchInfo { get; private set; }
    private bool IsSelectionLocked { get; set; } = true;

    #endregion

    #region Methods

    public void SelectObject(ISelectable selectable)
    {
        // Jakis obiekt znalazl sie pod myszka.
        if (LastSelectedObject != null && LastSelectedObject.IDEqual(selectable.ID) == true)
        {
            LastSelectedObject.OnDeselected();
            LastSelectedObject = null;
        }
        else if (LastSelectedObject != null)
        {
            LastSelectedObject.OnDeselected();
            selectable.OnSelected();
            LastSelectedObject = selectable;
        }
        else
        {
            selectable.OnSelected();
            LastSelectedObject = selectable;
        }
    }

    private void Update()
    {
        IsSelectionAvailable = CheckSelectionAvailable();

        Touch touch = TouchUtility.GetTouch(0);
        CreateTouchSnapshot(touch);

        if (IsSelectionAvailable && TouchUtility.TouchCount > 0)
        {
            CastRayAllForMousePosition(touch.position, out int hits);
            CheckSelection(touch, hits);
        }
    }

    private void CheckSelection(Touch touch, int hits)
    {
        Vector2 touchPosition = touch.position;
        Vector3 touchWorldPosition = Vector3.zero;

        List<ISelectable> selectedObjects = GetAllSelectableObjects(hits);
        if(touch.phase == TouchPhase.Began && selectedObjects.Count > Constants.DEFAULT_VALUE)
        {
            // toto; mozna dodac priorytetyzacje selekcji, ale w tym projekcie mamy tylko barykade.
            ISelectable selectedObject = selectedObjects[0];

            SelectObject(selectedObject);

            WasEmptyTouchBegan = false;
            CreateTouchSnapshot(touch);
        }
        else
        {
            WasEmptyTouchBegan = WasEmptyTouchBegan == false ? touch.phase == TouchPhase.Began : WasEmptyTouchBegan;
            CreateTouchSnapshot(touch);
            OnSelectionUpdate(touch, touchWorldPosition);
        }
    }

    private List<ISelectable> GetAllSelectableObjects(int hits)
    {
        ReusableSelectablesList.Clear();

        ISelectable selectable = null;
        for (int i = 0; i < hits; i++)
        {
            if(_reusableRaycastHitArray[i].collider != null)
            {
                selectable = _reusableRaycastHitArray[i].collider.gameObject.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    ReusableSelectablesList.Add(selectable);
                }
            }
        }

        return ReusableSelectablesList;
    }

    private RaycastHit[] CastRayAllForMousePosition(Vector3 touchPosition, out int hitCount)
    {
        if (WorldCamera == null)
        {
            hitCount = Constants.DEFAULT_VALUE;
            return _reusableRaycastHitArray;
        }

        _reusableRaycast = WorldCamera.ViewportPointToRay(WorldCamera.ScreenToViewportPoint(touchPosition));
        hitCount = Physics.RaycastNonAlloc(_reusableRaycast, _reusableRaycastHitArray, Mathf.Infinity);

        return _reusableRaycastHitArray;
    }

    private bool CheckSelectionAvailable()
    {
        return (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject() == false) && !IsSelectionLocked;
    }

    private void CreateTouchSnapshot(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)
        {
            LastBeganTouchInfo = new TouchInfo(IsSelectionAvailable, WasEmptyTouchBegan);
        }
    }

    public void LoadNextLvl()
    {

    }

    public void RestartLvl()
    {

    }

    public void StopLvlGame()
    {
        
    }

    public void StartLvlGame()
    {
        LastSelectedObject = null;
        IsSelectionLocked = false;
    }

    #endregion

    #region Enums

    public class TouchInfo
    {
        public bool WasSelectingAvailable { get; set; }
        public bool WasBeganEmpty { get; set; }

        public TouchInfo(bool wasSelectingAvailable, bool wasBeganEmpty)
        {
            WasSelectingAvailable = wasSelectingAvailable;
            WasBeganEmpty = wasBeganEmpty;
        }
    }

    #endregion
}
