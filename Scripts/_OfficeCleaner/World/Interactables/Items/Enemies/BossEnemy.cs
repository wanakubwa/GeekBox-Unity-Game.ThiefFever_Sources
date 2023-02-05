using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float rotationTimeS = 0.25f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float runSpeed;

    [Space]
    [SerializeField]
    private GameObject targetWaypoints;

    [Space]
    [SerializeField]
    private float triggerWaitTimeS = 2f;
    [SerializeField]
    private GameObject triggerInfo;
    [SerializeField]
    private Image triggerProgressSlider;

    [Space]
    [SerializeField]
    private Animator currentAnimator;
    [SerializeField]
    private string walkBoolName;
    [SerializeField]
    private string runBoolName;
    [SerializeField]
    private string shockBoolName;

    [Header("Footprints")]
    [SerializeField]
    private float spawnFootprintDelayS;
    [SerializeField]
    private float sideStepDeviation = 0.1f;

    #endregion

    #region Propeties

    private List<Vector3> WaypointsPositions { get; set; } = new List<Vector3>();
    private Vector3 NextWaypoint { get; set; } = Vector3.zero;
    private int NextWaypointIndex { get; set; } = 0;

    private BehaviourState CurrentState { get; set; } = BehaviourState.PATROL;
    private float CurrentWaitTimeS { get; set; } = Constants.DEFAULT_VALUE;
    private PlayerCharacterController Player { get; set; } = null;
    private bool CanMove { get; set; } = false;

    private string LastBoolParameterName { get; set; } = string.Empty;
    private float FootprintCounterS { get; set; } = Constants.DEFAULT_VALUE;
    private bool WasLeftFootprint { get; set; } = false;

    #endregion

    #region Methods

    // Animator call.
    [JetBrains.Annotations.UsedImplicitly]
    public void SetAttackState()
    {
        CurrentState = BehaviourState.ATTACk;
        RefreshAnimationState(runBoolName);
    }

    public void OnTriggerEnterRange()
    {
        if(CurrentState == BehaviourState.PATROL)
        {
            CurrentWaitTimeS = Constants.DEFAULT_VALUE;
            triggerProgressSlider.fillAmount = CurrentWaitTimeS;
            triggerInfo.SetActive(true);

            CurrentState = BehaviourState.TRIGGERED;
        }
    }

    public void OnTriggerExitRange()
    {
        triggerInfo.SetActive(false);

        if(CurrentState == BehaviourState.TRIGGERED)
        {
            CurrentState = BehaviourState.PATROL;
            RefreshAnimationState(walkBoolName);
        }
    }

    private void Start()
    {
        int waypointsCount = targetWaypoints.transform.childCount;
        for (int i = 0; i < waypointsCount; i++)
        {
            Transform childTransform = targetWaypoints.transform.GetChild(i);
            WaypointsPositions.Add(new Vector3(childTransform.position.x, transform.position.y, childTransform.position.z));
        }

        transform.position = WaypointsPositions[0];

        CanMove = true;
        ChangeWaypoint();
    }

    private void SetPatrolState()
    {
        triggerInfo.SetActive(false);
        CurrentState = BehaviourState.PATROL;
        RefreshAnimationState(walkBoolName);
    }

    private void SetShockState()
    {
        RefreshAnimationState(shockBoolName);
        CurrentState = BehaviourState.SHOCK;
    }

    private void Update()
    {
        if (CanMove)
        {
            UpdateState();
            RefreshFootprint();
        }
    }

    private void UpdateState()
    {
        switch (CurrentState)
        {
            case BehaviourState.PATROL:
                UpdatePatrolState();
                break;
            case BehaviourState.TRIGGERED:
                UpdateTriggeredState();
                break;
            case BehaviourState.ATTACk:
                UpdateAttackState();
                break;
        }
    }

    private void UpdateAttackState()
    {
        if(Player.IsHide == false)
        {
            Vector3 targetPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            transform.LookAt(targetPosition);

            Vector3 move = (targetPosition - transform.position).normalized * runSpeed * Time.deltaTime;
            transform.position = transform.position + new Vector3(move.x, Constants.DEFAULT_VALUE, move.z);

            if (GamePlayManager.Instance.IsGameplayRun == true && Vector3.Distance(transform.position, targetPosition) < 0.25f)
            {
                // Gracz zlapany.
                CanMove = false;
                GamePlayManager.Instance.LvlFailed();
            }
        }
        else
        {
            // Ustawienie na nastepny waypoint.
            ChangeWaypoint();
            SetPatrolState();
        }
    }

    private void UpdateTriggeredState()
    {
        CurrentWaitTimeS += Time.deltaTime;
        triggerProgressSlider.fillAmount = CurrentWaitTimeS / triggerWaitTimeS;

        if (CurrentWaitTimeS >= triggerWaitTimeS)
        {
            SetShockState();
            Player = PlayerManager.Instance.CurrentPlayer;
        }
    }

    private void UpdatePatrolState()
    {
        Vector3 move = (NextWaypoint - transform.position).normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(move.x, Constants.DEFAULT_VALUE, move.z);

        if (Vector3.Distance(transform.position, NextWaypoint) < 0.05f)
        {
            ChangeWaypoint();
        }
    }

    private void ChangeWaypoint()
    {
        if(WaypointsPositions.Count <= NextWaypointIndex)
        {
            NextWaypointIndex = 0;
        }

        NextWaypoint = WaypointsPositions[NextWaypointIndex];

        Quaternion lastRotation = transform.rotation;
        transform.LookAt(NextWaypoint);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = lastRotation;
        transform.DORotateQuaternion(targetRotation, rotationTimeS);

        NextWaypointIndex++;
    }

    private void RefreshAnimationState(string animationBoolName)
    {
        if (string.IsNullOrEmpty(LastBoolParameterName) == false)
        {
            currentAnimator.SetBool(LastBoolParameterName, false);
            LastBoolParameterName = string.Empty;
        }

        currentAnimator.SetBool(animationBoolName, true);
        LastBoolParameterName = animationBoolName;
    }

    private void RefreshFootprint()
    {
        FootprintCounterS += Time.deltaTime;
        if(FootprintCounterS > spawnFootprintDelayS)
        {
            Vector3 scaleModifier = WasLeftFootprint == true ? Vector3.one : new Vector3(-1f, 1f, 1f);
            WasLeftFootprint = !WasLeftFootprint;

            VFXManager.Instance.SpawnFootprintParticle(gameObject.transform, scaleModifier, sideStepDeviation);
            FootprintCounterS = Constants.DEFAULT_VALUE;
        }
    }

    #endregion

    #region Enums

    public enum BehaviourState
    {
        PATROL = 0,
        TRIGGERED = 1,
        ATTACk = 2,
        SHOCK = 3
    }

    #endregion
}