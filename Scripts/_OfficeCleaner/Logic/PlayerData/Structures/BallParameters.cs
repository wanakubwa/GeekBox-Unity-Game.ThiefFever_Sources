public struct BallParameters
{
    #region Fields

    #endregion

    #region Propeties

    public int StartUnitsBoost { get; private set; }

    #endregion

    #region Methods

    public BallParameters(int speedLimitBoost)
    {
        StartUnitsBoost = speedLimitBoost;
    }

    #endregion

    #region Enums



    #endregion
}
