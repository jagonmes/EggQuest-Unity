using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[ExecuteInEditMode]
public class AsignarACapa : MonoBehaviour
{
    public Material sprite2D_Profundidad;
    public List<GameObject> Layers = new List<GameObject>();
    public bool active = true;

    private void Awake()
    {
        if(Application.isPlaying && true)
            AsignarACapaDeColor();
    }

    [ContextMenu("Asignar a capa de color")]
    public void AsignarACapaDeColor()
    {
        foreach (var layer in this.Layers)
        {
            SpriteRenderer[] spriteRenderers = layer.GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in spriteRenderers)
            {
                if(sprite2D_Profundidad != null)
                    spriteRenderer.sharedMaterial = sprite2D_Profundidad;
                spriteRenderer.gameObject.layer = layer.gameObject.layer;
                spriteRenderer.gameObject.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x, spriteRenderer.transform.localPosition.y, 0);
            }
        }
    }
}
