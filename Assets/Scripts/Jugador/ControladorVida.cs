using System;
using System.Collections;
using UnityEngine;

public class ControladorVida : MonoBehaviour
{
    public int vidaMaxima = 0;
    public int vidaActual = 0;

    public float tiempoInvulnerabilidad = 0.5f;
    public bool invulnerable = false;
    public bool invulnerableAnim = false;
    public bool muerto = false;

    [SerializeField] private ControladorDeJugador controladorDeJugador;
    [SerializeField] private SoundPlayer spRecibirDano;
    [SerializeField] private bool cargarVidaMaxima = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(cargarVidaMaxima)
            vidaActual = vidaMaxima;
    }

    // Update is called once per frame
    void Start()
    {
        UIJugador.Instance.ActualizarVidas(vidaActual, vidaMaxima);
    }

    public void RecibirDano(Vector2 collisionPosition)
    {
        if (invulnerable || muerto)
            return;
        vidaActual = Math.Clamp(vidaActual - 1, 0, vidaMaxima);
        UIJugador.Instance.ActualizarVidas(vidaActual, vidaMaxima);
        invulnerable = true;
        invulnerableAnim = true;
        if (vidaActual <= 0)
        {
            invulnerable = false;
            invulnerableAnim = false;
            controladorDeJugador.animator.SetTrigger("Muerto");
            muerto = true;
            controladorDeJugador.MatarAlJugador();
            return;
        }
        spRecibirDano.PlayEffect();
        controladorDeJugador.animator.SetTrigger("RecibirDano");
        controladorDeJugador.KnockBack(tiempoInvulnerabilidad, collisionPosition);
        StartCoroutine(DesactivarInvulnerabilidad());
    }
    
    public void DanoPorCaida()
    {
        vidaActual = Math.Clamp(vidaActual - 1, 0, vidaMaxima);
        UIJugador.Instance.ActualizarVidas(vidaActual, vidaMaxima);
        if (vidaActual <= 0)
        {
            invulnerable = false;
            invulnerableAnim = false;
            controladorDeJugador.animator.SetTrigger("Muerto");
            muerto = true;
            controladorDeJugador.MatarAlJugador();
            return;
        }
    }

    public void ReiniciarVida()
    {
        vidaActual = vidaMaxima;
        UIJugador.Instance.ActualizarVidas(vidaActual, vidaMaxima);
        muerto = false;
        invulnerable = false;
        invulnerableAnim = false;
    }

    private IEnumerator DesactivarInvulnerabilidad()
    {
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        invulnerableAnim = false;
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        invulnerable = false;
    }
    
    
}
