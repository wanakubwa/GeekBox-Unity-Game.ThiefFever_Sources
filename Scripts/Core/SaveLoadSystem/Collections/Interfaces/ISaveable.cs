
namespace SaveLoadSystem
{
    public interface ISaveable : IResettable
    {
        #region Fields



        #endregion

        #region Propeties

        bool IsLoaded { get; }

        #endregion

        #region Methods

        void Save(string directoryPath = null);
        void Load(string directoryPath = null);

        #endregion

        #region Enums



        #endregion
    }
}

