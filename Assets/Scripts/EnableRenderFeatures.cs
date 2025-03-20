using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class EnableRenderFeatures : MonoBehaviour
{

    [SerializeField]private List<FullScreenPassRendererFeature> renderFeatures = new List<FullScreenPassRendererFeature>();
    [SerializeField]private UniversalRendererData rendererData;
    [SerializeField]private Camera ThisCamera;
    [SerializeField]private List<bool> enabledFeatures = new List<bool>();

    private void Awake()
    {
        if (ThisCamera == null)
            ThisCamera = this.gameObject.GetComponent<Camera>();

        if (ThisCamera != null)
        {
            if (rendererData != null)
            {
                foreach (var feature in rendererData.rendererFeatures)
                {
                    if (feature is FullScreenPassRendererFeature fullScreenPassFeature)
                    {
                        renderFeatures.Add(fullScreenPassFeature);
                    }
                }
                RenderPipelineManager.beginCameraRendering += EnableFeatures;
            }
        }
    }
    
    void EnableFeatures(ScriptableRenderContext context, Camera currentCamera)
    {
        if (this.enabled)
        {
            if (this.ThisCamera != null)
            {
                if(currentCamera.name == this.ThisCamera.name)
                {
                    for (int i = 0; i < renderFeatures.Count; i++)
                    {
                        if(enabledFeatures.Count > i)
                            renderFeatures[i].SetActive(enabledFeatures[i]);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (ThisCamera != null)
        {
            if (rendererData != null)
            {
                RenderPipelineManager.beginCameraRendering -= EnableFeatures;
            }
        }
    }
}
