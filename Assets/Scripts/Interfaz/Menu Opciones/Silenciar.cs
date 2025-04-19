using UnityEngine;

public class Silenciar : Opcion
{
    [SerializeField]private Texto caja;

    private void Start()
    {
        caja.texto = "               [" + (SoundManager.Instance.Muted?"x":" ") + "]";
    }
    
    public override void Accion()
    {
        base.Accion();
        SoundManager.Instance.Muted = !SoundManager.Instance.Muted;
        ConfigManager.Instance.SavePrefs("Muted", SoundManager.Instance.Muted?1:0);
        caja.texto = "               [" + (SoundManager.Instance.Muted?"x":" ") + "]";
    }
}
