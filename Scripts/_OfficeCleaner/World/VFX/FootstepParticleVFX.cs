using UnityEngine;
using DG.Tweening;
using System;

public class FootstepParticleVFX : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float disableTimeS = 0.5f;
    [SerializeField]
    private SpriteRenderer footstepSprite;

    #endregion

    #region Propeties

    private Action<FootstepParticleVFX> OnFootstepDisabled { get; set; } = delegate { };

    #endregion

    #region Methods

    public void Init(Action<FootstepParticleVFX> onFootstepDisabledCallback)
    {
        OnFootstepDisabled = onFootstepDisabledCallback;

        footstepSprite.color = Color.white;
        footstepSprite.DOFade(Constants.DEFAULT_VALUE, disableTimeS)
            .OnComplete(() => { OnFootstepDisabled(this); });
    }

    #endregion

    #region Enums



    #endregion
}