using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<ControladorDeJugador>().respawnPoint != respawnPoint)
            {
                other.GetComponent<ControladorDeJugador>().respawnPoint = respawnPoint;
                other.GetComponent<ControladorVida>().ReiniciarVida();
            }
        }
    }
}
