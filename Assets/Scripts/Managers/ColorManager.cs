using System.Collections.Generic;
using UnityEngine;

//INDICAMOS QUE LA PALETA DE COLORES SE CARGUE INCLUSO EN EL EDITOR
[ExecuteInEditMode]
public class ColorManager : MonoBehaviour
{
    private static ColorManager _instance;
    public static ColorManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ColorManager>();
                if (_instance != null)
                {
                    if(Application.isPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }
                else
                {
                    Debug.LogError("No hay ColorManager en la escena");
                }
            }
            return _instance;
        }
        private set {_instance = value;}
    }
    
    [SerializeField] private Sprite[] ColorSprites = null;
    [SerializeField]public string[] ColorNames = null;
    public Dictionary<string, Sprite> ColorPallets = new Dictionary<string, Sprite>();
    
    [SerializeField]public string SelectedColorPreset = null;
    [SerializeField]public string[] SelectedColors = null;
    [SerializeField]public Dictionary<string, List<string>> ColorPresets = new Dictionary<string, List<string>>();
    public bool GridActive = false;
    public bool GhostingActive = false;
    public List<string> customPresetColors = new List<string>(){"DMG", "LIGHT", "POCKET", "GREEN", "BROWN", "PASTEL"};
    
    void Awake()
    {
        LoadColorPallets();
        CreateColorPresets();
    }

    private void LoadColorNames()
    {
        if (ColorNames == null || ColorNames.Length != ColorSprites.Length)
        {
            ColorNames = new string[ColorSprites.Length];
            for (int i = 0; i < ColorSprites.Length; i++)
            {
                ColorNames[i] = ColorSprites[i].name;
            }
        }
    }
    
    private void LoadColorPallets()
    {
        LoadColorNames();
        ColorPallets.Clear();
        for (int i = 0; i < ColorSprites.Length; i++)
        {
            if (!ColorPallets.ContainsKey(ColorNames[i]))
            {
                ColorPallets.Add(ColorNames[i], ColorSprites[i]);
            }
            else
            {
                Debug.LogWarning($"Color {ColorNames[i]} ya existe en el diccionario.");
            }
        }
    }

    private void CreateColorPresets()
    {
        foreach (var color in ColorNames)
        {
            string [] colors = {color, color, color, color, color, color};
            if (!ColorPresets.ContainsKey(color))
            {
                ColorPresets.Add(color, new List<string>(colors));
            }
            else
            {
                Debug.LogWarning($"Preset {color} ya existe en el diccionario.");
            }
        }
        customPresetColors = ConfigManager.Instance.LoadStringList("CustomPreset", 6);
        ColorPresets.Add("CUSTOM", new List<string>(customPresetColors));
    }
}
