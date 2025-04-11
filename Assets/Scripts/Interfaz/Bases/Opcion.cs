using System;
using UnityEngine;

public class Opcion : MonoBehaviour
{
    public Texto texto;

    private void Awake()
    {
        if(texto == null)
            texto = GetComponent<Texto>();
    }

    public virtual void Accion()
    {
        Debug.Log("Accion principal de la opcion: " + this.gameObject.name);
    }
    public virtual void Derecha()
    {
        Debug.Log("Accion derecha de la opcion: " + this.gameObject.name);
        return;
    }
    
    public virtual void Izquierda()
    {
        Debug.Log("Accion izquierda de la opcion: " + this.gameObject.name);
        return;
    }
}
