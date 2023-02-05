using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AudioContainerSettings.asset", menuName = "Settings/AudioContainerSettings")]
public class AudioContainerSettings : ScriptableObject
{
    #region Fields

    private static AudioContainerSettings instance;

    [SerializeField]
    private List<SingleAudio> audioCollection = new List<SingleAudio>();

    [Space(10)]
    [SerializeField]
    private List<SingleSceneAudio> sceneAudioCollection = new List<SingleSceneAudio>();

    #endregion

    #region Propeties

    public static AudioContainerSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AudioContainerSettings>("Settings/AudioContainerSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<SingleAudio> AudioCollection
    {
        get => audioCollection;
        private set => audioCollection = value;
    }

    public List<SingleSceneAudio> SceneAudioCollection
    {
        get => sceneAudioCollection;
        private set => sceneAudioCollection = value;
    }

    #endregion

    #region Methods

    public SoundElement GetAudioElementByLabel(AudioLabel label)
    {
        if (AudioCollection == null)
        {
            Debug.Log("Brak elementow dzwiekow podpietych!");
            return null;
        }

        for (int i = 0; i < AudioCollection.Count; i++)
        {
            if (AudioCollection[i].Label == label)
            {
                return AudioCollection[i].Audio;
            }
        }

        return null;
    }

    public SoundElement GetAudioElementBySceneId(int sceneId)
    {
        if (SceneAudioCollection == null)
        {
            Debug.Log("Brak elementow dzwiekow podpietych do kolekcji scen".SetColor(Color.red));
            return null;
        }

        for (int i = 0; i < SceneAudioCollection.Count; i++)
        {
            if (SceneAudioCollection[i].SceneId == sceneId)
            {
                return SceneAudioCollection[i].Audio;
            }
        }

        return null;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (SceneAudioCollection != null)
        {
            RefreshSceneAudioCollection();
        }
    }

    private void RefreshSceneAudioCollection()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        for (int i = 0; i < scenes.Length; i++)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scenes[i].path);
            SingleSceneAudio sceneAudio = SceneAudioCollection.Find(x => x.SceneId == i);
            if (sceneAudio != null)
            {
                sceneAudio.SetSceneName(sceneName);
            }
        }
    }

#endif

    #endregion

    #region Enums

    public enum AudioLabel
    {
        UI_BUTTON_CLICK,
        WIN,
        COLLECT,
        BARRIER,
        WALL_BREAK,

        GOLD_PICKUP,
        PAINTING_PICKUP,
        DESTACK,
    }

    [Serializable]
    public class SingleAudio
    {
        #region Fields

        [SerializeField]
        private AudioLabel label;
        [SerializeField]
        private SoundElement audio;

        #endregion

        #region Propeties

        public AudioLabel Label
        {
            get => label;
            private set => label = value;
        }

        public SoundElement Audio
        {
            get => audio;
            private set => audio = value;
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }

    [Serializable]
    public class SingleSceneAudio
    {
        #region Fields
        [SerializeField, ReadOnly]
        private string sceneName;
        [SerializeField]
        private int sceneId;
        [SerializeField]
        private SoundElement audio;

        #endregion

        #region Propeties

        public int SceneId
        {
            get => sceneId;
            private set => sceneId = value;
        }

        public SoundElement Audio
        {
            get => audio;
            private set => audio = value;
        }

        public string SceneName
        {
            get => sceneName;
            private set => sceneName = value;
        }

        #endregion

        #region Methods

        public void SetSceneName(string name)
        {
            SceneName = name;
        }

        #endregion

        #region Handlers



        #endregion
    }

    #endregion
}
