using UnityEngine;
using UnityEngine.SceneManagement;

public class IrAMenuPrincipal : Opcion
{
    [SerializeField]private string nombreEscena;
    public override void Accion()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
