using UnityEngine;
using System.Collections;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TMP_ColorTintButton : ButtonBase
{

    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI buttonText;

    [Space, Title("Animations settings")]
    [SerializeField]
    private float idleDurationS;
    [SerializeField]
    private float selectedDurationS;
    [SerializeField]
    private Color targetIdleColor = Color.clear;
    [SerializeField]
    private Color startIdleColor = Color.white;
    [SerializeField]
    private Color selectedColor;


    #endregion

    #region Propeties

    public TextMeshProUGUI ButtonText { 
        get => buttonText; 
    }

    public float IdleDurationS { 
        get => idleDurationS; 
    }

    public float SelectedDurationS { 
        get => selectedDurationS;  
    }

    public Color TargetIdleColor { 
        get => targetIdleColor; 
    }

    public Color SelectedColor {
        get => selectedColor;
    }

    public Color StartIdleColor { 
        get => startIdleColor;
    }

    #endregion

    #region Methods

    public override void OnEnable()
    {
        base.OnEnable();

        StartIdleAnimation();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        ButtonText.DOKill();
    }

    public override void OnSelected()
    {
        base.OnSelected();

        ButtonText.DOColor(SelectedColor, SelectedDurationS);
    }

    public override void KillAllAnimations(bool complete = false)
    {
        base.KillAllAnimations(complete);

        ButtonText.DOKill(complete);
    }

    public override void ResetButton()
    {
        base.ResetButton();
        StartIdleAnimation();
    }

    private void StartIdleAnimation()
    {
        ButtonText.color = StartIdleColor;
        ButtonText.DOColor(TargetIdleColor, IdleDurationS).SetLoops(-1, LoopType.Yoyo);
    }

    #endregion

    #region Handlers



    #endregion
}
