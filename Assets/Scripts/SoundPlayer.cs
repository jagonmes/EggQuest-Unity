using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool isMusic = false;
    [SerializeField] public AudioClip source;
    [SerializeField] private AudioSource audioSource;
    public bool playing = false;
    public bool stopMusic = false;
    
    private void Start()
    {
        if (isMusic)
        {
            if(source != null)
                SoundManager.Instance.PlayMusic(source);
            if(stopMusic)
                SoundManager.Instance.StopMusic();
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

    public virtual void PlayEffect()
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
    
    public virtual void StopEffect()
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
