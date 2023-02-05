using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : ManagerSingletonBase<LanguageManager>
{
    #region Fields

    private string LANGUAGE_KEY = "LANGUAGE_SAVE";

    [SerializeField]
    private Language language = Language.PL;

    #endregion

    #region Propeties

    public event Action OnLanguageChange = delegate { };

    public List<string[]> TextCollection
    {
        get;
        private set;
    } = new List<string[]>();

    public List<string[]> DescriptionCollection
    {
        get;
        private set;
    } = new List<string[]>();

    public Dictionary<int, string> NotesCollection
    {
        get;
        private set;
    } = new Dictionary<int, string>();

    public int LocalizedKeyIndex
    {
        get;
        private set;
    }

    public Language CurrentLanguage
    {
        get => language;
        private set => language = value;
    }

    #endregion

    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        Language language;
        if(TryGetSavedLanguage(out language) == true)
        {
            CurrentLanguage = language;
        }

        RefreshTextCollection();
        LocalizedKeyIndex = FilesContainerSetup.Instance.LocalizedKeyIndex;
    }

    public void SetLanguage(Language language)
    {
        CurrentLanguage = language;
        RefreshTextCollection();
        SaveLanguage();
        OnLanguageChange();
    }

    // TODO: binary search.
    public string GetTextByKey(string key)
    {
        if (key.IsNullOrWhitespace() == true)
        {
            return string.Empty;
        }

        key.Trim();

        for (int i = 0; i < TextCollection.Count; i++)
        {
            if (TextCollection[i][LocalizedKeyIndex] == key)
            {
                string text = TextCollection[i][LocalizedKeyIndex + 1].Trim();
                text = text.FixNewlineUnicode();
                text = text.FixCommaUnicode();

                return text;
            }
        }

        Debug.LogFormat("Brak zdefionowanej wartosci textu dla klucza: [{0}]!".SetColor(Color.red), key);
        return key;
    }

    public string GetDescriptionByKey(string key)
    {
        if (key.IsNullOrWhitespace() == true)
        {
            return string.Empty;
        }

        key.Trim();

        for (int i = 0; i < DescriptionCollection.Count; i++)
        {
            if (DescriptionCollection[i][LocalizedKeyIndex] == key)
            {
                string description = DescriptionCollection[i][LocalizedKeyIndex + 1].Trim();
                description = description.FixCommaUnicode();
                description = description.FixNewlineUnicode();

                return description;
            }
        }

        Debug.LogFormat("Brak zdefionowanej wartosci opisu dla klucza: [{0}]!".SetColor(Color.red), key);
        return key;
    }

    public string GetNoteById(int id)
    {
        if (id == Constants.DEFAULT_ID)
        {
            return string.Empty;
        }

        foreach (var note in NotesCollection)
        {
            if (note.Key == id)
            {
                string text = note.Value.Trim();
                text = text.FixNewlineUnicode();
                text = text.FixCommaUnicode();

                return text;
            }
        }

        Debug.LogFormat("Brak zdefionowanej wartosci textu dla klucza o id: [{0}]!".SetColor(Color.red), id);
        return string.Empty;
    }

    public string GetRandomKeyWithPrefixWithoutSuffix(string prefix, string suffix)
    {
        string output = string.Empty;

        List<string> keysWithTag = GetAllKeyWithTag(prefix);
        if (keysWithTag != null)
        {
            List<string> keysWithoutSuffix = new List<string>();
            for(int i = 0; i < keysWithTag.Count; i++)
            {
                if(keysWithTag[i].Contains(suffix) == false)
                {
                    keysWithoutSuffix.Add(keysWithTag[i]);
                }
            }
            output = keysWithoutSuffix.GetRandomElement();
        }
        else
        {
            Debug.LogFormat("Brak kluczy zawierajacych tag: [{0}]!".SetColor(Color.red), tag);
        }

        return output;
    }

    public string GetRandomKeyWithTag(string tag)
    {
        string output = string.Empty;

        List<string> keysWithTag = GetAllKeyWithTag(tag);
        if(keysWithTag != null)
        {
            output = keysWithTag.GetRandomElement();
        }
        else
        {
            Debug.LogFormat("Brak kluczy zawierajacych tag: [{0}]!".SetColor(Color.red), tag);
        }

        return output;
    }

    public List<string> GetAllKeyWithTag(string tag)
    {
        if(TextCollection == null)
        {
            return null;
        }

        List<string> output = new List<string>();
        for(int i = 0; i < TextCollection.Count; i++)
        {
            if(TextCollection[i][LocalizedKeyIndex].Contains(tag) == true)
            {
                output.Add(TextCollection[i][LocalizedKeyIndex]);
            }
        }

        return output;
    }

    private void RefreshTextCollection()
    {
        if (FilesContainerSetup.Instance == null)
        {
            Debug.LogErrorFormat("Blad odswiezenia kolekcji plikow dla jezyka {0}", CurrentLanguage);
            return;
        }

        // Brzydkie mozna zmienic na cos lepszego.
        TextCollection.Clear();
        DescriptionCollection.Clear();
        NotesCollection.Clear();
        List<string[]> loadedCollection;

        loadedCollection = FilesContainerSetup.Instance.GetTextFilesContentForLanguageAndType(CurrentLanguage, FilesContainerSetup.FileType.TEXTS);
        if (loadedCollection != null)
        {
            TextCollection.AddRange(loadedCollection);
        }

        loadedCollection = FilesContainerSetup.Instance.GetTextFilesContentForLanguageAndType(CurrentLanguage, FilesContainerSetup.FileType.SINGLES);
        if (loadedCollection != null)
        {
            TextCollection.AddRange(loadedCollection);
        }

        loadedCollection = FilesContainerSetup.Instance.GetTextFilesContentForLanguageAndType(CurrentLanguage, FilesContainerSetup.FileType.DESCRIPTIONS);
        if (loadedCollection != null)
        {
            DescriptionCollection = loadedCollection;
        }

        Dictionary<int, string> loadedNotes = FilesContainerSetup.Instance.GetNotesForLanguage(CurrentLanguage);
        if(loadedNotes != null)
        {
            NotesCollection = loadedNotes;
        }
    }

    // Ponizsze metody sa brzydkim hackiem do zapisu jezyka :(
    private void SaveLanguage()
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, CurrentLanguage.ToString());
        PlayerPrefs.Save();
    }

    private bool TryGetSavedLanguage(out Language language)
    {
        language = Language.EN;
        if(PlayerPrefs.HasKey(LANGUAGE_KEY) == true)
        {
            return Enum.TryParse<Language>(PlayerPrefs.GetString(LANGUAGE_KEY), out language);
        }

        return false;
    }

    #endregion

    #region Handlers



    #endregion
}
