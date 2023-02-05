using UnityEngine;

class RandomMath
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static float RandomRangeUnity(float minValue, float maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    public static bool IsSuccessPercent(int successChancePercent)
    {
        float random = RandomRangeUnity(0f, 100f);
        return random < successChancePercent;
    }

    #endregion

    #region Enums



    #endregion
}
