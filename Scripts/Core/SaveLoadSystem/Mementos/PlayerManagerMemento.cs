using PlayerData;
using System;
using UnityEngine;

[Serializable]
public class PlayerManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private PlayerWallet savedWallet;

    #endregion

    #region Propeties

    public PlayerWallet SavedWallet { get => savedWallet; private set => savedWallet = value; }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        PlayerManager manager = sourceManager as PlayerManager;
        if(manager != null)
        {
            SavedWallet = manager.Wallet;
        }
    }

    #endregion

    #region Enums



    #endregion
}
