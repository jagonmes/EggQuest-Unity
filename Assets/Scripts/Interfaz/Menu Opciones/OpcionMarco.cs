using UnityEngine;

public class OpcionMarco : Opcion
{
    void Start()
    {
        texto.activo = false;
        string aux = "<----->"; 
        string aux2 = "Marco:";
        aux2+= new string(' ', texto.caracteresPorLinea - (aux2.Length - 1));
        texto.texto = aux2.Remove(aux2.Length - aux.Length - 1) + aux;
        texto.LoadText();
        return;
    }
    
    public override void Accion()
    {
    }
}
