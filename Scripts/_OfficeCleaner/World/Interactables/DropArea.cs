using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DropArea : Interactable
{
    #region Fields

    [SerializeField]
    private float removeDelay = 0.15f;

    [SerializeField]
    private Transform destinationPosition;

    #endregion

    #region Propeties

    public Transform DestinationPosition { get => destinationPosition; }
    private Coroutine DestackCoroutine { get; set; } = null;

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        DestackCoroutine = StartCoroutine(_WaitEndRemoveFromStack());
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        if(DestackCoroutine != null)
        {
            StopCoroutine(DestackCoroutine);
        }
    }

    private IEnumerator _WaitEndRemoveFromStack()
    {
        while(Player.Stacker.StackedItems.Count > 0)
        {
            Player.RemoveFromStack(OnRemovedElementsFromStack);
            yield return new WaitForSeconds(removeDelay);
        }
    }

    private void OnRemovedElementsFromStack(StackSystem.StackableObject element)
    {
        element.transform.DOMove(DestinationPosition.position, Player.Stacker.PickUpTimeS)
            .OnComplete(() => 
            {
                AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.DESTACK);
                element.DestroyObject();
            });
    }

    #endregion

    #region Enums



    #endregion
}
