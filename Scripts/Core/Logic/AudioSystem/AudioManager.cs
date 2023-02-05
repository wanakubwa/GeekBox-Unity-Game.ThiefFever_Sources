using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class AudioManager : ManagerSingletonBase<AudioManager>
{
    #region Fields

    [ShowInInspector, NonSerialized]
    private AudioTrack currentSoundTrack = null;
    [ShowInInspector, NonSerialized]
    private AudioAmbientTrack currentAmbientTrack = null;

    [Space(10)]
    [SerializeField, ReadOnly]
    private float volume = 0;
    [SerializeField]
    private float maxVolume = 1f;
    [SerializeField]
    private bool isAudioMute = false;

    #endregion

    #region Propeties

    public event Action<float> OnVolumeChanged = delegate { };

    public AudioTrack CurrentSoundTrack
    {
        get => currentSoundTrack;
        private set => currentSoundTrack = value;
    }

    public AudioAmbientTrack CurrentAmbientTrack
    {
        get => currentAmbientTrack;
        private set => currentAmbientTrack = value;
    }

    public float Volume
    {
        get => volume;
        private set => volume = value;
    }

    public float MaxVolume { 
        get => maxVolume; 
        private set => maxVolume = value; 
    }

    public bool IsAudioMute { 
        get => isAudioMute; 
        private set => isAudioMute = value; 
    }

    // Variables.
    private Dictionary<AudioContainerSettings.AudioLabel, AudioTrack> CurrentSounds { get; set; } = new Dictionary<AudioContainerSettings.AudioLabel, AudioTrack>();
    private AudioContainerSettings AudioContainer { get; set; }


    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        AudioContainer = AudioContainerSettings.Instance;
    }

    public void SetAudioMute(bool isMuteAudio)
    {
        IsAudioMute = isMuteAudio;
        if(IsAudioMute == true)
        {
            SetVolume(0f);
        }
        else
        {
            SetVolume(MaxVolume);
        }
    }

    public void SetVolume(float value)
    {
        Volume = Mathf.Clamp(value, 0f, 1f);
        OnVolumeChanged(Volume);
    }

    public void ToggleIsAudioMute()
    {
        SetAudioMute(!IsAudioMute);
    }

    public void PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel label)
    {
        SoundElement audioElement = AudioContainer.GetAudioElementByLabel(label);
        if (audioElement != null)
        {
            if(audioElement.IsExclusive == false)
            {
                PlaySoundCommon(audioElement, label);
            }
            else
            {
                PlaySoundExclusive(audioElement, label);
            }
        }
    }

    public void PlayAmbientSoundBySceneId(int sceneId)
    {
        SoundElement audioElement = AudioContainer.GetAudioElementBySceneId(sceneId);
        if(audioElement != null)
        {
            PlayAmbientSoundTrack(audioElement, sceneId);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SetAudioMute(UserSettingsManager.Instance.IsAudioMuted);
        PlayAmbientSoundBySceneId(SceneManager.GetActiveScene().buildIndex);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void PlayAudioSoundTrack(SoundElement audio, AudioContainerSettings.AudioLabel label)
    {

    }

    private void PlayAmbientSoundTrack(SoundElement audio, int sceneId)
    {
        if (CurrentAmbientTrack != null)
        {
            if(CurrentAmbientTrack.IsTrackEqual(sceneId) == true)
            {
                return;
            }

            CurrentAmbientTrack.DestroyAudio();
        }

        SoundElement audioElement = Instantiate(audio);
        audioElement.transform.ResetParent(transform);
        CurrentAmbientTrack = new AudioAmbientTrack(audioElement, sceneId);
    }

    private void PlaySoundExclusive(SoundElement audioElement, AudioContainerSettings.AudioLabel label)
    {
        if(CurrentSounds.TryGetValue(label, out AudioTrack track) == true)
        {
            if (track.IsTrackEqual(label) == true)
            {
                track.ResetAudio();
                return;
            }

            track.DestroyAudio();
            CurrentSounds.Remove(label);
        }

        SoundElement newAudioElement = SpawnSoundElement(audioElement, label.ToString());
        AudioTrack audioTrack = new AudioTrack(newAudioElement, label);
        CurrentSounds.Add(label, audioTrack);
    }

    private void PlaySoundCommon(SoundElement audioElement, AudioContainerSettings.AudioLabel label)
    {
        if (CurrentSoundTrack != null)
        {
            if (CurrentSoundTrack.IsTrackEqual(label) == true)
            {
                CurrentSoundTrack.ResetAudio();
                return;
            }

            CurrentSoundTrack.DestroyAudio();
        }

        SoundElement newAudioElement = SpawnSoundElement(audioElement);
        CurrentSoundTrack = new AudioTrack(newAudioElement, label);
    }

    private SoundElement SpawnSoundElement(SoundElement audioElement, string prefix = null)
    {
        SoundElement newAudioElement = Instantiate(audioElement);
        newAudioElement.transform.ResetParent(transform);
        newAudioElement.name = (prefix + audioElement.name);

        return newAudioElement;
    }

    #endregion

    #region Handlers

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        PlayAmbientSoundBySceneId(scene.buildIndex);
    }

    #endregion

    [Serializable]
    public class AudioTrack
    {
        #region Fields

        [SerializeField]
        private AudioContainerSettings.AudioLabel label;
        [SerializeField]
        private SoundElement audioElement;

        #endregion

        #region Propeties

        public AudioContainerSettings.AudioLabel Label
        {
            get => label;
            private set => label = value;
        }

        public SoundElement AudioElement
        {
            get => audioElement;
            private set => audioElement = value;
        }

        #endregion

        #region Methods

        public AudioTrack(SoundElement audio, AudioContainerSettings.AudioLabel label)
        {
            AudioElement = audio;
            Label = label;
        }

        public AudioTrack(SoundElement audio)
        {
            AudioElement = audio;
        }

        public bool IsTrackEqual(AudioContainerSettings.AudioLabel Label)
        {
            if (Label == label)
            {
                return true;
            }

            return false;
        }

        public void ResetAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void StopAudio()
        {
            AudioElement.StopAudio();
        }

        public void PlayAudio()
        {
            AudioElement.PlayOneShotAudio();
        }

        public void DestroyAudio()
        {
            AudioElement.DestroyAudio();
        }

        #endregion

        #region Handlers



        #endregion
    }

    [Serializable]
    public class AudioAmbientTrack
    {
        #region Fields

        [SerializeField]
        private int sceneId;
        [SerializeField]
        private SoundElement audioElement;

        #endregion

        #region Propeties

        public SoundElement AudioElement
        {
            get => audioElement;
            private set => audioElement = value;
        }

        public int SceneId
        {
            get => sceneId;
            private set => sceneId = value;
        }

        #endregion

        #region Methods

        public AudioAmbientTrack(SoundElement audio, int sceneId)
        {
            AudioElement = audio;
            SceneId = sceneId;
        }

        public bool IsTrackEqual(int sceneId)
        {
            if (SceneId == sceneId)
            {
                return true;
            }

            return false;
        }

        public void ResetAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void StopAudio()
        {
            AudioElement.StopAudio();
        }

        public void PlayAudio()
        {
            AudioElement.PlayOneShotAudio();
        }

        public void DestroyAudio()
        {
            AudioElement.DestroyAudio();
        }

        #endregion

        #region Handlers

        #endregion
    }
}
