using UnityEngine;

public class DanarAlJugadorPorContacto : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
        }
    }
}
