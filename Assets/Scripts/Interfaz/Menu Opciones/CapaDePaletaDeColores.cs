using System.Collections.Generic;
using UnityEngine;

public class CapaDePaletaDeColores : Opcion
{
    private List<string> colores = new List<string>();
    [SerializeField]private int layer = 0;
    private int selectedColorIndex = 0;
    private string selectedColor;
    [SerializeField]private PostProcessingCameraController PPcameraController;
    [SerializeField] private string textoAMostrar = "";
    
    void Start()
    {
        if(PPcameraController == null)
            PPcameraController = FindFirstObjectByType<PostProcessingCameraController>();
        if (PPcameraController == null)
            return;
        colores.AddRange(ColorManager.Instance.ColorPresets.Keys);
        colores.Remove("CUSTOM");
        selectedColor = ColorManager.Instance.customPresetColors[layer];
        selectedColorIndex= colores.IndexOf(selectedColor);
        string aux = "<" + selectedColor + ">";
        string aux2 = textoAMostrar + ":";
        aux2+= new string(' ', texto.caracteresPorLinea - (aux2.Length - 1));
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        PPcameraController.LoadColorPallete();
    }
    public override void Derecha()
    {

        selectedColorIndex++;
        if(selectedColorIndex >= colores.Count)
            selectedColorIndex = 0;
        selectedColor = colores[selectedColorIndex];
        string aux = "<" + selectedColor + ">";
        string aux2 = textoAMostrar + ":";
        aux2+= new string(' ', texto.caracteresPorLinea - (aux2.Length - 1));
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        ColorManager.Instance.customPresetColors[layer] = selectedColor;
        ColorManager.Instance.SelectedColors = ColorManager.Instance.customPresetColors.ToArray();
        ColorManager.Instance.ColorPresets["CUSTOM"] = ColorManager.Instance.customPresetColors;
        ConfigManager.Instance.SavePrefs("CustomPreset", ColorManager.Instance.customPresetColors);
        PPcameraController.LoadColorPallete();
        if(optionSound != null)
            optionSound.PlayEffect();
    }

    public override void Izquierda()
    {
        selectedColorIndex--;
        if(selectedColorIndex < 0)
            selectedColorIndex = colores.Count - 1;
        selectedColor = colores[selectedColorIndex];
        string aux = "<" + selectedColor + ">";
        string aux2 = textoAMostrar + ":";
        aux2+= new string(' ', texto.caracteresPorLinea - (aux2.Length - 1));
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        ColorManager.Instance.customPresetColors[layer] = selectedColor;
        ColorManager.Instance.SelectedColors = ColorManager.Instance.customPresetColors.ToArray();
        ColorManager.Instance.ColorPresets["CUSTOM"] = ColorManager.Instance.customPresetColors;
        ConfigManager.Instance.SavePrefs("CustomPreset", ColorManager.Instance.customPresetColors);
        PPcameraController.LoadColorPallete();
        if(optionSound != null)
            optionSound.PlayEffect();
    }

    public override void Accion()
    {
    }
}
