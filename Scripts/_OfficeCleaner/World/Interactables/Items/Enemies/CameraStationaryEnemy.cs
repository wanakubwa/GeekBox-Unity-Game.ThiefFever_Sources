using UnityEngine;
using UnityEngine.UI;

public class CameraStationaryEnemy : MonoBehaviour, IJammable
{
    #region Fields

    [SerializeField]
    private float rotationTimeS = 0.25f;

    [Space]
    [SerializeField]
    private float triggerWaitTimeS = 2f;
    [SerializeField]
    private ConeOfSightRaycaster coneRaycaster;
    [SerializeField]
    private GameObject coneVisualization;

    [Header("UI")]
    [SerializeField]
    private GameObject triggerInfo;
    [SerializeField]
    private Image triggerProgressSlider;

    [Space]
    [SerializeField]
    private GameObject jammingInfo;
    [SerializeField]
    private Image jammingProgressSlider;

    #endregion

    #region Propeties

    private BehaviourState CurrentState { get; set; } = BehaviourState.PATROL;
    private float CurrentWaitTimeS { get; set; } = Constants.DEFAULT_VALUE;
    private bool IsJammed { get; set; } = false;

    #endregion

    #region Methods

    public void OnTriggerEnterRange()
    {
        CurrentWaitTimeS = Constants.DEFAULT_VALUE;
        triggerProgressSlider.fillAmount = CurrentWaitTimeS;
        triggerInfo.SetActive(true);

        CurrentState = BehaviourState.TRIGGERED;
    }

    public void OnTriggerExitRange()
    {
        triggerInfo.SetActive(false);

        CurrentState = BehaviourState.PATROL;
    }

    public void SetJammed(bool isJammed)
    {
        IsJammed = isJammed;
        coneRaycaster.enabled = !isJammed;
        coneVisualization.gameObject.SetActive(!isJammed);
        jammingInfo.SetActive(isJammed);
    }

    public void UpdateJammedState(float progressNormalized)
    {
        jammingProgressSlider.fillAmount = 1 - progressNormalized;
    }

    private void Update()
    {
        // == false.
        if (!IsJammed)
        {
            UpdateState();
        }
    }

    private void UpdateState()
    {
        switch (CurrentState)
        {
            case BehaviourState.TRIGGERED:
                UpdateTriggeredState();
                break;
            case BehaviourState.ATTACk:
                break;
        }
    }

    private void UpdateTriggeredState()
    {
        CurrentWaitTimeS += Time.deltaTime;
        triggerProgressSlider.fillAmount = CurrentWaitTimeS / triggerWaitTimeS;

        if (CurrentWaitTimeS >= triggerWaitTimeS)
        {
            CurrentState = BehaviourState.ATTACk;
            GamePlayManager.Instance.LvlFailed();
        }
    }

    #endregion

    #region Enums

    public enum BehaviourState
    {
        PATROL = 0,
        TRIGGERED = 1,
        ATTACk = 2
    }

    #endregion
}