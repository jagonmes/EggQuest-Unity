using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

//INDICAMOS QUE LA FUENTE SE CARGUE, INCLUSO EN EL EDITOR
[ExecuteInEditMode]
public class FontManager : MonoBehaviour
{
    private static FontManager _instance;

    [SerializeField] private Sprite[] fontSprites = null;
    [SerializeField] private Sprite[] fontGraySprites = null;
    [SerializeField] private string listaDeCaracteres = "";
    public static FontManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FontManager>();
                if (_instance != null)
                {
                    if(Application.isPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }
                else
                {
                    Debug.LogError("No hay FontManager en la escena.");
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    //DICCCIONARIO QUE CONTIENE LOS CARACTERES Y SUS RESPECTIVOS SPRITES
    public Dictionary<string, Sprite> caracter { get;private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> caracterGris { get;private set; } = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if(FontManager.Instance != null && FontManager.Instance != this)
            Destroy(this.gameObject);
        LoadFont();
    }


    //PASAMOS EL TEXTO A LINEAS
    public List<string> parseText(string texto, int size)
    {
        List<string> lineasDeTexto = new List<string>();
        string[] parrafos = texto.Split("\n");
        int currentSize = 0;
        string lineaActual = "";
        foreach (string parrafo in parrafos)
        {
            string[] palabras = parrafo.Split(" ");
            foreach (string palabra in palabras)
            {
                int wordSize = palabra.Length;
                //COMPROBAMOS SI LA PALABRA CABE EN UNA LINEA, SI NO CABE, LA SALTAMOS
                if(wordSize > size)
                    continue;
                
                //SI LA PALABRA CABE EN LA LINEA ACTUAL
                if (currentSize + wordSize <= size)
                {
                    //ANADIMOS LA PALABRA A LA LINEA ACTUAL CON UN ESPACIO
                    lineaActual += palabra + " ";
                    //AUMENTAMOS EL TAMANO ACTUAL
                    currentSize += wordSize + 1;
                }
                //SI LA PALABRA NO CABE EN LA LINEA ACTUAL
                else
                {
                    //ANADIMOS LA LINEA ACTUAL A LA LISTA
                    lineasDeTexto.Add(lineaActual);
                    //LIMPIAMOS LA LINEA ACTUAL
                    lineaActual = "";
                    currentSize = 0;
                    //ANADIMOS LA PALABRA A LA LINEA ACTUAL CON UN ESPACIO
                    lineaActual += palabra + " ";
                    //AUMENTAMOS EL TAMANO ACTUAL
                    currentSize += wordSize + 1;
                }
            }
            //SI LA LINEA ACTUAL NO ESTA VACIA, LA ANADIMOS A LA LISTA
            if (lineaActual != "")
            {
                lineasDeTexto.Add(lineaActual);
                lineaActual = "";
                currentSize = 0;
            }
        }
        return lineasDeTexto;
    }

    //RELLENA EL DICCIONARIO DE CARACTERES
    private void LoadFont()
    {
        //GENERA EL DICCIONARIO DE CARACTERES
        for (int i = 0; i < listaDeCaracteres.Length; i++)
        {
            if(i >= fontSprites.Length && i > fontSprites.Length)
            {
                Debug.LogError("No hay suficientes sprites para la lista de caracteres.");
                return;
            }
            caracter.Add(listaDeCaracteres[i].ToString(), fontSprites[i]);
            caracterGris.Add(listaDeCaracteres[i].ToString(), fontGraySprites[i]);
        }
        
    }
    
    //CARGA LOS ASSETS DE LA FUENTE DE TEXTO
    [ContextMenu("Load Font Assets")]
    private void LoadFontAssets()
    {
        // CARGA TODAS LOS SPRITES DE LA FUENTE DE TEXTO
        fontSprites = Resources.LoadAll<Sprite>("Fuente de texto/Fuente/");
        fontGraySprites = Resources.LoadAll<Sprite>("Fuente de texto/Fuente-Gris/");
        // VERIFICA QUE HAY SPRITES
        if (fontSprites != null || fontGraySprites != null)
        {
            if (fontSprites.Length == 0 || fontGraySprites.Length == 0)
            {
                Debug.LogError("No se encontraron sprites en la fuente de texto.");
                return;
            }
        }
        else
        {
            Debug.LogError("No se pudieron cargar los sprites de la fuente de texto.");
            return;
        }
        // CARGA LA LISTA DE CARACTERES
        try
        {
            string path = "Assets/Resources/Fuente de texto/ListaDeCaracteres.txt";
            StreamReader reader = new StreamReader(path); 
            listaDeCaracteres = reader.ReadToEnd();
            reader.Close();
        }catch (Exception e)
        {
            Debug.LogError("No se pudo cargar la lista de caracteres: " + e.Message);
            return;
        }
    }
}
