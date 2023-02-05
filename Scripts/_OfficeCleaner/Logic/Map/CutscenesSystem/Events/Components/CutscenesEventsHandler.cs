using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CutscenesSystem
{
    public class CutscenesEventsHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private UnityEvent onCutsceneStarted = new UnityEvent();
        [SerializeField]
        private UnityEvent onCutsceneEnded = new UnityEvent();

        #endregion

        #region Propeties

        private CutscenesEvents CachedEvents { get; set; }

        #endregion

        #region Methods

        private void Awake()
        {
            CachedEvents = CutscenesEvents.Instance;
        }

        private void OnEnable()
        {
            CachedEvents.OnCutsceneStarted += OnCutsceneStartedHandler;
            CachedEvents.OnCutsceneEnded += OnCutsceneEndedHandler;
        }

        private void OnDisable()
        {
            if(CachedEvents != null)
            {
                CachedEvents.OnCutsceneStarted -= OnCutsceneStartedHandler;
                CachedEvents.OnCutsceneEnded -= OnCutsceneEndedHandler;
            }
        }

        private void OnCutsceneEndedHandler()
        {
            onCutsceneEnded.Invoke();
        }

        private void OnCutsceneStartedHandler()
        {
            onCutsceneStarted.Invoke();
        }

        #endregion

        #region Enums



        #endregion
    }
}