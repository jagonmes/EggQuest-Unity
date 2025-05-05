using UnityEngine;

public class RecogerGema : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIJugador.Instance?.RecolectarGema(gameObject);
        }
    }
}
