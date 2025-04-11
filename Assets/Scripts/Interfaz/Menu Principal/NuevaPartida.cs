using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartida : Opcion
{
    public override void Accion()
    {
        base.Accion();
        SceneManager.LoadScene("SampleScene");
    }
}
