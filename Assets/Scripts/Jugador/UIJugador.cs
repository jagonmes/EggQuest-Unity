using System;
using UnityEngine;

public class UIJugador : MonoBehaviour
{
public static UIJugador Instance;
public SpriteRenderer[] vidas;
public Sprite lleno;
public Sprite vacio;
[SerializeField] private Texto textoGemas;
public int gemasRecolectadas = 0;
[SerializeField]private SoundPlayer soundPlayer;

private void Awake()
{
    Instance = this;
    if(textoGemas == null)
        textoGemas = GetComponent<Texto>();
}

private void Start()
{
    gemasRecolectadas = PlayerPrefs.GetInt("Monedas", 0);
    ActualizarGemasEnUI();
}

public void ActualizarVidas(int vidaActual, int vidaMaxima)
{
    for (int i = 0; i < vidas.Length; i++)
    {
        if(vidaActual >= i + 1)
            vidas[i].sprite = lleno;
        else
            vidas[i].sprite = vacio;
        if(i + 1 > vidaMaxima)
            vidas[i].gameObject.SetActive(false);
        else
            vidas[i].gameObject.SetActive(true);
    }
}

public void RecolectarGema(GameObject gema)
{
    gemasRecolectadas = Math.Clamp(gemasRecolectadas + 1, 0, 99);
    soundPlayer.PlayEffect();
    ActualizarGemasEnUI();
    Destroy(gema);
}

public void ActualizarGemasEnUI()
{
    textoGemas.texto = gemasRecolectadas.ToString("00");
}

}
