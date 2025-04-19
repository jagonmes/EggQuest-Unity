using UnityEngine;

public class Opciones : Opcion
{
    [SerializeField] private GameObject[] menuPrincipal;
    [SerializeField] private GameObject menuOpciones;
    [SerializeField] private bool activeInMobile = true;
        
    void Start()
    {
        if ((Application.isMobilePlatform && !activeInMobile))
        {
            texto.activo = false;
            texto.LoadText();
            return;
        }
    }
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
