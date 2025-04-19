using UnityEngine;

public class ContinuarMenuPausa : Opcion
{
    [SerializeField] private GameObject[] menuActual;
    [SerializeField] private GrupoDeOpciones grupoDeOpciones;
    public override void Accion()
    {
        base.Accion();
        grupoDeOpciones.ReiniciarOpcionSeleccionada();
        foreach (var obj in menuActual)
        {
            obj.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}
