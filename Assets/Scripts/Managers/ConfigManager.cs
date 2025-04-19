using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private static ConfigManager _instance;
    public static ConfigManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ConfigManager>();
                if (_instance != null)
                {
                    if(Application.isPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }
                else
                {
                    Debug.LogError("No hay ConfigManager en la escena.");
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    public void Awake()
    {
        if(ConfigManager.Instance != null && ConfigManager.Instance != this)
            Destroy(this.gameObject);
        LoadPrefs();
    }
    
    public void LoadPrefs()
    {
        ColorManager.Instance.SelectedColorPreset = PlayerPrefs.GetString("ColorPreset", "DMG");
        ColorManager.Instance.GridActive = PlayerPrefs.GetInt("Grid", 0) == 1;
        ColorManager.Instance.GhostingActive = PlayerPrefs.GetInt("Ghosting", 0) == 1;
        ColorManager.Instance.customPresetColors = LoadStringList("CustomPreset", 6);
        SoundManager.Instance.MusicVolume = PlayerPrefs.GetFloat("Music", 1);
        SoundManager.Instance.EffectsVolume = PlayerPrefs.GetFloat("Effects", 1);
        SoundManager.Instance.Muted = PlayerPrefs.GetInt("Muted", 0) == 1;
    }

    public void SavePrefs(string pref, int value)
    {
        PlayerPrefs.SetInt(pref, value);
        PlayerPrefs.Save();
    }
    
    public void SavePrefs(string pref, float value)
    {
        PlayerPrefs.SetFloat(pref, value);
        PlayerPrefs.Save();
    }
    
    public void SavePrefs(string pref, string value)
    {
        PlayerPrefs.SetString(pref, value);
        PlayerPrefs.Save();
    }
    
    public void SavePrefs(string pref, List<string> value)
    {
        for (int i = 0; i < value.Count; i++)
        {
            PlayerPrefs.SetString(pref+i.ToString(), value[i]);
            PlayerPrefs.Save();
        }
    }
    
    public void SavePrefs(string pref, List<int> value)
    {
        for (int i = 0; i < value.Count; i++)
        {
            PlayerPrefs.SetInt(pref+i.ToString(), value[i]);
            PlayerPrefs.Save();
        }
    }
    
    public void SavePrefs(string pref, List<float> value)
    {
        for (int i = 0; i < value.Count; i++)
        {
            PlayerPrefs.SetFloat(pref+i.ToString(), value[i]);
            PlayerPrefs.Save();
        }
    }
    
    public List<string> LoadStringList(string pref, int number, string defaultValue = "DMG")
    {
        List<string> values = new List<string>();
        for (int i = 0; i < number; i++)
        {
            values.Add(PlayerPrefs.GetString(pref+i.ToString(), defaultValue));
        }
        return values;
    }
    
    public List<int> LoadIntegerList(string pref, int number, int defaultValue = 0)
    {
        List<int> values = new List<int>();
        for (int i = 0; i < number; i++)
        {
            values.Add(PlayerPrefs.GetInt(pref+i.ToString(), defaultValue));
        }
        return values;
    }
    
    public List<float> LoadFloatList(string pref, int number, float defaultValue = 0)
    {
        List<float> values = new List<float>();
        for (int i = 0; i < number; i++)
        {
            values.Add(PlayerPrefs.GetFloat(pref+i.ToString(), defaultValue));
        }
        return values;
    }
}
