using UnityEngine;

public class Opciones : Opcion
{
    [SerializeField] private GameObject[] menuPrincipal;
    [SerializeField] private GameObject menuOpciones;
    
    public override void Accion()
    {
        base.Accion();
        CambiarMenu();
    }
    private void CambiarMenu()
    {
        foreach (var objeto in menuPrincipal)
        {
            objeto.SetActive(false);
        }
        menuOpciones.SetActive(true);
    }
}
