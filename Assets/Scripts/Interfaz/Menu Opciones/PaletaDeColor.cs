using System.Collections.Generic;
using UnityEngine;

public class PaletaDeColor : Opcion
{
    private List<string> colores = new List<string>();
    private string selectedColorPreset;
    private int selectedColorPresetIndex;
    [SerializeField]private PostProcessingCameraController PPcameraController;
    private Dictionary<string, List<string>> Presets = new Dictionary<string, List<string>>();
    
    void Start()
    {
        if(PPcameraController == null)
            PPcameraController = FindFirstObjectByType<PostProcessingCameraController>();
        if (PPcameraController == null)
            return;
        colores.AddRange(ColorManager.Instance.ColorPresets.Keys);
        selectedColorPreset = ColorManager.Instance.SelectedColorPreset;
        selectedColorPresetIndex = colores.IndexOf(selectedColorPreset);
        string aux = "<" + selectedColorPreset + ">";
        string aux2 = "Paleta de color:                    ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        ColorManager.Instance.SelectedColors = ColorManager.Instance.ColorPresets[selectedColorPreset].ToArray();
        ConfigManager.Instance.SavePrefs("ColorPreset", ColorManager.Instance.SelectedColorPreset);
        PPcameraController.LoadColorPallete();
    }
    public override void Derecha()
    {
        selectedColorPresetIndex++;
        if(selectedColorPresetIndex >= colores.Count)
            selectedColorPresetIndex = 0;
        selectedColorPreset = colores[selectedColorPresetIndex];
        string aux = "<" + selectedColorPreset + ">";
        string aux2 = "Paleta de color:                    ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        ColorManager.Instance.SelectedColorPreset = selectedColorPreset;
        ColorManager.Instance.SelectedColors = ColorManager.Instance.ColorPresets[selectedColorPreset].ToArray();
        ConfigManager.Instance.SavePrefs("ColorPreset", ColorManager.Instance.SelectedColorPreset);
        PPcameraController.LoadColorPallete();
    }

    public override void Izquierda()
    {
        selectedColorPresetIndex--;
        if(selectedColorPresetIndex < 0)
            selectedColorPresetIndex = colores.Count - 1;
        selectedColorPreset = colores[selectedColorPresetIndex];
        string aux = "<" + selectedColorPreset + ">";
        string aux2 = "Paleta de color:                    ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        ColorManager.Instance.SelectedColorPreset = selectedColorPreset;
        ColorManager.Instance.SelectedColors = ColorManager.Instance.ColorPresets[selectedColorPreset].ToArray();
        ConfigManager.Instance.SavePrefs("ColorPreset", ColorManager.Instance.SelectedColorPreset);
        PPcameraController.LoadColorPallete();
    }
    
    
}
