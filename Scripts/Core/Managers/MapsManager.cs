using System;
using System.Diagnostics;
using UnityEngine;

public class MapsManager: ManagerSingletonBase<MapsManager>, IGameEvents
{
    #region Fields

    private const string CONTAINER_TAG = "World_Container";

    #endregion

    #region Propeties

    public event Action<OfficeLvlMap> OnMapSpawned = delegate { };

    //Variables.
    public OfficeLvlMap CurrentMap { get; private set; }
    private Transform SceneWorldContainer { get; set; }

    private MapGeneratorSettings GeneratorSettings { get; set; }

    #endregion

    #region Methods

    public void LoadNextLvl()
    {
        LoadNextMap();
    }

    public void RestartLvl()
    {
        ResetCurrentRoad();
    }

    public override void Initialize()
    {
        base.Initialize();

        GeneratorSettings = MapGeneratorSettings.Instance;
    }

    public override void LoadContent()
    {
        base.LoadContent();

        SceneWorldContainer = GameObject.FindGameObjectWithTag(CONTAINER_TAG).transform;

        LoadNextMap();
    }

    public void StopLvlGame()
    {
        CurrentMap.OnGameStop();
    }

    public void StartLvlGame()
    {
        CurrentMap.OnGameStart();
    }

    private void ResetCurrentRoad()
    {
        SpawnMap();
    }

    private void LoadNextMap()
    {
        SpawnMap();
    }

    private void SpawnMap()
    {
        if(CurrentMap != null)
        {
            CurrentMap.gameObject.SetActive(false);
            Destroy(CurrentMap.gameObject);
        }

        SpawnConcreteLvlMap();
    }

    private void SpawnConcreteLvlMap()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        OfficeLvlMap concreteMapPrefab = GeneratorSettings.GetMapForLvl(PlayerManager.Instance.Wallet.NextLvlNo) as OfficeLvlMap;

        CurrentMap = Instantiate(concreteMapPrefab);
        CurrentMap.transform.ResetParent(SceneWorldContainer);

        CurrentMap.Init(Instance);
        UnityEngine.Debug.LogFormat("Mapa wygenerowana: {0}ms", watch.ElapsedMilliseconds);

        OnMapSpawned(CurrentMap);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SaveLoadManager.Instance.OnResetCompleted += ResetGame;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= ResetGame;
        }
    }

    private void ResetGame()
    {
        SpawnMap();
    }

    #endregion

    #region Enums



    #endregion
}
