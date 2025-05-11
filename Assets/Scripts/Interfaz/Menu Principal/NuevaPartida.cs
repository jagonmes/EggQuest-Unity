using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartida : Opcion
{
    [SerializeField]private string nombreEscena;
    public override void Accion()
    {
        ConfigManager.Instance.SavePrefs("LastLevel", "");
        ConfigManager.Instance.SavePrefs("Monedas", 0);
        SceneManager.LoadScene(nombreEscena);
    }
}
