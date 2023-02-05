using System.Collections;
using TMPro;

namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(RectTransform))]
    public class AutoTMPScroll : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI textReference;
        [SerializeField]
        private float scrollSpeed;
        [SerializeField]
        private float waitTimeS;

        #endregion

        #region Propeties

        public TextMeshProUGUI TextReference {
            get => textReference; 
        }
        public float ScrollSpeed {
            get => scrollSpeed;
        }

        // Variables.
        private WaitForSeconds JumpWaiter { get; set; }
        private RectTransform WrapperRect { get; set; }
        private RectTransform TextRect { get; set; }
        private Coroutine ScrollTextCoroutineHandler { get; set; }
        private Vector3 StartAnchoredPosition { get; set; }

        #endregion

        #region Methods

        #region API

        public void RefreshTextScroll()
        {
            TextRect.anchoredPosition = Vector3.zero;
            if (ScrollTextCoroutineHandler != null)
            {
                StopCoroutine(ScrollTextCoroutineHandler);
            }

            ScrollTextCoroutineHandler = StartCoroutine(_ScrollTextCoroutine());
        }

        #endregion

        private void Awake()
        {
            JumpWaiter = new WaitForSeconds(waitTimeS);
            WrapperRect = GetComponent<RectTransform>();
            TextRect = TextReference.GetComponent<RectTransform>();
        }

        private void Start()
        {
            //StartLocalPosition = TextRect.localPosition;
            RefreshTextScroll();
        }

        private IEnumerator _ScrollTextCoroutine()
        {
            yield return new WaitForEndOfFrame();

            // Reset data.
            float scrollPosition = 0f;
            float moveYMax = TextRect.rect.height - WrapperRect.rect.height;

            if (moveYMax <= 0)
            {
                yield break;
            }

            while (true)
            {
                Vector3 textPosition = new Vector3(TextRect.localPosition.x, scrollPosition % moveYMax, TextRect.localPosition.z);
                TextRect.anchoredPosition = textPosition;

                if (scrollPosition >= moveYMax)
                {
                    // przeskok nastpil w tej klatce. - reset + odczekanie.
                    scrollPosition = 0f;
                    yield return JumpWaiter;
                }

                scrollPosition = scrollPosition + ScrollSpeed * Time.deltaTime;

                yield return null;
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
