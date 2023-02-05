using UnityEngine;
using System.Collections;
using TMPro;

public class NotificationIcon : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI notificationCounterText;

    #endregion

    #region Propeties

    public TextMeshProUGUI NotificationCounterText {
        get => notificationCounterText; 
        private set => notificationCounterText = value; 
    }

    public int NotificationCounter {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void RegisterNotification()
    {
        NotificationCounter++;
        NotificationCounterText.text = NotificationCounter.ToString();

        gameObject.SetActive(true);
    }

    public void ResetNotification()
    {
        NotificationCounter = 0;
        NotificationCounterText.text = NotificationCounter.ToString();

        gameObject.SetActive(false);
    }

    #endregion

    #region Handlers



    #endregion
}
