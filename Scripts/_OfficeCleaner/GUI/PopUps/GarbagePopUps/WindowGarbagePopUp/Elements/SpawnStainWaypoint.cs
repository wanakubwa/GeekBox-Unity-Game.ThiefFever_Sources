using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace GarbagePopUps.WindowGarbagePopUp
{
    public class SpawnStainWaypoint : MonoBehaviour
    {
        #region Fields

        [SerializeField, ReadOnly]
        private int id = Guid.NewGuid().GetHashCode();

        #endregion

        #region Propeties

        public int Id
        {
            get => id;
        }

        #endregion

        #region Methods

        private void Reset()
        {
            id = Guid.NewGuid().GetHashCode();
        }

        #endregion

        #region Enums



        #endregion
    }
}

