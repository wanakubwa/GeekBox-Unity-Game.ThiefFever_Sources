using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GarbagePopUps.WindowGarbagePopUp
{
    public class StainImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IIDEquatable
    {
        #region Fields

        [SerializeField]
        private float durability = 10f;
        [SerializeField]
        private Image targetImage;

        #endregion

        #region Propeties

        public float Durability {
            get => durability; 
        }
        public Image TargetImage { 
            get => targetImage;
        }

        // Variables.
        public int ID { get; set; }
        private Action<int> OnCompletedCallback { get; set; }
        private float CurrentDurability { get; set; } = Constants.DEFAULT_VALUE;
        private Vector2 LastPosition { get; set; } = Vector2.positiveInfinity;

        #endregion

        #region Methods

        public void SetInfo(Action<int> onCompleted)
        {
            ID = Guid.NewGuid().GetHashCode();
            OnCompletedCallback = onCompleted;
            CurrentDurability = Durability;

            RefreshImage();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            LastPosition = eventData.position;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LastPosition = Vector2.positiveInfinity;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if(LastPosition != Vector2.positiveInfinity)
            {
                float delta = Vector3.Distance(LastPosition, eventData.position);
                CurrentDurability -= delta;
                RefreshImage();

                if (CurrentDurability <= Constants.DEFAULT_VALUE)
                {
                    OnCompletedCallback(ID);
                }
            }

            LastPosition = eventData.position;
        }

        private void RefreshImage()
        {
            float alpha = Mathf.Clamp01(CurrentDurability / Durability);
            Color color =  new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, alpha);
            TargetImage.color = color;
        }

        public bool IDEqual(int otherId)
        {
            return otherId == ID;
        }

        #endregion

        #region Enums



        #endregion
    }
}
