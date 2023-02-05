using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "FilesContainerSetup.asset", menuName = "Settings/FilesContainerSetup")]
public class FilesContainerSetup : ScriptableObject
{
    #region Fields

    public const char FIELD_SEPARATOR = ';';
    public const char LINE_SEPARATOR = '\n';

    private static FilesContainerSetup instance;

    [Space]
    [SerializeField]
    private int headerLinesCount = 1;
    [SerializeField]
    private int localizedKeyIndex = 0;

    [Space]
    [SerializeField]
    private List<FileElement> filesCollection = new List<FileElement>();

    #endregion

    #region Propeties

    public static FilesContainerSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<FilesContainerSetup>("Settings/FilesContainerSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public int HeaderLinesCount { 
        get => headerLinesCount; 
        private set => headerLinesCount = value; 
    }

    public List<FileElement> FilesCollection
    {
        get => filesCollection;
        private set => filesCollection = value;
    }

    public int LocalizedKeyIndex { 
        get => localizedKeyIndex; 
        private set => localizedKeyIndex = value; 
    }

    #endregion

    #region Methods

    public List<string[]> GetTextFilesContentForLanguageAndType(Language language, FileType fileType)
    {
        List<string[]> output = new List<string[]>();
        if(FilesCollection == null)
        {
            return null;
        }

        List<TextAsset> files = GetFilesOfType(fileType);
        foreach (TextAsset file in files)
        {
            string contentData = file.text;
            string[] lines = contentData.Split(LINE_SEPARATOR);

            for (int i = HeaderLinesCount; i < lines.Length; i++)
            {
                string[] lineFields = lines[i].Split(FIELD_SEPARATOR);
                string[] keyAndText = new string[2];
                keyAndText[0] = GetSafeTextLineFieldAt(lineFields, LocalizedKeyIndex);
                keyAndText[1] = GetSafeTextLineFieldAt(lineFields, (int)language);

                output.Add(keyAndText);
            }
        }

        Debug.LogFormat("{0} wczytane w ilosci plikow {1}, zebrano {2} rekordow.", fileType, FilesCollection.Count, output.Count);
        return output;
    }

    public Dictionary<int, string> GetNotesForLanguage(Language language)
    {
        Dictionary<int, string> output = new Dictionary<int, string>();

        List<TextAsset> files = GetFilesOfType(FileType.NOTES);
        for(int i=0; i < files.Count; i++)
        {
            string contentData = files[i].text;
            string[] lines = contentData.Split(LINE_SEPARATOR);

            for (int x = HeaderLinesCount; x < lines.Length; x++)
            {
                string[] lineFields = lines[x].Split(FIELD_SEPARATOR);

                int intKey = GetSafeTextLineFieldAt(lineFields, LocalizedKeyIndex).ParseToInt();
                string content = GetSafeTextLineFieldAt(lineFields, (int)language);

                output.Add(intKey, content);
            }
        }

        return output;
    }

    public List<string> GetAllKeysByType(FileType fileType)
    {
        List<string> keys = new List<string>();

        if (FilesCollection == null)
        {
            return keys;
        }

        List<TextAsset> files = GetFilesOfType(fileType);
        foreach (TextAsset file in files)
        {
            string contentData = file.text;
            string[] lines = contentData.Split(LINE_SEPARATOR);

            for (int i = HeaderLinesCount; i < lines.Length; i++)
            {
                string[] lineFields = lines[i].Split(FIELD_SEPARATOR);
                keys.Add(GetSafeTextLineFieldAt(lineFields, LocalizedKeyIndex));
            }
        }

        return keys;
    }

    public List<TextAsset> GetFilesOfType(FileType fileType)
    {
        List<TextAsset> output = new List<TextAsset>();

        for (int i = 0; i < FilesCollection.Count; i++)
        {
            if (FilesCollection[i].FilesType == fileType)
            {
                output.Add(FilesCollection[i].File);
            }
        }

        return output;
    }

    private string GetSafeTextLineFieldAt(string[] fields ,int index)
    {
        if(fields.IsNullOrEmpty() == true || fields.Length == 1)
        {
            return string.Empty;
        }

        if(fields.Length -1 < index)
        {
            // Pozycja pierwszego elementu textu w przypadku singles.
            return fields[1];
        }

        return fields[index];

    }

    #endregion

    #region Handlers


    #endregion

    [Serializable]
    public class FileElement
    {
        #region Fields

        [SerializeField]
        private FileType filesType;
        [SerializeField]
        private TextAsset file;

        #endregion

        #region Propeties

        public FileType FilesType { 
            get => filesType; 
            private set => filesType = value; 
        }

        public TextAsset File { 
            get => file; 
            private set => file = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }

    public enum FileType
    {
        TEXTS,
        DESCRIPTIONS,
        SINGLES,
        NOTES
    }
}
