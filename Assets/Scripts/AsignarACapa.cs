using System;
using UnityEngine;

[ExecuteInEditMode]
public class AsignarACapa : MonoBehaviour
{
    public Material sprite2D_Profundidad;

    private void Awake()
    {
        if(Application.isPlaying)
            AsignarACapaDeColor();
    }

    [ContextMenu("Asignar a capa de color")]
    public void AsignarACapaDeColor()
    {
        foreach (var go in this.GetComponentsInChildren<Transform>())
        {
            SpriteRenderer[] spriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in spriteRenderers)
            {
                if(sprite2D_Profundidad != null)
                    spriteRenderer.sharedMaterial = sprite2D_Profundidad;
                spriteRenderer.gameObject.layer = go.gameObject.layer;
                spriteRenderer.gameObject.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x, spriteRenderer.transform.localPosition.y, 0);
            }
        }
    }
}
