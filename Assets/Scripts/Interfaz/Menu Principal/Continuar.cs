using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continuar : Opcion
{
    public void Start()
    {
        if(PlayerPrefs.GetString("LastLevel", "") == "")
        {
            texto.activo = false;
        }else
        {
            texto.activo = true;
        }
    }

    public override void Accion()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastLevel", "Nivel 1"));
    }
}
