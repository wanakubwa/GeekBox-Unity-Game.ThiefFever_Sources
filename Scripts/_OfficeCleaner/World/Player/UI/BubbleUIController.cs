using UnityEngine;
using UnityEngine.UI;

public class BubbleUIController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject hideProgressInfo;
    [SerializeField]
    private Image hideProgressSlider;

    [SerializeField]
    private GameObject exitProgressInfo;
    [SerializeField]
    private Image exitProgressSlider;

    [SerializeField]
    private GameObject jammerProgressInfo;
    [SerializeField]
    private Image jammerProgressSlider;

    #endregion

    #region Propeties


    #endregion

    #region Methods

    // Hide.
    public void SetHideInfoVisible(bool isVisible)
    {
        hideProgressInfo.SetActive(isVisible);
        hideProgressSlider.fillAmount = Constants.DEFAULT_VALUE;
    }

    public void UpdateHideProgress(float ammount)
    {
        hideProgressSlider.fillAmount = ammount;
    }

    // Exit.
    public void SetExitInfoVisible(bool isVisible)
    {
        exitProgressInfo.SetActive(isVisible);
        exitProgressSlider.fillAmount = Constants.DEFAULT_VALUE;
    }

    public void UpdateExitProgress(float ammount)
    {
        exitProgressSlider.fillAmount = ammount;
    }

    // Jammer.
    public void SetJammerInfoVisible(bool isVisible)
    {
        jammerProgressInfo.SetActive(isVisible);
        jammerProgressSlider.fillAmount = Constants.DEFAULT_VALUE;
    }

    public void UpdateJammerProgress(float ammount)
    {
        jammerProgressSlider.fillAmount = ammount;
    }

    // Boilerplate.
    private void Awake()
    {
        SetHideInfoVisible(false);
        SetExitInfoVisible(false);
        SetJammerInfoVisible(false);
    }

    #endregion

    #region Enums


    #endregion
}
