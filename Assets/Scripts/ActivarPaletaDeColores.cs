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
}
