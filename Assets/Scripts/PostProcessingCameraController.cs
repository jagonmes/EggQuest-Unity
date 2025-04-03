using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Rendering;


public class PostProcessingCameraController : MonoBehaviour
{
    [SerializeField]private EnableRenderFeatures enableRenderFeaturesScript;
    [SerializeField]private Camera thisCamera;
    
    [Header("PALETA DE COLORES")]
    [SerializeField]private Material PaletaDeColores;
    [SerializeField]public string[] Colors = {"DMG", "DMG", "DMG", "DMG","DMG", "DMG"};
    [SerializeField] private Sprite PaletaPorDefecto;
    
    [Header("GHOSTING")]
    [SerializeField]private Material Ghosting;
    private bool ActivarGhosting = false;
    
    [Header("GRID")]
    [SerializeField]private Material Grid;
    private bool ActivarGrid = false;
    
    //TEXTURA
    private int Width = 0;
    private int Height = 0;
    

    private void Awake()
    {
        if (thisCamera == null)
            thisCamera = this.gameObject.GetComponent<Camera>();

        if (thisCamera != null)
        {
            RenderPipelineManager.beginCameraRendering += ChangeTextureResolution;
        }
    }

    private void Start()
    {
        //TODO CARGAR AQUI LA CONFIGURACION PREVIA
        LoadColorPallete();
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

    //FUNCIONES PALETA DE COLORES
    [ContextMenu("Load Color Pallete")]
    public void LoadColorPallete()
    {
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < Colors.Length; i++)
        {
            Sprite sprite = null;
            if(ColorManager.Instance.ColorPallets.ContainsKey(Colors[i]))
                sprite = ColorManager.Instance.ColorPallets[Colors[i]];
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
    void ChangeTextureResolution(ScriptableRenderContext context, Camera currentCamera)
    {
        if (this.enabled)
        {
            if (this.thisCamera != null)
            {
                if (this.Width != Screen.width || this.Height != Screen.height)
                {
                    RenderTexture texture = this.thisCamera.targetTexture;
                    this.thisCamera.targetTexture = null;
                    texture.Release();
                    /*
                    int multiplier = (int)Mathf.Max(Screen.height / 720, 1);
                    int Height = 720 * multiplier;
                    int Width = 800 * multiplier;
                    */
                    int multiplier = (int)Mathf.Max(Screen.height / 1440, 1);
                    int Height = 1440 * multiplier;
                    int Width = 1600 * multiplier;
                    texture.width = Width;
                    texture.height = Height;
                    this.thisCamera.targetTexture = texture;
                    this.Width = Screen.width;
                    this.Height = Screen.height;
                }
            }
        }
    }
    
    private void OnDestroy()
    {
        if (thisCamera != null)
        {
            RenderPipelineManager.beginCameraRendering -= ChangeTextureResolution;
        }
    }
}
