using UnityEngine;

public class GeometryMath
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static Vector2 GetDirectionBetweenPoins2D(Vector2 first, Vector2 second)
    {
        Vector2 delta = second - first;
        return delta.normalized;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 dir = point - pivot;
        dir = rotation * dir;
        point = dir + pivot;
        return point;
    }

    #endregion

    #region Enums



    #endregion
}
