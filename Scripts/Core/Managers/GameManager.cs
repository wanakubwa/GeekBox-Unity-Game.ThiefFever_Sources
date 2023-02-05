using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameManager : ManagerSingletonBase<GameManager>
{
    #region Fields

    [Header("To spawn on Awake")]
    [SerializeField]
    List<GameObject> managersCollection = new List<GameObject>();

    [Space]
    [SerializeField]
    List<GameObject> eventsCollection = new List<GameObject>();

    [Space]
    [SerializeField]
    List<GameObject> debugOnlyCollection = new List<GameObject>();

    [Space]
    [SerializeField]
    private SceneLabel actualScene;

    #endregion

    #region Propeties

    public event Action<SceneLabel> OnSceneLabelChanged = delegate { };

    public List<GameObject> ManagersCollection {
        get => managersCollection; 
        private set => managersCollection = value; 
    }

    public List<GameObject> EventsCollection { 
        get => eventsCollection; 
        private set => eventsCollection = value; 
    }

    public List<GameObject> DebugOnlyCollection { 
        get => debugOnlyCollection; 
        private set => debugOnlyCollection = value; 
    }

    public List<IManager> Managers
    {
        get;
        private set;
    } = new List<IManager>();

    public SceneLabel ActualSetScene { 
        get => actualScene; 
        private set => actualScene = value; 
    }

    #endregion

    #region Methods

    public void SaveGame()
    {
        SaveLoadManager.Instance.SaveGame(Managers, ActualSetScene);
    }

    public void SaveGameAtScenarioEnd()
    {
        //SaveLoadManager.Instance.SaveGameAtEndScenario(Managers, ActualSetScene);
    }

    public void LoadGame()
    {
       // ScenariosManager.Instance.LoadSavedOfficialScenario(Managers);
    }

    public void ResetGame()
    {
        SaveLoadManager.Instance?.ResetGame(Managers);
    }

    public void LoadMenuScene()
    {
        //ScenariosManager scenariosManager = ScenariosManager.Instance;
        //if (scenariosManager != null)
        //{
        //    scenariosManager.LoadMenuScene(Managers);
        //}
    }

    public void LoadTargetScene(SceneLabel scene)
    {
        SaveLoadManager.Instance.LoadTargetScene(Managers, scene);
    }

    protected override void Awake()
    {
        GameManager[] objs = FindObjectsOfType<GameManager>();

        if (objs.Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        // Spawning managers at start game.
        List<GameObject> objectsToSpawn = GetAllObjectsToSpawn();
        SpawnAllObjectsOnAwake(objectsToSpawn);
    }

    private List<GameObject> GetAllObjectsToSpawn()
    {
        List<GameObject> objectsToSpawn = new List<GameObject>();
        objectsToSpawn.AddRange(EventsCollection);
        objectsToSpawn.AddRange(ManagersCollection);

        if(BuildVersionSettings.Instance.IsDebugBuild == true)
        {
            objectsToSpawn.AddRange(DebugOnlyCollection);
        }

        return objectsToSpawn;
    }

    private void SpawnAllObjectsOnAwake(List<GameObject> managersCollection)
    {
        if(managersCollection == null)
        {
            return;
        }

        foreach (GameObject manager in managersCollection)
        {
            GameObject newManager = Instantiate(manager);
            newManager.transform.ResetParent(gameObject.transform);

            IManager iManager = newManager.GetComponent<IManager>();
            if(iManager != null)
            {
                Managers.Add(iManager);
            }
        }

        foreach (IManager manager in Managers)
        {
            manager.Initialize();
        }
    }

    protected override void Start()
    {
        base.Start();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

       LoadSceneByActualLabel();
    }

    protected override void OnSceneSwitched(Scene scene)
    {
        base.OnSceneSwitched(scene);

        ActualSetScene = ManagersContentSetup.Instance.GetSceneLabelBySceneIndex(scene.buildIndex);
        OnSceneLabelChanged(ActualSetScene);
    }

    private void LoadSceneByActualLabel()
    {
        switch (ActualSetScene)
        {
            case SceneLabel.GAME:
                LoadTargetScene(ActualSetScene);
                break;
            case SceneLabel.PRELOADER_SCENE:
                //nothing.
                break;
            default:
                SaveLoadManager.Instance.LoadTargetScene(Managers, SceneLabel.GAME, SceneManager.GetActiveScene().buildIndex);

                break;
        }
    }

    #endregion

    #region Enums

    #endregion
}
