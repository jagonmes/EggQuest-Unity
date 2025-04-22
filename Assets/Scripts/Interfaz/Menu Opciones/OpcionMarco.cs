using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionMarco : Opcion
{
    private List<string> nombreMarcos = new List<string>();
    private int selectedBorderIndex = 0;
    private string selectedBorder = "";
    [SerializeField] private string textoAMostrar = "Marco";

    void Start()
    {
        nombreMarcos.AddRange(BorderManager.Instance.BorderNames);
        selectedBorder = BorderManager.Instance.SelectedBorder;
        selectedBorderIndex = nombreMarcos.IndexOf(BorderManager.Instance.SelectedBorder);
        CambiarTexto();
    }
    
    public override void Derecha()
    {

        selectedBorderIndex++;
        if(selectedBorderIndex >= nombreMarcos.Count)
            selectedBorderIndex = 0;
        selectedBorder = nombreMarcos[selectedBorderIndex];
        BorderManager.Instance.SelectedBorder = selectedBorder;
        CambiarTexto();
        ConfigManager.Instance.SavePrefs("Border", BorderManager.Instance.SelectedBorder);
        if(optionSound != null)
            optionSound.PlayEffect();
    }
    
    public override void Izquierda()
    {

        selectedBorderIndex--;
        if(selectedBorderIndex < 0)
            selectedBorderIndex = nombreMarcos.Count - 1;
        selectedBorder = nombreMarcos[selectedBorderIndex];
        BorderManager.Instance.SelectedBorder = selectedBorder;
        CambiarTexto();
        ConfigManager.Instance.SavePrefs("Border", BorderManager.Instance.SelectedBorder);
        if(optionSound != null)
            optionSound.PlayEffect();
    }


    public void CambiarTexto()
    {
        string aux = "<" + BorderManager.Instance.SelectedBorder + ">"; 
        string aux2 = textoAMostrar + ":";
        aux2+= new string(' ', texto.caracteresPorLinea - (aux2.Length - 1));
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        texto.LoadText();
    }


    public override void Accion()
    {
    }

}
