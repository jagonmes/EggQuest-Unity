using System;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    private static BorderManager _instance;
    public static BorderManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<BorderManager>();
                if (_instance != null)
                {
                    if(Application.isPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }
                else
                {
                    Debug.Log("No hay BorderManager en la escena.");
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    
    public event Action OnBorderChanged;

    private string _selectedBorder = "DMG";
    public string SelectedBorder
    {
        get => _selectedBorder;
        set
        {
            if (_selectedBorder != value)
            {
                _selectedBorder = value;
                OnBorderChanged?.Invoke();
            }
        }
    }
    
    [SerializeField]public List<string> BorderNames = new List<string>();
    [SerializeField]public List<Sprite> Sprites = new List<Sprite>();
    [SerializeField]public List<Color> BGColors = new List<Color>();
    [SerializeField]public List<Color> FGColors = new List<Color>();
    
    public void Awake()
    {
        if(BorderManager.Instance != null && BorderManager.Instance != this)
            Destroy(this.gameObject);
    }
}
