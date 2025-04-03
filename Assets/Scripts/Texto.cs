using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Texto : MonoBehaviour
{
    private int _size = 99;
    public int size = 99;
    public int characterSize = 8;
    [SerializeField] private int alturaDeLinea = 9;
    private string _texto = "";
    public string texto = "Prueba de texto";
    [SerializeField]private List<string> TextoNormalizado = new List<string>();
    [SerializeField]private List<GameObject> Letras;
    [SerializeField]private GameObject LetraPrefab = null;

    private void Awake()
    {
        if (Letras == null)
            Letras = new List<GameObject>();
    }

    private void Start()
    {
        //TODO CUANDO EL TEXTO DE TODO ESTE DECIDIDO, CAMBIAR ESTO
        if (Application.isEditor)
        LoadText();
        _texto = texto;
        _size = size;
        
    }

    private void Update()
    {

        if (Application.isEditor)
        if (_texto != texto || size != _size)
        {
            _texto = texto;
            _size = size;
            LoadText();
        }
        
    }

    private void LoadText()
    {
        TextoNormalizado = FontManager.Instance.parseText(texto, size);
        foreach (GameObject letra in Letras)
        {
            if(Application.isPlaying)
                Destroy(letra);
            else
            {
                DestroyImmediate(letra);
            }
        }

        Letras.Clear();
            

        for (int i = 0; i < TextoNormalizado.Count; i++)
        {
            int posX = 0;
            foreach (var c in TextoNormalizado[i])
            {
                if (TextoNormalizado[i] == " " || TextoNormalizado[i] == "")
                    continue;
                if (LetraPrefab != null)
                {
                    if (!FontManager.Instance.caracter.ContainsKey(c.ToString()))
                        continue;
                    GameObject letra = Instantiate(LetraPrefab, this.transform);
                    letra.transform.localPosition = new Vector3(this.transform.localPosition.x + posX, -alturaDeLinea*i, 0);
                    letra.GetComponent<SpriteRenderer>().sprite = FontManager.Instance.caracter[c.ToString()];
                    Letras.Add(letra);   
                }

                posX += characterSize;
            }
        }
    }
}
