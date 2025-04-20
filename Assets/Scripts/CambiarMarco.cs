using System;
using UnityEngine;
using UnityEngine.UI;

public class CambiarMarco : MonoBehaviour
{
    [SerializeField] private GameObject Fondo;
    [SerializeField] private GameObject Marco;
    
    void Awake()
    {
        if (Marco == null)
            Marco = GameObject.Find("Marco-MainCamera");
        if (Fondo == null)
            Fondo = GameObject.Find("Fondo-MainCamera");
    }

    private void Start()
    {
        if(BorderManager.Instance != null)
            BorderManager.Instance.OnBorderChanged += cambiarMarco;
        cambiarMarco();
    }

    private void cambiarMarco()
    {
        int selectedBorderIndex = BorderManager.Instance.BorderNames.IndexOf(BorderManager.Instance.SelectedBorder);
        Fondo.GetComponent<Image>().sprite = BorderManager.Instance.Sprites[selectedBorderIndex];
        Fondo.GetComponent<Image>().color = BorderManager.Instance.BGColors[selectedBorderIndex];
        Marco.GetComponent<Image>().color = BorderManager.Instance.FGColors[selectedBorderIndex]; 
    }
    
    
    private void OnDestroy()
    {
        if(BorderManager.Instance != null)
            BorderManager.Instance.OnBorderChanged -= cambiarMarco;
    }

}
