using UnityEngine;

public class GridOption : Opcion
{
    [SerializeField]private Texto caja;
    [SerializeField]private PostProcessingCameraController PPcameraController;

    private void Start()
    {
        if(PPcameraController == null)
            PPcameraController = FindFirstObjectByType<PostProcessingCameraController>();
        if (PPcameraController == null)
            return;
        caja.texto = "               [" + (ColorManager.Instance.GridActive?"x":" ") + "]";
    }
    
    public override void Accion()
    {
        base.Accion();
        PPcameraController.GridToggle();
        caja.texto = "               [" + (PPcameraController.ActivarGrid?"x":" ") + "]";
        ColorManager.Instance.GridActive = PPcameraController.ActivarGrid;
        ConfigManager.Instance.SavePrefs("Grid", ColorManager.Instance.GridActive?1:0);
    }
}
