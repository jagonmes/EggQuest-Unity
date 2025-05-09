using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Altar : MonoBehaviour
{
    [SerializeField] GameObject HuevoBase;
    [SerializeField] SpriteRenderer espada;
    [SerializeField] GameObject espalda;
    [SerializeField] GameObject espaldaCapa;
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject JugadorCapa;
    [SerializeField] GameObject EspadaUI;
    private bool activado = false;
    private bool enPosicion = false;
    [SerializeField] SeguirAlJugador seguirAlJugador;


    private void Update()
    {
        if (activado && !enPosicion)
        {
            Jugador.GetComponent<PlayerInput>().enabled = false;
            if (Jugador.transform.position.x > this.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = -1;
            }else if (Jugador.transform.position.x < this.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = 1;
            }

            if (Aprox(Jugador.transform.position.x, this.transform.position.x, 1f))
            {
                enPosicion = true;
                StartCoroutine(activarHuevoConCapa());
            }
        }

        
    }

    private bool Aprox(float a, float b, float range)
    {
        if(b + range > a && b - range < a)
        {
            return true;
        }else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;
        }
    }

    private IEnumerator activarHuevoConCapa()
    {
        Jugador.SetActive(false);
        JugadorCapa.transform.position = Jugador.transform.position;
        HuevoBase.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HuevoBase.SetActive(false);
        espalda.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        espada.enabled = false;
        yield return new WaitForSeconds(0.5f);
        espalda.SetActive(false);
        espaldaCapa.SetActive(true);
        EspadaUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        espaldaCapa.SetActive(false);
        JugadorCapa.GetComponent<ControladorVida>().vidaActual = Jugador.GetComponent<ControladorVida>().vidaActual;
        JugadorCapa.SetActive(true);
        seguirAlJugador.centroJugador = JugadorCapa.transform;
    }
}
