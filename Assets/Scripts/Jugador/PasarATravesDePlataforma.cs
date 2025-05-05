using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Serialization;

public class PasarATravesDePlataforma : MonoBehaviour
{
    [SerializeField] private Collider2D colliderDeLaPlataforma;
    private bool _jugadorEnPlataforma = false;
    private EntradaDelJugador _entradaDelJugador;
    [SerializeField]private bool _esperando = false;
    private void Update()
    {
        if (_jugadorEnPlataforma && _entradaDelJugador != null)
            if (_entradaDelJugador.yAxis < 0 && !_esperando && Time.timeScale != 0)
            {
                _esperando = true;
                colliderDeLaPlataforma.enabled = false;
                StartCoroutine(ReactivarPlataforma());
            }
    }

    private void SetJugadorEnPlataforma(Collision2D other, bool enPlataforma)
    {
        var jugador = other.gameObject.GetComponent<EntradaDelJugador>();
        if (jugador != null)
        {
            if (enPlataforma)
            {
                _entradaDelJugador = jugador;
            }
            else
            {
                _entradaDelJugador = null;
            }
            _jugadorEnPlataforma = enPlataforma;
        }
    }

    private IEnumerator ReactivarPlataforma()
    {
       yield return new WaitForSeconds(1.0f);
       colliderDeLaPlataforma.enabled = true;
       _esperando = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetJugadorEnPlataforma(other, true);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        SetJugadorEnPlataforma(other, false);
    }
}
