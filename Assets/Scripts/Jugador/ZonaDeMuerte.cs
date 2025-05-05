using System;
using UnityEngine;

public class ZonaDeMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorDeJugador>().JugadorCaido();
        }
    }
}
