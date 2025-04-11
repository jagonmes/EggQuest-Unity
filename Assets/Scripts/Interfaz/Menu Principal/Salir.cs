using UnityEngine;

public class Salir : Opcion
{
    override public void Accion()
    {
        Application.Quit();
        Debug.Log("Estas intentando salir del juego en el editor.");
    }
}
