using System;
using UnityEngine;

public class Volumen : Opcion
{
[SerializeField] private bool isMusic = false;
public int volumen = 10;

private void Start()
{
    if(isMusic)
        volumen  = (int)(SoundManager.Instance.MusicVolume * 100 / 10);
    else
        volumen = (int)(SoundManager.Instance.EffectsVolume * 100 / 10);
    PintarVolumen();
}

public override void Derecha()
{
    volumen++;
    if(volumen > 10)
        volumen = 0;
    PintarVolumen();
    if(optionSound != null)
        optionSound.PlayEffect();
}
public override void Izquierda()
{
    volumen--;
    if(volumen < 0)
        volumen = 10;
    PintarVolumen();
    if(optionSound != null)
        optionSound.PlayEffect();
}

public void PintarVolumen()
{
    string aux = "<" + volumen + ">";
    string aux2 = "";
    if (isMusic)
    {
        aux2 = "Volumen música:";
        aux2+= new string(' ', texto.caracteresPorLinea * 2 - (aux2.Length - 2));
        SoundManager.Instance.MusicVolume = volumen / 10f;
        ConfigManager.Instance.SavePrefs("Music", SoundManager.Instance.MusicVolume);
    }
    else
    {
        aux2 = "Volumen efectos:";
        aux2+= new string(' ', texto.caracteresPorLinea * 2 - (aux2.Length - 2));
        SoundManager.Instance.EffectsVolume = volumen / 10f;
        ConfigManager.Instance.SavePrefs("Effects", SoundManager.Instance.EffectsVolume);
    }
    texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
}

public override void Accion()
{
    
}
}
