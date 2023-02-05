using UnityEngine;

public class KeyInteractable : Interactable
{
    #region Fields

    [SerializeField]
    private string keyLabel;

    #endregion

    #region Propeties

    public string KeyLabel { get => keyLabel; }

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        CollectKey();
        DestroyObject();
    }

    private void CollectKey()
    {
        GameplayEvents.Instance.NotifyOnKeyCollected(KeyLabel);
    }

    #endregion

    #region Enums



    #endregion
}
