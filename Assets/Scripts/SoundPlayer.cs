using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool isMusic = false;
    [SerializeField] private AudioClip source;
    [SerializeField] private AudioSource audioSource;
    
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

    public void PlayEffect()
    {
        if(!isMusic && audioSource != null && source != null)
        {
            audioSource.clip = source;
            audioSource.mute = SoundManager.Instance.Muted;
            audioSource.volume = SoundManager.Instance.EffectsVolume;
            audioSource.Play();
        }
    }

    public void SoundSettingsChanged()
    {
        audioSource.volume = SoundManager.Instance.EffectsVolume;
        audioSource.mute = SoundManager.Instance.Muted;
    }
    
    public void OnDestroy()
    {
        if (!isMusic && SoundManager.Instance != null)
        {
            SoundManager.Instance.OnSoundSettingsChanged -= SoundSettingsChanged;
        }
    }
}
