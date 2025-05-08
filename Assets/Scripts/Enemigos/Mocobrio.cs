using System.Collections;
using UnityEngine;

public class Mocobrio : Enemigo
{

    private bool aplastado = false;
    [SerializeField] private SoundPlayer spMuerte;
    [SerializeField] private SoundPlayer spSalto;
    [SerializeField]private Collider2D colliderDano;
    protected override void Morir()
    {
        base.Morir();
        colliderDano.enabled = false;
        if (aplastado)
        {
            animator.SetTrigger("Aplastado");
            StartCoroutine(ActivarAnimacionDeMuerte(0.25f, 0.25f));
        }
        else
        {
            StartCoroutine(ActivarAnimacionDeMuerte(0f, 0.25f));
        }


    }

    protected IEnumerator ActivarAnimacionDeMuerte(float t, float t2)
    {
        yield return new WaitForSeconds(t);
        animator.SetBool("Muerto", true);
        spMuerte.PlayEffect();
        yield return new WaitForSeconds(t2);
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DanoAEnemigos"))
        {
            if (aplastable)
            {
                DanarEnemigo();
                if (vida <= 0)
                {
                    aplastado = true;
                    Morir();
                }
                spSalto.PlayEffect();
                other.GetComponentInParent<ControladorDeJugador>().RebotarEnEnemigo();
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
        if (other.CompareTag("Plataformas") || other.CompareTag("Enemy"))
        {
            direccionDeMovimiento = -direccionDeMovimiento;
        }
    }
}
