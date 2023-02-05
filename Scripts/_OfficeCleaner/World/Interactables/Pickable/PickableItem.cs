using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : Interactable
{
    #region Fields

    [SerializeField]
    private GameObject visualizationTreasure;

    [SerializeField]
    private Treasure targetTreasure;

    [Space]
    [SerializeField]
    private UnityEvent onPickup = new UnityEvent();

    #endregion

    #region Propeties

    public Treasure TargetTreasure { 
        get => targetTreasure;
    }
    public GameObject VisualizationTreasure { 
        get => visualizationTreasure; 
    }

    #endregion

    #region Methods

    public void PickUpItem()
    {
        if(IsInteractable == true)
        {
            VisualizationTreasure.gameObject.SetActive(false);
            Player.AddToStack(TargetTreasure, VisualizationTreasure.transform.position);

            IsInteractable = false;

            onPickup.Invoke();
        }
    }

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        PickUpItem();
    }

    #endregion

    #region Enums



    #endregion
}