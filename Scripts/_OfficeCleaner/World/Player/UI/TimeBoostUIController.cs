using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Player.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class TimeBoostUIController : MonoBehaviour
{
    #region Fields

    private const string TIME_DISPLAY_FORMAT = "+ {0}s";

    [SerializeField]
    private float displayTimeS = 1.5f;
    [SerializeField]
    private Vector2 spawnDeviation = Vector2.zero;
    [SerializeField]
    private TimeBoostElement timeInfoPrefab;

    #endregion

    #region Propeties

    private OfficeLvlMap CachedLvl { get; set; } = null;
    private Stack<TimeBoostElement> SpawnedTimeInfoPool { get; set; } = new Stack<TimeBoostElement> { };

    #endregion

    #region Methods

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            TimeBoostElement element = Instantiate(timeInfoPrefab, timeInfoPrefab.transform.parent);
            element.gameObject.SetActive(false);

            SpawnedTimeInfoPool.Push(element);
        }
    }

    [Button]
    public void ShowTextFloatingInfo(int value)
    {
        TimeBoostElement element = null;
        if (SpawnedTimeInfoPool.Count > Constants.DEFAULT_VALUE)
        {
            element = SpawnedTimeInfoPool.Pop();
        }
        else
        {
            element = Instantiate(timeInfoPrefab, timeInfoPrefab.transform.parent);
            element.gameObject.SetActive(false);
        }

        Vector2 anchoredPosition = GetRandomRectPosition();
        element.TimeRect.anchoredPosition = anchoredPosition;
        element.TimeText.color = new Color(element.TimeText.color.r, element.TimeText.color.g, element.TimeText.color.b, 0f);
        element.TimeText.SetText(string.Format(TIME_DISPLAY_FORMAT, value));

        element.gameObject.SetActive(true);

        element.TimeRect.DOAnchorPosY(anchoredPosition.y + 150, displayTimeS)
            .OnComplete(
            () => {
                element.gameObject.SetActive(false);
                SpawnedTimeInfoPool.Push(element);
            });

        element.TimeText.DOFade(1f, displayTimeS);
    }

    private Vector2 GetRandomRectPosition()
    {
        float randomX = RandomMath.RandomRangeUnity(-spawnDeviation.x, spawnDeviation.x);
        float randomY = RandomMath.RandomRangeUnity(-spawnDeviation.y, spawnDeviation.y);

        return new Vector2(randomX, randomY);
    }

    private void OnEnable()
    {
        MapsManager.Instance.OnMapSpawned += OnMapSpawnedHandler;

        if(MapsManager.Instance.CurrentMap != null)
        {
            OnMapSpawnedHandler(MapsManager.Instance.CurrentMap);
        }
    }

    private void OnDisable()
    {
        if(MapsManager.Instance != null)
        {
            MapsManager.Instance.OnMapSpawned -= OnMapSpawnedHandler;
        }

        if (CachedLvl != null)
        {
            CachedLvl.OnMoneyAdded -= ShowTextFloatingInfo;
        }
    }

    private void OnMapSpawnedHandler(OfficeLvlMap obj)
    {
        CachedLvl = obj;
        obj.OnMoneyAdded += ShowTextFloatingInfo;
    }

    #endregion

    #region Enums

    #endregion
}
