using UnityEngine;
using TMPro;
using Sirenix.Utilities;

[ExecuteInEditMode]
public class LocalizedTMPro : TextMeshProUGUI
{
    #region Fields

    [SerializeField]
    private string localizedKey;

    #endregion

    #region Propeties

    public string LocalizedKey { 
        get => localizedKey; 
        set => localizedKey = value; 
    }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        SetLocalizedContentText();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if(LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChange += SetLocalizedContentText;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();


        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChange -= SetLocalizedContentText;
        }
    }

    public void SetLocalizedContentText()
    {
        LanguageManager languageManager = LanguageManager.Instance;
        if(languageManager != null)
        {
            string localizedText = languageManager.GetTextByKey(LocalizedKey);
            if(localizedText.IsNullOrWhitespace() == false && localizedText.Equals(LocalizedKey) == false)
            {
                text = localizedText;
            }
        }
    }

    public void SetKey(string key)
    {
        LanguageManager languageManager = LanguageManager.Instance;
        if (languageManager != null)
        {
            string localizedText = languageManager.GetTextByKey(key);
            text = localizedText;
        }
    }

    #endregion

    #region Handlers



    #endregion
}
