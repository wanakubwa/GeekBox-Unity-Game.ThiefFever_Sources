using UnityEngine;
using System.Collections;
using TMPro;

public class UILvlDisplayer : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI lvlText;
    [SerializeField]
    private string lvlTextFormat = "Level {0}";

    #endregion

    #region Propeties

    public TextMeshProUGUI LvlText { get => lvlText; }
    public string LvlTextFormat { get => lvlTextFormat; }


    // Variables.
    private PlayerManager CachedManager { get; set; }
    #endregion

    #region Methods

    private void Start()
    {
        CachedManager = PlayerManager.Instance;
        RefreshLvlText(CachedManager.Wallet.CurrentLvlNo);
    }

    private void OnEnable()
    {
        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnLvlNoChange += RefreshLvlText;

            if(CachedManager != null)
            {
                RefreshLvlText(CachedManager.Wallet.CurrentLvlNo);
            }
        }

        if(SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted += OnGameReset;
        }
    }

    private void OnGameReset()
    {
        RefreshLvlText(PlayerManager.Instance.Wallet.CurrentLvlNo);
    }

    private void OnDisable()
    {
        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnLvlNoChange -= RefreshLvlText;
        }

        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= OnGameReset;
        }
    }

    private void RefreshLvlText(int lvlNo)
    {
        LvlText.SetText(string.Format(LvlTextFormat, lvlNo));
    }

    #endregion

    #region Enums



    #endregion
}
