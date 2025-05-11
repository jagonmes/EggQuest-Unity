using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteNivel : MonoBehaviour
{
    [SerializeField] private string nombreDelNivel;
    [SerializeField] private GameObject cortina;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorDeJugador>().enabled = true;
            StartCoroutine(CargarSiguienteNivel());
        }
    }

    private IEnumerator CargarSiguienteNivel()
    {
        cortina.SetActive(true);
        ConfigManager.Instance.SavePrefs("Monedas", UIJugador.Instance.gemasRecolectadas);
        ConfigManager.Instance.SavePrefs("LastLevel", nombreDelNivel=="Menu Principal" ? "" : nombreDelNivel);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nombreDelNivel);
    }
}
