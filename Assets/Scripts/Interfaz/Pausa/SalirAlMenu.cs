using UnityEngine;

public class SalirAlMenu : Opcion
{
    public override void Accion()
    {
        if(this.gameObject.activeInHierarchy == false)
            return;
        base.Accion();
        // Cargar la escena del menu principal
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu Principal");
    }
}
