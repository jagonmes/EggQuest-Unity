using System;
using UnityEngine;

public class PantallaCompleta : Opcion
{
    [SerializeField]private Texto caja;
    [SerializeField]private Resolucion controladorDeResolucion;

    private void Start()
    {
        if (Application.isMobilePlatform || Application.isEditor)
        {
            texto.activo = false;
            caja.activo = false;
            texto.LoadText();
            caja.LoadText();
        }
    }
    
    public override void Accion()
    {
        base.Accion();
        CambiarPantallaCompleta();
    }
    
    private void CambiarPantallaCompleta()
    {

        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Pantalla completa: " + Screen.fullScreen);
    }

    private void Update()
    {
        caja.texto = "               [" + (Screen.fullScreen?"x":" ") + "]";
    }
}
