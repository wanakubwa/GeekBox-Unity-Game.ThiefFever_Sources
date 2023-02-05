using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class SoundElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private bool playOnAwake;
    [SerializeField]
    private bool isFade;
    [SerializeField]
    private bool isExclusive = false;

    #endregion

    #region Propeties

    public AudioSource Sound
    {
        get => sound;
        private set => sound = value;
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
        private set => playOnAwake = value;
    }

    public bool IsFade { 
        get => isFade; 
    }
    public bool IsExclusive {
        get => isExclusive;
    }

    #endregion

    #region Methods

    public void SetVolume(float volume)
    {
        Sound.volume = volume;
    }

    public void PlayOneShotAudio()
    {
        float currentVolume = AudioManager.Instance.Volume;

        if (IsFade == true)
        {
            Sound.volume = 0f;
            Sound.PlayOneShot(Sound.clip);
            Sound.DOFade(currentVolume, 1f);
        }
        else
        {
            Sound.volume = currentVolume;
            Sound.PlayOneShot(Sound.clip);
        }
    }

    public void StopAudio()
    {
        if(IsFade == true)
        {
            Sound.DOFade(0f, 0.30f);
        }
        else
        {
            Sound.Stop();
        }
    }

    public void DestroyAudio()
    {
        if (IsFade == true)
        {
            Sound.DOFade(0f, 1f).OnComplete(() => Destroy(gameObject));
        }
        else
        {
            StopAudio();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        if (PlayOnAwake == true)
        {
            PlayOneShotAudio();
        }
    }

    private void Reset()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.OnVolumeChanged += SetVolume;
        }
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnVolumeChanged -= SetVolume;
        }
    }

    #endregion

    #region Enums



    #endregion
}
