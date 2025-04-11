using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpciones : GrupoDeOpciones
{
    [SerializeField] private GameObject[] menuPrincipal;

    public override void Volver(InputAction.CallbackContext context)
    {
        base.Volver(context);
        if (context.performed)
        {
            foreach (var objeto in menuPrincipal)
            {
                objeto.SetActive(true);
            }
            this.gameObject.SetActive(false);
            this.RecolocarFlecha();
        }
    }

}
