using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class EntradaDelJugador : MonoBehaviour
{
    [SerializeField]private PlayerInput playerInput;
    public float yAxis = 0;
    public float xAxis = 0;

    void Start()
    {
        StartCoroutine(ActivarPlayerInput());
    }
    
    public virtual void Arriba(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            yAxis += 1;
        }else if (context.canceled)
        {
            yAxis -= 1;
        }
    }
    
    public virtual void Abajo(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            yAxis -= 1;
        }else if (context.canceled)
        {
            yAxis += 1;
        }
    }
    
    public virtual void Derecha(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            xAxis += 1;
        }else if (context.canceled)
        {
            xAxis -= 1;
        }
    }
    
    public virtual void Izquierda(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            xAxis -= 1;
        }else if (context.canceled)
        {
            xAxis += 1;
        }
    }

    private IEnumerator ActivarPlayerInput()
    {
        yield return new WaitForSeconds(0.1f);
        playerInput.enabled = true;
    }
}
