using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cinematica : MonoBehaviour
{
[SerializeField] GameObject HuevoBase;
    [SerializeField] GameObject DialogoUI;
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject Arbol;
    [SerializeField] GameObject Punto1;
    [SerializeField] GameObject Punto2;
    [SerializeField] private Texto texto;
    private bool activado = false;
    private bool enPosicion1 = false;
    private bool enPosicion2 = false;


    private void Update()
    {
        if (activado && !enPosicion1)
        {
            Jugador.GetComponent<PlayerInput>().enabled = false;
            if (Jugador.transform.position.x > Punto1.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = -1;
            }else if (Jugador.transform.position.x < Punto1.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = 1;
            }

            if (Aprox(Jugador.transform.position.x, Punto1.transform.position.x, 1f))
            {
                enPosicion1 = true;
                Jugador.GetComponent<EntradaDelJugador>().xAxis = 0;
                Jugador.GetComponent<SpriteRenderer>().flipX = false;
                StartCoroutine(activarEscena());
            }
        }else if(!activado && enPosicion1 && !enPosicion2)
        {
            Jugador.GetComponent<PlayerInput>().enabled = false;
            if (Jugador.transform.position.x > Punto2.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = -1;
            }else if (Jugador.transform.position.x < Punto2.transform.position.x)
            {
                Jugador.GetComponent<EntradaDelJugador>().xAxis = 1;
            }

            if (Aprox(Jugador.transform.position.x, Punto2.transform.position.x, 1f))
            {
                enPosicion2 = true;
                Jugador.GetComponent<EntradaDelJugador>().xAxis = 0;
                Jugador.GetComponent<SpriteRenderer>().flipX = false;
                activado = true;
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

    private IEnumerator activarEscena()
    {
        Arbol.SetActive(false);
        DialogoUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        texto.texto = "En fin... Ya que has vencido a ese bicho...";
        yield return new WaitForSeconds(4f);
        texto.texto = "Supongo que puedes pasar.";
        yield return new WaitForSeconds(4f);
        DialogoUI.SetActive(false);
        activado = false;
    }
}
