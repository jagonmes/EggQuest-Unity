using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : GrupoDeOpciones
{
    [SerializeField] private GameObject menuPausa;

    public override void StartButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            base.StartButton(context);
            if (menuPausa.activeSelf)
            {
                Time.timeScale = 1;
                menuPausa.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                menuPausa.SetActive(true);
            }
        }
    }

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
