using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OfficeLvlMap : LvlMap
{
    #region Fields

    [SerializeField]
    private int targetMoney = 1000;
    [SerializeField]
    private float lvlTimeLimitS = 60f;
    [SerializeField]
    private Transform roomsParent;
    [SerializeField]
    private GameObject exitArea;

    #endregion

    #region Propeties

    public event Action<GarbageType> OnUpdateLvlGarbages = delegate { };
    public event Action<float> OnTimeAdded = delegate { };
    public event Action<int> OnMoneyAdded = delegate { };

    public float LvlTimeLimitS { get => lvlTimeLimitS; }
    public float LvlLeftTimeS { get; private set; }
    public int CurrentMoney { get; set; } = Constants.DEFAULT_VALUE;

    // Variables.
    public StorageRoom Storage { get; set; }
    public Dictionary<GarbageType, int> Garbages { get; private set; } = new Dictionary<GarbageType, int>();
    public List<RoomBase> Rooms { get; set; } = new List<RoomBase>();

    #endregion

    #region Methods

    public void AddMoney(int ammount)
    {
        CurrentMoney += ammount;
        OnMoneyAdded(ammount);

        if (CurrentMoney >= targetMoney)
        {
            exitArea.SetActive(true);
        }
    }

    public int GetGarbageAmmount(GarbageType garbage)
    {
        Garbages.TryGetValue(garbage, out int count);
        return count;
    }

    public override void Init(MapsManager mapsManager)
    {
        base.Init(mapsManager);

        // Inicjalizacja pomieszczen.
        Rooms = roomsParent.GetComponentsInChildren<RoomBase>().ToList();
        Storage = GetComponentInChildren<StorageRoom>();
        LvlLeftTimeS = LvlTimeLimitS;

        exitArea.SetActive(false);
    }

    public void AddTime(float timeToAddS)
    {
        LvlLeftTimeS += timeToAddS;
        OnTimeAdded(timeToAddS);
    }

    public void OnGameStop()
    {
        StopAllCoroutines();
    }

    public void OnGameStart()
    {
        //StartCoroutine(_TimeWatchdog());
    }

    public void OnRemoveGarbage(GarbageType garbage)
    {
        if(Garbages.TryGetValue(garbage, out int count) == true)
        {
            Garbages[garbage] = --count;
            OnUpdateLvlGarbages(garbage);
        }
        else
        {
            Debug.LogError("Proba usuniecia elementu, ktorego nie ma w kolekcji smieci poziomu! " + garbage);
        }

        CheckWinConditions();
    }


    private void CheckWinConditions()
    {
        int mapGarbageCount = Constants.DEFAULT_VALUE;
        foreach (var garbage in Garbages)
        {
            mapGarbageCount += garbage.Value;
        }

        if(mapGarbageCount == Constants.DEFAULT_VALUE)
        {
            GamePlayManager.Instance.LvlSuccess();
        }
    }

    private void OnLvlFailed()
    {
        GamePlayManager.Instance.LvlFailed();
    }

    private IEnumerator _TimeWatchdog()
    {
        while (true)
        {
            LvlLeftTimeS -= Time.deltaTime;
            if (LvlLeftTimeS <= Constants.DEFAULT_VALUE)
            {
                OnLvlFailed();
                yield break;
            }
            else
            {
                GameplayEvents.Instance.NotifiMapTimeUpdate(LvlLeftTimeS);
            }

            yield return null;
        }
    }

    #endregion

    #region Enums



    #endregion
}
