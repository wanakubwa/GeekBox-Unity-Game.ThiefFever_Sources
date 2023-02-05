using UnityEngine;
using GarbagePopUps.WindowGarbagePopUp;
using System.Collections.Generic;

public class WindowGarbagePopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private RectTransform stainsParent;
    [SerializeField]
    private Transform fingerFollower;
    [SerializeField]
    private StainImage[] stains;

    #endregion

    #region Propeties

    public StainImage[] Stains {
        get => stains;
    }
    public RectTransform StainsParent {
        get => stainsParent;
    }
    public Transform FingerFollower { 
        get => fingerFollower;
    }

    private List<StainImage> SpawnedStains { get; set; } = new List<StainImage>();
    private WindowGarbagePopUpModel CurrentModel { get; set; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();
        CurrentModel = GetModel<WindowGarbagePopUpModel>();
    }

    public override void CustomStart()
    {
        base.CustomStart();

        SpawnStains(CurrentModel.TotalStains);
    }

    private void Update()
    {
        if(TouchUtility.TouchCount > 0)
        {
            Touch touch = TouchUtility.GetTouch(0);
            FingerFollower.position = touch.position;
        }
    }

    private void SpawnStains(int toSpawn)
    {
        SpawnedStains.ClearDestroy();

        for (int i = 0; i < toSpawn; i++)
        {
            StainImage newStain = Instantiate(Stains.GetRandomElement());
            newStain.transform.ResetParent(StainsParent);
            newStain.transform.position = CurrentModel.StainsWaypoints.GetRandomElement().transform.position;
            newStain.transform.rotation = Quaternion.Euler(
                new Vector3(Constants.DEFAULT_VALUE, Constants.DEFAULT_VALUE, RandomMath.RandomRangeUnity(Constants.DEFAULT_VALUE, 360f)));

            newStain.SetInfo(OnStainRemoved);

            SpawnedStains.Add(newStain);
        }
    }

    private void OnStainRemoved(int id)
    {
        SpawnedStains.DestroyElementByID(id);
        if(SpawnedStains.Count == Constants.DEFAULT_VALUE)
        {
            CurrentModel.OnStainsRemoved();
        }
    }

    #endregion

    #region Enums



    #endregion
}
