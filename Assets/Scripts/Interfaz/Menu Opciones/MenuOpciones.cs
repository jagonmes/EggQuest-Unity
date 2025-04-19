using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MenuOpciones : GrupoDeOpciones
{
    [SerializeField] private GameObject[] menuAnterior;

    public override void Volver(InputAction.CallbackContext context)
    {
        base.Volver(context);
        if (context.performed)
        {
            foreach (var objeto in menuAnterior)
            {
                objeto.SetActive(true);
            }
            this.gameObject.SetActive(false);
            this.ReiniciarOpcionSeleccionada();
        }
    }

}
