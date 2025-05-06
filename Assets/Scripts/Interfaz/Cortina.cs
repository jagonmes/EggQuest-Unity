using UnityEngine;

public class Cortina : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float VelocidadDeMovimiento = 100f;
    [SerializeField] private int direccionDeMovimiento = 1;
    [SerializeField] private float distancia = 186f;
    [SerializeField] private bool derecha = true;
    [SerializeField] private bool noDesactivar = false;
    void Start()
    {
        if (!derecha)
        {
            direccionDeMovimiento = -direccionDeMovimiento;
            distancia = -distancia;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
     var posicion = this.transform.position;
     var posicionLocal = this.transform.localPosition;
     if (derecha)
     {
         if (posicionLocal.x > distancia)
         {
             this.gameObject.SetActive(noDesactivar);
         }
         else
         {
             this.transform.position = new Vector3(posicion.x + VelocidadDeMovimiento * Time.deltaTime * direccionDeMovimiento, posicion.y, posicion.z);
         }
         
     }
     else
     {
         if (posicionLocal.x + distancia < distancia)
         {
             this.gameObject.SetActive(noDesactivar);
         }
         else
         {
             this.transform.position = new Vector3(posicion.x + VelocidadDeMovimiento * Time.deltaTime * direccionDeMovimiento, posicion.y, posicion.z);
         }


     }
     
    }
}
