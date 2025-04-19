using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<SoundManager>();
                if (_instance != null)
                {
                    if(Application.isPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }
                else
                {
                    //Debug.LogError("No hay SoundManager en la escena");
                }
            }
            return _instance;
        }
        private set {_instance = value;}
    }
    
    private float _musicVolume = 1;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            if (Math.Abs(_musicVolume - value) > 0.01f)
            {
                _musicVolume = value;
                OnSoundSettingsChanged?.Invoke();
            }
        }
    }

    private float _effectsVolume = 1;
    public float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            if (Math.Abs(_effectsVolume - value) > 0.01f)
            {
                _effectsVolume = value;
                OnSoundSettingsChanged?.Invoke();
            }
        }
    }

    private bool _muted = false;
    public bool Muted
    {
        get => _muted;
        set
        {
            if (_muted != value)
            {
                _muted = value;
                OnSoundSettingsChanged?.Invoke();
            }
        }
    }
    [SerializeField] private AudioSource music;
    public event Action OnSoundSettingsChanged;

    public void Start()
    {
        OnSoundSettingsChanged += SoundSettingsChanged;
    }

    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.mute = Muted;
        music.volume = MusicVolume;
        music.Play();
    }
    
    public void StopMusic()
    {
        music.Stop();
    }
    
    public void SoundSettingsChanged()
    {
        music.volume = MusicVolume;
        music.mute = Muted;
    }
}
