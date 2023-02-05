using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationController
{
    #region Fields

    [SerializeField]
    private Animator currentAnimator;
    [SerializeField]
    private string idleBoolName;
    [SerializeField]
    private GameObject runParticles;
    [SerializeField]
    private SerializableDictionary<AnimationType, string> animatorStatesMap = new SerializableDictionary<AnimationType, string>();

    #endregion

    #region Propeties

    public string IdleBoolName {
        get => idleBoolName;
    }
    public GameObject RunParticles { 
        get => runParticles;
    }

    private string LastBoolParameterName { get; set; } = string.Empty;
    public GameObject CurrentSpawnedTool { get; set; } = null;

    #endregion

    #region Methods

    public void SetIdle(bool isIdle)
    {
        currentAnimator.SetBool(IdleBoolName, isIdle);
    }

    public void RefreshAnimationState(AnimationType state)
    {
        if(string.IsNullOrEmpty(LastBoolParameterName) == false)
        {
            currentAnimator.SetBool(LastBoolParameterName, false);
            LastBoolParameterName = string.Empty;
        }

        if(animatorStatesMap.TryGetValue(state, out string animationTriggerName) == true)
        {
            currentAnimator.SetBool(animationTriggerName, true);
            LastBoolParameterName = animationTriggerName;
        }
    }

    #endregion

    #region Enums

    public enum AnimationType
    {
        NORMAL,
        STACK_RUN
    }

    #endregion
}
