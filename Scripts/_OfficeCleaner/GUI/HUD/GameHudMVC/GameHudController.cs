using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameHudModel), typeof(GameHudView))]
public class GameHudController : UIController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void OnResetButtonClick()
    {
        GetModel<GameHudModel>().ResetLvl();
    }

    public override void Initialize()
    {
        base.Initialize();

        GamePlayManager.Instance.OnGameStart += GameStartHandler;
        GamePlayManager.Instance.OnGameStop += GameStopHandler;

        GameStopHandler();
    }

    private void OnDestroy()
    {
        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnGameStart -= GameStartHandler;
            GamePlayManager.Instance.OnGameStop -= GameStopHandler;
        }
    }

    private void GameStartHandler()
    {
        gameObject.SetActive(true);
        GetView<GameHudView>().GameStart();
    }

    private void GameStopHandler()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Enums



    #endregion
}
