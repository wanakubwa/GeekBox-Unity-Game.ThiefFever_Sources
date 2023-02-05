using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonAudioTrigger : UIAudioTrigger
{
    #region Fields

    #endregion

    #region Propeties

    private Button BtnReference { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        BtnReference = GetComponent<Button>();
    }

    private void OnEnable()
    {
        BtnReference.onClick.AddListener(PlayAudio);
    }

    private void OnDisable()
    {
        BtnReference.onClick.RemoveListener(PlayAudio);
    }

    #endregion

    #region Enums

    #endregion
}
