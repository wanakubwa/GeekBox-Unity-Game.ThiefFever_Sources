using UnityEngine;

namespace CursorSystem.Interfaces
{
    public interface ICursorFollowerObject : IUpdatable, ICleanable
    {
        void Init(Transform parent);

        void Refresh(Vector3 worldPosition);

        void OnMouseRelease();
    }
}
