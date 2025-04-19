using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class DPAD : MonoBehaviour
{
    [SerializeField] private Sprite pulsado;
    [SerializeField] private Sprite sinPulsar;
    [SerializeField] private Image iArriba;
    [SerializeField] private Image iAbajo;
    [SerializeField] private Image iIzquierda;
    [SerializeField] private Image iDerecha;
    [SerializeField] private PlayerInput playerInput;


    private void Start()
    {
        playerInput.enabled = true;
    }

    public virtual void Arriba(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            iArriba.sprite = pulsado;
        }
        else
        {
            iArriba.sprite = sinPulsar;
        }
    }
    public virtual void Abajo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            iAbajo.sprite = pulsado;
        }
        else
        {
            iAbajo.sprite = sinPulsar;
        }
    }
    public virtual void Izquierda(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            iIzquierda.sprite = pulsado;
        }
        else
        {
            iIzquierda.sprite = sinPulsar;
        }
    }
    public virtual void Derecha(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            iDerecha.sprite = pulsado;
        }
        else
        {
            iDerecha.sprite = sinPulsar;
        }
    }
}
