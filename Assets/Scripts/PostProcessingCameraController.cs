using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingCameraController : MonoBehaviour
{
    [SerializeField]private EnableRenderFeatures enableRenderFeaturesScript;
    [SerializeField]private Camera thisCamera;
    
    [Header("PALETA DE COLORES")]
    [SerializeField]private Material PaletaDeColores;
    [SerializeField] private Sprite PaletaPorDefecto;
    
    [Header("GHOSTING")]
    [SerializeField]private Material Ghosting;

    public bool ActivarGhosting { get; private set; } = false;

    [Header("GRID")]
    [SerializeField]private Material Grid720;
    [SerializeField]private Material Grid1440;
    [SerializeField]private UniversalRendererData rendererData;
    private Material Grid;
    public bool ActivarGrid { get; private set; } = false;
    private FullScreenPassRendererFeature gridRenderFeature;
    
    //TEXTURA
    private int Width = 0;
    private int Height = 0;
    

    private void Awake()
    {
        if (thisCamera == null)
            thisCamera = this.gameObject.GetComponent<Camera>();

        if (thisCamera != null)
        {
            RenderPipelineManager.beginCameraRendering += CameraResolutionChange;
        }
        
        if (rendererData != null)
        {
            foreach (var feature in rendererData.rendererFeatures)
            {
                if (feature is FullScreenPassRendererFeature fullScreenPassFeature)
                {
                    if (feature.name == "Grid")
                    {
                        gridRenderFeature = fullScreenPassFeature;
                        break;
                    }
                }
            }
        }
        //CARGAMOS EL MATERIAL DEL GRID
        if(Grid720 != null)
            Grid = Grid720;
        else if(Grid1440 != null)
            Grid = Grid1440;
        else
            Debug.LogError("No hay grid asignado");
        
        if (Ghosting != null)
        {
            ActivarGhosting = Ghosting.GetInt("_Activo") == 1;
            SetGhosting();
        }
        if (Grid != null)
        {
            ActivarGrid = Grid.GetInt("_Activo") == 1;
            SetGrid();
        }
        
    }

    private void Start()
    {
        //TODO CARGAR AQUI LA CONFIGURACION PREVIA
        ChangeTextureResolution();
        ColorManager.Instance.SelectedColors = ColorManager.Instance.ColorPresets[ColorManager.Instance.SelectedColorPreset].ToArray();
        LoadColorPallete();
        ActivarGrid = ColorManager.Instance.GridActive;
        ActivarGhosting = ColorManager.Instance.GhostingActive;
        SetGhosting();
        SetGrid();
    }

    //FUNCIONES PALETA DE COLORES
    [ContextMenu("Load Color Pallete")]
    public void LoadColorPallete()
    {
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < ColorManager.Instance.SelectedColors.Length; i++)
        {
            Sprite sprite = null;
            if(ColorManager.Instance.ColorPallets.ContainsKey(ColorManager.Instance.SelectedColors[i]))
                sprite = ColorManager.Instance.ColorPallets[ColorManager.Instance.SelectedColors[i]];
            if(sprite == null)
                sprite = PaletaPorDefecto;
            sprites.Add(sprite);
        }
        PaletaDeColores.SetTexture("_Paleta_de_Colores_BackGround", sprites[0].texture);
        PaletaDeColores.SetTexture("_Paleta_de_Colores_Plataformas", sprites[1].texture);
        PaletaDeColores.SetTexture("_Paleta_de_Colores_Enemigos", sprites[2].texture);
        PaletaDeColores.SetTexture("_Paleta_de_Colores_Jugador", sprites[3].texture);
        PaletaDeColores.SetTexture("_Paleta_de_Colores_ForeGround", sprites[4].texture);
        PaletaDeColores.SetTexture("_Paleta_de_Colores_Interfaz", sprites[5].texture);
    }

    //FUNCIONES GHOSTING
    [ContextMenu("Ghosting Toggle")]
    public void GhostingToggle()
    {
        ActivarGhosting = !ActivarGhosting;
        SetGhosting();
    }

    public void SetGhosting()
    {
        if(Ghosting != null)
            Ghosting.SetInt("_Activo", ActivarGhosting ? 1 : 0);
    }
    
    //FUNCIONES GRID
    [ContextMenu("Grid Toggle")]
    public void GridToggle()
    {
        ActivarGrid = !ActivarGrid;
        SetGrid();
    }

    public void SetGrid()
    {
        if(Grid != null)
            Grid.SetInt("_Activo", ActivarGrid ? 1 : 0);
    }
    
    //FUNCIONES RENDER TEXTURE
    void CameraResolutionChange(ScriptableRenderContext context, Camera currentCamera)
    {
        if (this.enabled)
        {
            if (this.thisCamera != null)
            {
                if (this.Width != Screen.width || this.Height != Screen.height)
                {
                    ChangeTextureResolution();
                }
            }
        }
    }

    private void ChangeTextureResolution()
    {
        RenderTexture texture = this.thisCamera.targetTexture;
        this.thisCamera.targetTexture = null;
        texture.Release();
                    
        int Height = 1440;
        int Width = 1600;
                    
        if (Screen.height == 720)
        {
            Height = 720;
            Width = 800;
            if (gridRenderFeature != null && Grid720 != null)
            {
                gridRenderFeature.passMaterial = Grid720;
                Grid = Grid720;
                SetGrid();
            }
        }
        else
        {
            int multiplier = (int)Mathf.Max(Mathf.Floor(Screen.height / 1440.0f), 1);
            Height = 1440 * multiplier;
            Width = 1600 * multiplier;
            if (gridRenderFeature != null && Grid1440 != null)
            {
                gridRenderFeature.passMaterial = Grid1440;
                Grid = Grid1440;
                SetGrid();
            }
        }

                    
        texture.width = Width;
        texture.height = Height;
        this.thisCamera.targetTexture = texture;
        this.Width = Screen.width;
        this.Height = Screen.height;
    }

    private void OnDestroy()
    {
        if (thisCamera != null)
        {
            RenderPipelineManager.beginCameraRendering -= CameraResolutionChange;
        }
    }
}
