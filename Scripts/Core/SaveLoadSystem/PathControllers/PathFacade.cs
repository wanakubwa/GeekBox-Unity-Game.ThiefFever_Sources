
public class PathFacade
{
    #region Fields

    private static PathControllerBase currentPath = null;

    #endregion

    #region Propeties

    public static PathControllerBase CurrentPath  {
        get
        {
            if (currentPath == null)
            {
                Init();
            }

            return currentPath;
        }

        private set => currentPath = value;
    }

    #endregion

    #region Methods

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        CurrentPath = new AndroidPathController();
#else
        CurrentPath = new PathControllerBase();
#endif

        CurrentPath.Init();
    }

    #endregion

    #region Enums



    #endregion
}
