using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTriggerHandler : MonoBehaviour
{
    #region MEMEBRS

    [SerializeField]
    private PlayerUnityEvent onPlayerEnter = new PlayerUnityEvent();
    [SerializeField]
    private PlayerUnityEvent onPlayerExit = new PlayerUnityEvent();

    #endregion

    #region PROPERTIES

    #endregion

    #region FUNCTIONS

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacterController playerCharacter = other.gameObject.GetComponent<PlayerCharacterController>();
        if(playerCharacter != null)
        {
            onPlayerEnter.Invoke(playerCharacter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerCharacterController playerCharacter = other.gameObject.GetComponent<PlayerCharacterController>();
        if (playerCharacter != null)
        {
            onPlayerExit.Invoke(playerCharacter);
        }
    }

    #endregion

    #region CLASS_ENUMS

    [Serializable]
    public class PlayerUnityEvent : UnityEngine.Events.UnityEvent<PlayerCharacterController> { }

    #endregion
}