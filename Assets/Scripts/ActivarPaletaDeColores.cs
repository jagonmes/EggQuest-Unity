using UnityEngine;

public class ActivarPaletaDeColores : MonoBehaviour
{
    [SerializeField]private Material _material;
    [SerializeField]private MeshRenderer _meshRenderer;
    void Awake()
    {
        Material[] materials = _meshRenderer.materials;
        materials[0] = _material;
        _meshRenderer.materials = materials;
    }
    void Update()
    {
        Debug.Log("RG16 " + SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RG16));
        Debug.Log("ARGB32 " + SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32));
    }
}
