using UnityEngine;
using System.Collections;
using GeekBox.UI;

namespace GeekBox.UI
{
    public class ValueToggle : TextColorToggle
    {
        #region Fields

        [Header("Value Settings")]
        [SerializeField]
        private float value;

        #endregion

        #region Propeties

        public float Value { 
            get => value; 
            private set => this.value = value; 
        }

        #endregion

        #region Methods

        public void SetValue(float newValue)
        {
            Value = newValue;
        }

        #endregion

        #region Enums



        #endregion
    }
}

