using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : GrupoDeOpciones
{
    [SerializeField] private GameObject menuPausa;
    public override void Accion(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (opciones.Count != 0)
                if (opciones[opcionSeleccionada] != null)
                    opciones[opcionSeleccionada].Accion();
        }
    }
}
