using UnityEngine;

public class UIAudioTrigger : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private AudioContainerSettings.AudioLabel label;

    #endregion

    #region Propeties

    public AudioContainerSettings.AudioLabel Label { get => label; }

    #endregion

    #region Methods

    public void PlayAudio()
    {
        AudioManager.Instance?.PlayAudioSoundByLabel(Label);
    }

    #endregion

    #region Enums



    #endregion
}
