using SaveLoadSystem;
using UnityEngine.SceneManagement;

public class AppLifecycleManager : ManagerSingletonBase<AppLifecycleManager>
{
    #region Fields



    #endregion

    #region Propeties

    #endregion

    #region Methods

    private void OnApplicationPause(bool pause)
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager == null || CheckValidScene(gameManager.ActualSetScene) == false)
        {
            return;
        }

        if (pause == true)
        {
            gameManager.SaveGame();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager gameManager = GameManager.Instance;
        if (gameManager == null || CheckValidScene(gameManager.ActualSetScene) == false)
        {
            return;
        }

        gameManager.SaveGame();
    }

    private bool CheckValidScene(SceneLabel currentLabel)
    {
        return currentLabel == SceneLabel.GAME || currentLabel == SceneLabel.MAIN_MENU;
    }

    #endregion

    #region Enums



    #endregion
}
