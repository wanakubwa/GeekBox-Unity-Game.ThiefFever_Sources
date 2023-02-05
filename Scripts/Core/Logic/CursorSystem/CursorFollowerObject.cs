using CursorSystem.Interfaces;
using UnityEngine;

namespace CursorSystem.FollowersObj
{
    public abstract class CursorFollowerObject<T> : ICursorFollowerObject where T : MonoBehaviour
    {
        #region Fields


        #endregion

        #region Propeties

        public T AttachedObject { get; private set; }
        protected SelectingManager Selecting { get; set; }

        #endregion

        #region Methods

        public virtual void Init(Transform parent)
        {
            Selecting = SelectingManager.Instance;
            AttachedObject = CreateObject(parent);
        }

        public virtual void Refresh(Vector3 worldPosition)
        {
            AttachedObject.transform.position = worldPosition;
        }

        public virtual void OnMouseRelease()
        {

        }

        public void CustomUpdate(float deltaTime)
        {
            bool isVisible = Selecting.IsSelectionAvailable;
            ToggleVisible(isVisible);

            // true
            if (isVisible)
            {
                RefreshVisualization();
            }
        }

        public void CleanData()
        {
            GameObject.Destroy(AttachedObject.gameObject);
        }

        protected virtual void RefreshVisualization()
        {

        }

        protected virtual void ToggleVisible(bool isVisible)
        {
            if(AttachedObject.gameObject.activeInHierarchy != isVisible)
            {
                AttachedObject.gameObject.SetActive(isVisible);
            }
        }

        protected abstract T CreateObject(Transform parent);

        #endregion

        #region Enums



        #endregion
    }
}
