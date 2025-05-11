using UnityEngine;

public class ReboteEspada : MonoBehaviour
{
    [SerializeField]ControladorDeJugador _controladorDeJugador;
    [SerializeField] public bool reboteHotizontal = false;

    private void Start()
    {
        if (_controladorDeJugador == null)
            _controladorDeJugador = FindObjectOfType<ControladorDeJugador>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_controladorDeJugador == null)
            return;
        if (other.CompareTag("Espada"))
        {
            if (other.gameObject.name == "AtaqueAbajo")
            {
                _controladorDeJugador.ReboteEspadaVertical();
            }
            else if(reboteHotizontal)
            {
                _controladorDeJugador.ReboteEspada(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
            }
        }
    }
}
