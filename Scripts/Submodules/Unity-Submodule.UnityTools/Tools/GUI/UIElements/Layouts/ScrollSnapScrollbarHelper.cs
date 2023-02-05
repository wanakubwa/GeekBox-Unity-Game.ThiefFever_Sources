using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
    [DisallowMultipleComponent]
    public class ScrollSnapScrollbarHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        internal IScrollSnap ss;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnScrollBarDown();
        }

        public void OnDrag(PointerEventData eventData)
        {
            ss.CurrentPage();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnScrollBarUp();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnScrollBarDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnScrollBarUp();
        }

        void OnScrollBarDown()
        {
            if (ss != null)
            {
                ss.SetLerp(false);
                ss.StartScreenChange();
            }
        }

        void OnScrollBarUp()
        {
            ss.SetLerp(true);
            ss.ChangePage(ss.CurrentPage());
        }
    }
}