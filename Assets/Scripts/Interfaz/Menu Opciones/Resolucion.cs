using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Resolucion : Opcion
{
    public List<resolucion> resoluciones = new List<resolucion>();
    public int selectedResolution = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Application.isMobilePlatform || Application.isEditor)
        {
            texto.activo = false;
            string aux3 = "<----x---->"; 
            string aux4 = "Resolución:                         ";
            texto.texto = aux4.Remove(aux4.Length - aux3.Length - 1) + aux3;
            texto.LoadText();
            return;
        }
        int lastHeight = 0;
        int lastWidth = 0;
        List<Resolution> resolucionesPosibles = Screen.resolutions.ToList();
        foreach (var res in resolucionesPosibles)
        {
            if (((res.height % 360 == 0 || (res.height / 720 >= 1 && res.height % 720 == 0.5f)) && res.width > res.height) &&(lastHeight != res.height || lastWidth != res.width))
            {
                resolucion resAux = new resolucion(res.height, res.width);
                if (!resoluciones.Contains(resAux))
                {
                    Debug.Log(resAux.altura + " " + resAux.ancho);
                    resoluciones.Add(new resolucion(res.height, res.width));
                    lastHeight = res.height;
                    lastWidth = res.width;
                    if (res.width == Screen.width && res.height == Screen.height)
                    {
                        selectedResolution = resoluciones.Count - 1;
                    }
                }
            }
        }
        string aux = "<" + resoluciones[selectedResolution].ancho+"x"+resoluciones[selectedResolution].altura + ">";
        string aux2 = "Resolución:                         ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        
        Debug.Log(resoluciones.Count);
    }
    public override void Derecha()
    {
        selectedResolution++;
        if(selectedResolution >= resoluciones.Count)
            selectedResolution = 0;
        string aux = "<" + resoluciones[selectedResolution].ancho+"x"+resoluciones[selectedResolution].altura + ">";
        string aux2 = "Resolución:                         ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        Screen.SetResolution(resoluciones[selectedResolution].ancho, resoluciones[selectedResolution].altura, Screen.fullScreen);
    }

    public override void Izquierda()
    {
        selectedResolution--;
        if(selectedResolution < 0)
            selectedResolution = resoluciones.Count - 1;
        string aux = "<" + resoluciones[selectedResolution].ancho+"x"+resoluciones[selectedResolution].altura + ">";
        string aux2 = "Resolución:                         ";
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        Screen.SetResolution(resoluciones[selectedResolution].ancho, resoluciones[selectedResolution].altura, Screen.fullScreen);
    }

    public class resolucion
    {
        public int altura = 0;
        public int ancho = 0;

        public resolucion(int altura, int ancho)
        {
            this.altura = altura;
            this.ancho = ancho;
        }
    }
}
