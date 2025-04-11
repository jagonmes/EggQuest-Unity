using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Texto : MonoBehaviour
{
    private int _size = 99;
    public int caracteresPorLinea = 99;
    private int _lines = 99;
    public int lines = 99;
    public int characterSize = 8;
    [SerializeField] private int alturaDeLinea = 9;
    private bool _centered = false;
    [SerializeField] public bool textoCentrado = false;
    private string _texto = "";
    public string texto = "Prueba de texto";
    [SerializeField]private List<string> TextoNormalizado = new List<string>();
    [SerializeField]public List<GameObject> Letras;
    [SerializeField]private GameObject LetraPrefab = null;
    public int OrderInLayer = 1000;
    private bool _activo = true;
    public bool activo = true;

    private void Awake()
    {
        if (Letras == null)
            Letras = new List<GameObject>();
        else
        {
            bool letrasNulas = false;
            foreach (var letra in Letras)
            {
                if (letra == null)
                {
                    letrasNulas = true;
                    break;
                }
            }

            if (letrasNulas)
            {
                Letras.Clear();
                Letras.AddRange(this.GetComponentsInChildren<SpriteRenderer>().Select(x => x.gameObject));
            }

            
        }
    }

    private void Start()
    {
        //TODO CUANDO EL TEXTO DE TODO ESTE DECIDIDO, CAMBIAR ESTO
        LoadText();
        _texto = texto;
        _size = caracteresPorLinea;
        _lines = lines;
        _centered = textoCentrado;
        _activo = activo;
    }

    private void Update()
    {
        if (_texto != texto || caracteresPorLinea != _size || lines != _lines || textoCentrado != _centered || activo != _activo)
        {
            //Debug.Log("CARGANDO TEXTO");
            _lines = lines;
            _texto = texto;
            _size = caracteresPorLinea;
            _centered = textoCentrado;
            _activo = activo;
            LoadText();
        }
    }

    public void LoadText()
    {
        TextoNormalizado = FontManager.Instance.parseText(texto, caracteresPorLinea);

        foreach (var letra in this.GetComponentsInChildren<SpriteRenderer>())
        {
            if (Application.isPlaying)
            {
                Destroy(letra.gameObject);
            }
            else
            {
                DestroyImmediate(letra.gameObject);
            } 
        }
        Letras.Clear();
            

        for (int i = 0; i < TextoNormalizado.Count; i++)
        {
            if (i >= _lines)
            {
                return;
            }
            
            //POSICION INICIAL DEL TEXTO
            int posX = 0;
            if(textoCentrado)
                posX = -(TextoNormalizado[i].Length * characterSize) / 2 + characterSize/2;
            
            foreach (var c in TextoNormalizado[i])
            {
                if (TextoNormalizado[i] == " " || TextoNormalizado[i] == "")
                    continue;
                if (LetraPrefab != null)
                {
                    if (!FontManager.Instance.caracter.ContainsKey(c.ToString()))
                        continue;
                    GameObject letra = Instantiate(LetraPrefab, this.transform);
                    letra.transform.localPosition = new Vector3(this.transform.localPosition.x + posX + characterSize/2, -alturaDeLinea*i, 0);
                    if(activo)
                        letra.GetComponent<SpriteRenderer>().sprite = FontManager.Instance.caracter[c.ToString()];
                    else
                        letra.GetComponent<SpriteRenderer>().sprite = FontManager.Instance.caracterGris[c.ToString()];
                    letra.GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
                    Letras.Add(letra);   
                }

                posX += characterSize;
            }
        }
    }
}
