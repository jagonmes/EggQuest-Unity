using System;
using UnityEngine;

public class Ghosting : Opcion
{
    [SerializeField]private Texto caja;
    [SerializeField]private PostProcessingCameraController PPcameraController;

    private void Start()
    {
        if(PPcameraController == null)
            PPcameraController = FindFirstObjectByType<PostProcessingCameraController>();
        if (PPcameraController == null)
            return;
        caja.texto = "               [" + (ColorManager.Instance.GhostingActive?"x":" ") + "]";
    }
    
    public override void Accion()
    {
        base.Accion();
        PPcameraController.GhostingToggle();
        caja.texto = "               [" + (PPcameraController.ActivarGhosting?"x":" ") + "]";
        ColorManager.Instance.GhostingActive = PPcameraController.ActivarGhosting;
        ConfigManager.Instance.SavePrefs("Ghosting", ColorManager.Instance.GhostingActive?1:0);
    }
}
