using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JammerBox : TimeInteractable
{
    #region Fields

    [SerializeField]
    private float jammDurationS = 5f;
    [SerializeField]
    private float lightJammedValue = 0.4f;

    #endregion

    #region Propeties

    private List<IJammable> JammableObjects { get; set; } = new List<IJammable>();
    private Coroutine JammCoroutine { get; set; } = null;
    private bool IsJammingEnabled { get; set; } = false;
    private Light SceneLight { get; set; } = null;
    private float NormalLightValue { get; set; }

    #endregion

    #region Methods

    protected override bool CanInteract()
    {
        return base.CanInteract() && IsJammingEnabled == false;
    }

    protected override void OnTimerStarted()
    {
        base.OnTimerStarted();

        Player.BubbleUI.SetJammerInfoVisible(true);
    }

    protected override void OnTimerEnded()
    {
        base.OnTimerEnded();

        Player.BubbleUI.SetJammerInfoVisible(false);
        IsJammingEnabled = true;

        JammableObjects.ForEach(x => x.SetJammed(true));
        JammCoroutine = StartCoroutine(_JammRoutine());
        SceneLight.intensity = lightJammedValue;
    }

    protected override void OnTimerUpdate(float currentTimeS)
    {
        base.OnTimerUpdate(currentTimeS);

        Player.BubbleUI.UpdateJammerProgress(currentTimeS / WaitTimeS);
    }

    protected override void OnTimerInterrupted()
    {
        base.OnTimerInterrupted();

        Player.BubbleUI.SetJammerInfoVisible(false);
    }

    private void Start()
    {
        // Nie jest to optymalne ale w tym przypadku nie powoduje problemow z wydajnoscia.
        JammableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IJammable>().ToList();
        SceneLight = GameObject.FindGameObjectWithTag("Light_Main").GetComponent<Light>();
        NormalLightValue = SceneLight.intensity;
    }

    private void OnDisable()
    {
        if(SceneLight != null)
        {
            SceneLight.intensity = NormalLightValue;
        }
    }

    private void OnJammEnded()
    {
        IsJammingEnabled = false;
        SceneLight.intensity = NormalLightValue;
        JammableObjects.ForEach(x => x.SetJammed(false));
    }

    private IEnumerator _JammRoutine()
    {
        float currentTimeS = Constants.DEFAULT_VALUE;

        while (true)
        {
            float progressNormalized = currentTimeS / jammDurationS;
            JammableObjects.ForEach(x => x.UpdateJammedState(progressNormalized));

            if(currentTimeS >= jammDurationS)
            {
                OnJammEnded();
                break;
            }

            currentTimeS += Time.deltaTime;
            yield return null;
        }
    }

    #endregion

    #region Enums



    #endregion
}