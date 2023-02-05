using StackSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Transform stackRoot;
    [SerializeField]
    private float pickUpTimeS = 0.25f;

    #endregion

    #region Propeties

    public float PickUpTimeS { get => pickUpTimeS; }

    public Stack<StackableObject> StackedItems { get; private set; } = new Stack<StackableObject>();
    private float CurrentStackHeight { get; set; } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public void RemoveFromStack(Action<StackableObject> onRemoveCallback = null)
    {
        if (StackedItems.TryPop(out StackableObject lastItem) == true)
        {
            CurrentStackHeight -= lastItem.GetStackVisualizationHeight();
            
            if(onRemoveCallback != null)
            {
                onRemoveCallback(lastItem);
            }
            else
            {
                Destroy(lastItem.gameObject);
            }
        }
    }

    public void AddToStack(StackableObject prefab, Vector3 startPosition)
    {
        StackableObject newStackItem = SpawnNewItem(prefab, startPosition);

        float currentObjectHeight = newStackItem.GetStackVisualizationHeight();
        Vector3 targetLocalPosition = new Vector3(0f, CurrentStackHeight, 0f);

        // Animacja lotu na stack.
        newStackItem.transform.DOLocalMove(targetLocalPosition, pickUpTimeS);
        newStackItem.transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, newStackItem.OnStackRotationYDegree, 0f), pickUpTimeS);

        CurrentStackHeight += newStackItem.GetStackVisualizationHeight();
        StackedItems.Push(newStackItem);
    }

    private StackableObject SpawnNewItem(StackableObject prefab, Vector3 startPosition)
    {
        StackableObject newStackItem = Instantiate(prefab);
        newStackItem.transform.ResetParent(stackRoot);
        newStackItem.transform.localScale = prefab.OnStackScale;
        newStackItem.transform.position = startPosition;

        return newStackItem;
    }

    #endregion

    #region Enums



    #endregion
}
