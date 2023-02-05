using System;
using UnityEngine;

namespace StackSystem
{
    public class StackableObject : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Vector3 onStackScale = Vector3.one;
        [SerializeField]
        private float onStackRotationYDegree;
        [SerializeField]
        private MeshFilter objectMesh;

        #endregion

        #region Propeties

        public Vector3 OnStackScale { get => onStackScale; }
        public float OnStackRotationYDegree { get => onStackRotationYDegree; }

        #endregion

        #region Methods

        public float GetStackVisualizationHeight()
        {
            return objectMesh.sharedMesh.bounds.size.y * OnStackScale.y;
        }

        public virtual void DestroyObject()
        {
            Destroy(gameObject);
        }

        private void Reset()
        {
            objectMesh = GetComponent<MeshFilter>();
        }

        #endregion

        #region Enums



        #endregion
    }
}
