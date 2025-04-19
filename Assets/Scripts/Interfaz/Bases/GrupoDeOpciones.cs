using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrupoDeOpciones : MonoBehaviour
{
    [SerializeField]protected List<Opcion> opciones = new List<Opcion>();
    [SerializeField]private GameObject flecha;
    [SerializeField]private PlayerInput playerInput;
    protected int opcionSeleccionada = 0;
    protected OptionSound optionSound;
    void Start()
    {
        playerInput.enabled = true;
        optionSound = FindFirstObjectByType<OptionSound>();
        RecolocarFlecha();
    }
    
    public virtual void Arriba(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion arriba del grupo de acciones: " + this.gameObject.name);
                for (int i = opcionSeleccionada - 1; i < opciones.Count && i >= 0; i--)
                {
                    if (opciones[i].texto.activo && opciones[i].texto.Letras.Count > 0)
                    {
                        flecha.transform.position = new Vector3(opciones[i].texto.Letras[0].transform.position.x - 8,
                            opciones[i].texto.Letras[0].transform.position.y,
                            opciones[i].texto.Letras[0].transform.position.z);
                        opcionSeleccionada = i;
                        if(optionSound != null)
                            optionSound.PlayEffect();
                        break;
                    }
                } 
            }
        }
    }
    public virtual void Abajo(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion abajo del grupo de acciones: " + this.gameObject.name);
                for (int i = opcionSeleccionada + 1; i < opciones.Count; i++)
                {
                    if (opciones[i].texto.activo && opciones[i].texto.Letras.Count > 0)
                    {
                        flecha.transform.position = new Vector3(opciones[i].texto.Letras[0].transform.position.x - 8,
                            opciones[i].texto.Letras[0].transform.position.y,
                            opciones[i].texto.Letras[0].transform.position.z);
                        opcionSeleccionada = i;
                        if (optionSound != null)
                            optionSound.PlayEffect();
                        break;
                    }
                }
            }
        }
    }
    public virtual void Izquierda(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion izquierda del grupo de acciones: " + this.gameObject.name);
                if (opciones.Count != 0)
                    if (opciones[opcionSeleccionada] != null)
                        opciones[opcionSeleccionada].Izquierda();
            }
        }
    }
    public virtual void Derecha(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion derecha del grupo de acciones: " + this.gameObject.name);
                if (opciones.Count != 0)
                    if (opciones[opcionSeleccionada] != null)
                        opciones[opcionSeleccionada].Derecha();
            }
        }
    }
    
    public virtual void Accion(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion derecha del grupo de acciones: " + this.gameObject.name);
                if (opciones.Count != 0)
                    if (opciones[opcionSeleccionada] != null)
                        opciones[opcionSeleccionada].Accion();
            }
        }
    }
    public virtual void Volver(InputAction.CallbackContext context)
    {
        if (context.performed && flecha != null)
        {
            if (flecha.activeInHierarchy)
            {
                Debug.Log("Accion volver del grupo de acciones: " + this.gameObject.name);
                if (optionSound != null)
                    optionSound.PlayEffect();
            }
        }
    }
    
    public virtual void StartButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Accion start del grupo de acciones: " + this.gameObject.name);
        }
    }

    public void Update()
    {
        RecolocarFlecha();
    }

    public void RecolocarFlecha()
    {
        if (flecha != null && flecha.activeInHierarchy)
        {
            if (opciones.Count != 0)
            {
                for (int i = opcionSeleccionada; i < opciones.Count; i++)
                {
                    if (opciones[i].texto.activo && opciones[i].texto.Letras.Count > 0)
                    {
                        flecha.transform.position = new Vector3(opciones[i].texto.Letras[0].transform.position.x - 8,opciones[i].texto.Letras[0].transform.position.y, opciones[i].texto.Letras[0].transform.position.z);
                        opcionSeleccionada = i;
                        break;
                    }
                }
            }
        }
    }

    public void ReiniciarOpcionSeleccionada()
    {
        opcionSeleccionada = 0;
        RecolocarFlecha();
    }
}
