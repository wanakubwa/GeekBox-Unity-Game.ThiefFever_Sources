using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour, IIDEquatable
{
    #region Fields

    #endregion

    #region Propeties

    protected PlayerCharacterController Player { get; set; } = null;
    protected bool IsInteractable { get; set; } = true;

    public int ID { get; private set; } = Guid.NewGuid().GetHashCode();

    #endregion

    #region Methods

    protected virtual void Awake()
    {

    }

    protected virtual void OnPlayerEnter(PlayerCharacterController player)
    {
        Player = player;
    }

    protected virtual void OnPlayerExit(PlayerCharacterController player)
    {
        
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null && IsInteractable == true)
        {
            OnPlayerEnter(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            OnPlayerExit(player);
            Player = null;
        }
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    #endregion

    #region Enums



    #endregion
}
