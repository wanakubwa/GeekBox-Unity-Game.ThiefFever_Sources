using PlayerData;
using System;
using UnityEngine;

public class PlayerManager : SingletonSaveableManager<PlayerManager, PlayerManagerMemento>, IGameEvents
{
    #region Fields

    [SerializeField]
    private PlayerCharacterController playerPrefab;

    private PlayerWallet wallet = new PlayerWallet();

    #endregion

    #region Propeties

    public event Action<float> OnEnergyChanged = delegate { };
    public event Action<PlayerCharacterController> OnCharacterSpawned = delegate { };

    public PlayerWallet Wallet { 
        get => wallet; 
        private set => wallet = value;
    }
    private PlayerCharacterController PlayerPrefab { 
        get => playerPrefab;
    }

    private float DestroyedSurface { get; set; } = Constants.DEFAULT_VALUE;
    public float CurrentEnergyNormalized { get; private set; } = Constants.DEFAULT_VALUE;
    public PlayerCharacterController CurrentPlayer { get; set; }

    #endregion

    #region Methods

    public void LoadNextLvl()
    {
        Wallet.SetCurrentLvlNo(Wallet.CurrentLvlNo + 1);
    }

    public void RestartLvl()
    {

    }

    public void StartLvlGame()
    {
        CurrentPlayer.IsInputEnabled = true;
    }

    public void StopLvlGame()
    {
        CurrentPlayer.IsInputEnabled = false;
    }

    public void HidePlayer()
    {
        StopLvlGame();
        CurrentPlayer.SetVisible(false);
    }

    public void ShowPlayer()
    {
        StartLvlGame();
        CurrentPlayer.SetVisible(true);
    }

    public override void LoadManager(PlayerManagerMemento memento)
    {
        Wallet.Load(memento.SavedWallet);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        MapsManager.Instance.OnMapSpawned += OnMapSpawnedHandler;
        GamePlayManager.Instance.OnLvlSuccess += OnLvlSuccessHandler;
    }

    public override void ResetGameData()
    {
        base.ResetGameData();
        Wallet.SetDefaultData();
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GamePlayManager.Instance.OnLvlSuccess -= OnLvlSuccessHandler;
    }

    private void SpawnPlayer(Vector3 spawnPosition)
    {
        if(CurrentPlayer != null)
        {
            Destroy(CurrentPlayer.gameObject);
        }

        // Spawn na scenie.
        CurrentPlayer = Instantiate(PlayerPrefab);
        CurrentPlayer.transform.position = spawnPosition;
        OnCharacterSpawned(CurrentPlayer);
    }

    // HANDLERS.
    private void OnLvlSuccessHandler(int score)
    {
        LvlData data = new LvlData(Constants.DEFAULT_ID, score, Constants.DEFAULT_VALUE, Constants.DEFAULT_VALUE);
        Wallet.AddLvlData(data);
    }

    private void OnMapSpawnedHandler(OfficeLvlMap obj)
    {
        SpawnPlayer(obj.Storage.SpawnPosition.transform.position);
    }

    #endregion

    #region Enums



    #endregion
}
