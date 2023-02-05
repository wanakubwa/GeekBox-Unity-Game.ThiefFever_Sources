using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameHudView : UIView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI leftTimeS;

    [Space]
    [SerializeField]
    private ItemListElement listElementPrefab;
    [SerializeField]
    private RectTransform listParent;


    #endregion

    #region Propeties

    public TextMeshProUGUI LeftTimeS { get => leftTimeS; }
    public ItemListElement ListElementPrefab { get => listElementPrefab; }
    public RectTransform ListParent { get => listParent; }

    private OfficeLvlMap CurrentMap { get; set; } = null;
    private List<ItemListElement> SpawnedItems { get; set; } = new List<ItemListElement>();

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentMap = MapsManager.Instance.CurrentMap;

        if(CurrentMap != null) 
        {
            OnMapSpawnedHandler(CurrentMap);
        }
    }

    public void GameStart()
    {
        CurrentMap = MapsManager.Instance.CurrentMap;

        if (CurrentMap != null)
        {
            OnMapSpawnedHandler(CurrentMap);
        }
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        if (GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnMapTimeSUpdate += OnMapTimeSUpdateHandler;
        }

        if(MapsManager.Instance != null)
        {
            MapsManager.Instance.OnMapSpawned += OnMapSpawnedHandler;
        }
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnMapTimeSUpdate -= OnMapTimeSUpdateHandler;
        }

        if (MapsManager.Instance != null)
        {
            MapsManager.Instance.OnMapSpawned -= OnMapSpawnedHandler;
        }
    }

    private void RefreshMapItems()
    {
        SpawnedItems.ClearDestroy();

        foreach (var mapGarbage in CurrentMap.Garbages)
        {
            ItemListElement newElement = Instantiate(ListElementPrefab);
            newElement.transform.ResetParent(ListParent);

            newElement.SetInfo(mapGarbage.Key, mapGarbage.Value);

            SpawnedItems.Add(newElement);
        }
    }

    private void AttachMapEvents()
    {
        CurrentMap.OnUpdateLvlGarbages += OnUpdateLvlGarbagesMapHandle;
    }

    // Handlers.

    private void OnMapTimeSUpdateHandler(float timeS)
    {
        float timeMs = timeS * Constants.SECONDS_TO_MILI_FACTOR;
        timeMs = Mathf.Clamp(timeMs, 0f, float.MaxValue);
        LeftTimeS.SetText(timeMs.ToTimeFormatt("mm:ss:ff"));
    }

    private void OnUpdateLvlGarbagesMapHandle(GarbageType garbageType)
    {
        ItemListElement garbageElement = SpawnedItems.Find(x => x.TypeOfGarbage == garbageType);
        if(garbageElement != null)
        {
            garbageElement.RefreshInfo(CurrentMap.GetGarbageAmmount(garbageType));
        }
    }

    private void OnMapSpawnedHandler(OfficeLvlMap map)
    {
        CurrentMap = map;

        if(CurrentMap != null)
        {
            RefreshMapItems();
            AttachMapEvents();
        }
    }

    #endregion

    #region Enums



    #endregion
}
