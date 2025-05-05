using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool isMusic = false;
    [SerializeField] public AudioClip source;
    [SerializeField] private AudioSource audioSource;
    public bool playing = false;
    
    private void Start()
    {
        if (isMusic)
        {
            SoundManager.Instance.PlayMusic(source);
        }
        else
        {
            SoundManager.Instance.OnSoundSettingsChanged += SoundSettingsChanged;
        }
    }

    private void Update()
    {
        if (!isMusic)
        {
            if(audioSource.isPlaying && audioSource.clip == source)
                playing = true;
            else
                playing = false;
        }
    }

    public void PlayEffect()
    {
        if(!isMusic && audioSource != null && source != null)
        {
            audioSource.clip = source;
            audioSource.mute = SoundManager.Instance.Muted;
            audioSource.volume = SoundManager.Instance.EffectsVolume;
            audioSource.Play();
            playing = true;
        }
    }
    
    public void StopEffect()
    {
        if(!isMusic && audioSource != null)
        {
            audioSource.Stop();
            playing = false;
        }
    }

    public void SoundSettingsChanged()
    {
        audioSource.volume = SoundManager.Instance.EffectsVolume;
        audioSource.mute = SoundManager.Instance.Muted;
    }
    
    public void SetLoop(bool loop)
    {
        if (!isMusic && audioSource != null)
        {
            audioSource.loop = loop;
        }
    }
    
    public void OnDestroy()
    {
        if (!isMusic && SoundManager.Instance != null)
        {
            SoundManager.Instance.OnSoundSettingsChanged -= SoundSettingsChanged;
        }
    }
}
