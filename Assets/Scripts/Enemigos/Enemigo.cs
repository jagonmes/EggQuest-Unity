using System;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] protected int vida = 1;
    [SerializeField] protected float velocidadDeMovimiento = 40f;
    [SerializeField] protected int direccionDeMovimiento = 1;
    [SerializeField] protected bool aplastable = false;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    protected void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if(rb != null)
            rb.linearVelocityX = velocidadDeMovimiento * direccionDeMovimiento;
    }
    
    protected virtual void DanarEnemigo()
    {
        vida--;
    }

    protected virtual void Morir()
    {
        Destroy(rb);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DanoAEnemigos"))
        {
            if (aplastable)
            {
                DanarEnemigo();
                if (vida <= 0)
                {
                    Morir();
                }
            }
            else
            {
                other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
            }
        }
        if (other.CompareTag("Espada"))
        {
            DanarEnemigo();
            if (vida <= 0)
            {
                Morir();
            }
        }
    }
    
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
        }
    }
}
