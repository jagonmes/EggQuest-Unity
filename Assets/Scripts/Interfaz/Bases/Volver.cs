using UnityEngine;

public class Volver : Opcion
{
    [SerializeField] private GameObject[] menuActual;
    [SerializeField] private GameObject[] menuAnterior;
    [SerializeField] private GrupoDeOpciones grupoDeOpciones;
    public override void Accion()
    {
        base.Accion();
        grupoDeOpciones.ReiniciarOpcionSeleccionada();
        foreach (var obj in menuActual)
        {
            obj.SetActive(false);
        }
        foreach (var obj in menuAnterior)
        {
            obj.SetActive(true);
        }
    }
}
