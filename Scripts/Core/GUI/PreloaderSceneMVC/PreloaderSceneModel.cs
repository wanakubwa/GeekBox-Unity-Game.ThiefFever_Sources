//using GeekBox.Ads;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PreloaderSceneModel : UIModel
{
    #region Fields

    [SerializeField]
    private SceneLabel nextSceneLabel;

    #endregion

    #region Propeties

    public SceneLabel NextSceneLabel { 
        get => nextSceneLabel; 
    }

    private List<IInitializable> InitializeManagers
    {
        get;
        set;
    } = new List<IInitializable>();

    private int InitializedCounter {
        get;
        set;
    } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        InitializeManagers = new List<IInitializable> { 
            //StreamingAssetsManager.Instance,
            //EasyMobileManager.Instance
        };

        MEC.Timing.RunCoroutine(_InitializeManagers());
    }

    public void LoadNextScene()
    {
        GameManager.Instance.LoadTargetScene(NextSceneLabel);
    }

    private IEnumerator<float> _InitializeManagers()
    {
        yield return Timing.WaitForSeconds(1f);

        if(InitializeManagers.Count < 1)
        {
            HandleManagerInitialized();
        }
        else
        {
            foreach (IInitializable initializable in InitializeManagers)
            {
                initializable.OnInitialized += HandleManagerInitialized;

                try
                {
                    initializable.Initialize();
                }
                catch (Exception ex)
                {
                    Debug.LogFormat("INIT ERROR! msg: {0}", ex.Message);
                    HandleManagerInitialized();
                }

                yield return MEC.Timing.WaitForOneFrame;
            }
        }
    }

    private void HandleManagerInitialized()
    {
        InitializedCounter++;
        if(InitializedCounter >= InitializeManagers.Count)
        {
            LoadNextScene();
        }
    }

    #endregion

    #region Enums



    #endregion
}
