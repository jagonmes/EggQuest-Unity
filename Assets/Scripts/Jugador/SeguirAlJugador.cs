using UnityEngine;

public class SeguirAlJugador : MonoBehaviour
{
    public Transform centroJugador;
    public bool jugadorEncontrado = false;
    void Start()
    {
        centroJugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        jugadorEncontrado = centroJugador != null;
    }

    void Update()
    {
        if(jugadorEncontrado)
            this.transform.position = new Vector3(centroJugador.position.x, centroJugador.position.y, this.transform.position.z);
    }
}
