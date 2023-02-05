using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class DialogTextFitter : UIMonoBehavior
{
    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI invisibleTextTemplate;
    [SerializeField]
    private TextMeshProUGUI mainText;

    [Space]
    [SerializeField]
    private float timeLetterDelayS;

    #endregion

    #region Propeties

    public event Action OnTextEndDiplay = delegate { };

    public TextMeshProUGUI InvisibleTextTemplate { 
        get => invisibleTextTemplate; 
    }

    public TextMeshProUGUI MainText { 
        get => mainText; 
    }

    public float TimeLetterDelayS { 
        get => timeLetterDelayS; 
        private set => timeLetterDelayS = value; 
    }

    #endregion

    #region Methods

    public void ResetText()
    {
        InvisibleTextTemplate.text = string.Empty;
        MainText.text = string.Empty;
    }

    public void SetText(string text)
    {
        InvisibleTextTemplate.text = text;

        MainText.text = text;
        MainText.maxVisibleCharacters = 0;
    }

    public void StartDisplayText(string text)
    {
        SetText(text);
        StartManagedCoroutine(PrintTextByLetters(text));
    }

    public void SetTimeLetterDelay(float timeS)
    {
        TimeLetterDelayS = timeS;
    }

    private IEnumerator PrintTextByLetters(string text)
    {
        // Odczekanie w celu przeliczenia layoutu.
        yield return new WaitForEndOfFrame();

        int i = 0;
        while (i < text.Length)
        {
            while (i < text.Length && text[i].Equals(' ') == true)
            {
                MainText.maxVisibleCharacters = i + 1;
                i++;
            }

            MainText.maxVisibleCharacters = i + 1;
            i++;
            yield return new WaitForSeconds(TimeLetterDelayS);
        }

        OnTextEndDiplay();
    }

    #endregion

    #region Handlers



    #endregion
}
