using UnityEngine;

public class Gate : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private string requiredKeyLabel;

    [Space]
    [SerializeField]
    private GameObject lockedGate;
    [SerializeField]
    private GameObject unlockedGate;

    #endregion

    #region Propeties

    #endregion

    #region Methods

    private void Start()
    {
        SetDefault();

        GameplayEvents.Instance.OnKeyColected += OnKeyColectedHandler;
    }

    private void OnDestroy()
    {
        if(GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnKeyColected -= OnKeyColectedHandler;
        }
    }

    private void SetDefault()
    {
        lockedGate.SetActive(true);
        unlockedGate.SetActive(false);
    }

    private void OnKeyColectedHandler(string obj)
    {
        if(obj == requiredKeyLabel)
        {
            lockedGate.SetActive(false);
            unlockedGate.SetActive(true);
        }
    }

    #endregion

    #region Enums



    #endregion
}
