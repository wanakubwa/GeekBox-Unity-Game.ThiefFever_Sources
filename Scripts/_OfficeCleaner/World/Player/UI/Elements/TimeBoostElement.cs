using System.Collections;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class TimeBoostElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timeText;
        [SerializeField]
        private RectTransform timeRect;

        public TextMeshProUGUI TimeText { get => timeText; }
        public RectTransform TimeRect { get => timeRect; }
    }
}