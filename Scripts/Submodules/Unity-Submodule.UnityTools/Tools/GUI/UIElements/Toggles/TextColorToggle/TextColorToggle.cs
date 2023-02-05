using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace GeekBox.UI
{
    public class TextColorToggle : Toggle
    {
        #region Fields

        [Header("Sprite Settings")]
        [SerializeField]
        private Image targetImage;
        [SerializeField]
        private Sprite activeSprite;
        [SerializeField]
        private Sprite inactiveSprite;

        [Header("Text Settings")]
        [SerializeField]
        private TextMeshProUGUI labelText;
        [SerializeField]
        private Color activeTextColor;
        [SerializeField]
        private Color inactiveTextColor;

        #endregion

        #region Propeties

        public TextMeshProUGUI LabelText
        {
            get => labelText;
        }

        public Color ActiveTextColor
        {
            get => activeTextColor;
        }

        public Color InactiveTextColor
        {
            get => inactiveTextColor;
        }

        public Sprite ActiveSprite { 
            get => activeSprite; 
        }

        public Sprite InactiveSprite { 
            get => inactiveSprite; 
        }

        public Image TargetImage { 
            get => targetImage; 
        }

        #endregion

        #region Methods

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            CheckCurrentStatus(isOn);
        }

        public void CheckCurrentStatus(bool isOnStatus)
        {
            if (isOnStatus == true)
            {
                SetActiveState();
            }
            else
            {
                SetInactiveState();
            }
        }

        public void SetIsOnWithoutNotifyFixed(bool isOnStatus)
        {
            base.SetIsOnWithoutNotify(isOnStatus);
            CheckCurrentStatus(isOn);
        }

        protected override void Awake()
        {
            base.Awake();

            onValueChanged.AddListener(CheckCurrentStatus);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            onValueChanged.RemoveListener(CheckCurrentStatus);
        }

        private void SetActiveState()
        {
            LabelText.color = ActiveTextColor;
            TargetImage.sprite = ActiveSprite;
        }

        private void SetInactiveState()
        {
            LabelText.color = InactiveTextColor;
            TargetImage.sprite = InactiveSprite;
        }

        #endregion

        #region Enums



        #endregion
    }
}

