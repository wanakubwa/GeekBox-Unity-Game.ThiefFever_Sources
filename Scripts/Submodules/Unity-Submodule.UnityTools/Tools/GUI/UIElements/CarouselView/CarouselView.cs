using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarouselView<T , U> : UIBehaviour, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler where T : CarouselViewElement<U>
{
    [SerializeField]
    private T carouselElementPrefab;
    [SerializeField]
    /// <summary>
    /// Space between images.
    /// </summary>
    private float image_gap = 30;
    [SerializeField]
    private int swipeThrustHold = 30;
    [SerializeField]
    private UnityEvent onSwiped;

    private bool canSwipe;
    private bool isSwiped = false;
    private float elementWidth;
    private float lerpTimer;
    private float lerpPosition;
    private float mousePositionStartX;
    private float mousePositionEndX;
    private float dragAmount;
    private float screenPosition;
    private float lastScreenPosition;

    [HideInInspector]
    /// <summary>
    /// The index of the current image on display.
    /// </summary>
    public int current_index;

    public List<U> CollectionElements
    {
        get;
        private set;
    } = new List<U>();

    public List<CarouselViewElement<U>> CarouselCollection
    {
        get;
        private set;
    } = new List<CarouselViewElement<U>>();

    public T CarouselElementPrefab { get => carouselElementPrefab; }
    public UnityEvent OnSwiped { get => onSwiped; }

    #region API

    public void SetCollection(List<U> collection)
    {
        CarouselCollection.ClearDestroy();

        elementWidth = CarouselElementPrefab.RootTransform.rect.width;
        for (int i =0; i < collection.Count; i++)
        {
            CarouselViewElement<U> viewElement = Instantiate(CarouselElementPrefab);
            viewElement.transform.ResetParent(transform);
            viewElement.DrawElement(collection[i]);
            CarouselCollection.Add(viewElement);
        }

        RefreshValues();
    }

    public void GoToIndex(int value)
    {
        current_index = value;
        lerpTimer = 0;
        lerpPosition = (elementWidth + image_gap) * current_index;
        screenPosition = lerpPosition * -1;
        lastScreenPosition = screenPosition;

        RefreshVisualizations();
    }

    public void GoToIndexSmooth(int value)
    {
        current_index = value;
        lerpTimer = 0;
        lerpPosition = (elementWidth + image_gap) * current_index;
    }

    #endregion

    #region mono

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSwiped == false)
            {
                screenPosition = lastScreenPosition;
            }
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canSwipe = true;
            isSwiped = false;
            mousePositionStartX = Input.mousePosition.x;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (canSwipe)
            {
                mousePositionEndX = Input.mousePosition.x;
                dragAmount = mousePositionEndX - mousePositionStartX;
                screenPosition = lastScreenPosition + dragAmount;
            }
        }
    }

    private void RefreshValues()
    {
        elementWidth = CarouselElementPrefab.RootTransform.rect.width;
        for (int i = 1; i < CarouselCollection.Count; i++)
        {
            CarouselCollection[i].RootTransform.anchoredPosition = new Vector2(((elementWidth + image_gap) * i), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If collection is not initialized or is empty return.
        if(CarouselCollection.Count < 1)
        {
            return;
        }

        lerpTimer = lerpTimer + Time.deltaTime;

        if (lerpTimer < 0.333f)
        {
            screenPosition = Mathf.Lerp(lastScreenPosition, lerpPosition * -1, lerpTimer * 3);
            lastScreenPosition = screenPosition;
        }

        if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe)
        {
            canSwipe = false;
            lastScreenPosition = screenPosition;
            if (current_index < CarouselCollection.Count)
                OnSwipeComplete();
            else if (current_index == CarouselCollection.Count && dragAmount < 0)
                lerpTimer = 0;
            else if (current_index == CarouselCollection.Count && dragAmount > 0)
                OnSwipeComplete();

            isSwiped = true;
        }

        RefreshVisualizations();
    }
    #endregion

    #region private methods
    void OnSwipeComplete()
    {
        lastScreenPosition = screenPosition;

        if (dragAmount > 0) // Left swipe.
        {
            if (dragAmount >= swipeThrustHold)
            {
                if (current_index == 0)
                {
                    lerpTimer = 0;
                    GoToIndexSmooth(CarouselCollection.Count - 1);
                }
                else
                {
                    current_index--;
                    lerpTimer = 0;
                    if (current_index < 0)
                    {
                        current_index = 0;
                    }
                    lerpPosition = (elementWidth + image_gap) * current_index;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }
        else if (dragAmount < 0) // Right swipe.
        {
            if (Mathf.Abs(dragAmount) >= swipeThrustHold)
            {
                if (current_index == CarouselCollection.Count - 1)
                {
                    lerpTimer = 0;
                    GoToIndexSmooth(0);
                }
                else
                {
                    lerpTimer = 0;
                    current_index++;
                    lerpPosition = (elementWidth + image_gap) * current_index;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }
        dragAmount = 0;

        RefreshVisualizations();
        OnSwiped.Invoke();
    }

    private void RefreshVisualizations()
    {
        for(int i =0; i < CarouselCollection.Count; i++)
        {
            CarouselCollection[i].RootTransform.anchoredPosition = new Vector2(screenPosition + ((elementWidth + image_gap) * i), 0);

            if (i == current_index)
            {
                CarouselCollection[i].SetFocused();
            }
            else
            {
                CarouselCollection[i].SetUnFocued();
            }
        }
    }

    #endregion

    #region public methods
    #endregion
}
