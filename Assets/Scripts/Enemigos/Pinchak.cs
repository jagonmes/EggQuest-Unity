using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pinchak : Enemigo
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    private float velocidadDeMovimientoInicial = 0;
    public bool atacando = false;
    [SerializeField]private ReboteEspada reboteEspada;
    [SerializeField]private GameObject zonaDano;
    [SerializeField]private Collider2D colliderDano;
    [SerializeField]private SoundPlayer spMuerte;
    
    protected override void Start()
    {
        base.Start();
        if(direccionDeMovimiento > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        velocidadDeMovimientoInicial = velocidadDeMovimiento;
        StartCoroutine(IntentarAtacar());
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
        }
        if (other.CompareTag("Espada"))
        {
            if (!atacando)
            {
                DanarEnemigo();
                if (vida <= 0)
                {
                    Morir();
                }
            }
        }
        if (other.CompareTag("Plataformas") || other.CompareTag("Enemy"))
        {
            direccionDeMovimiento = -direccionDeMovimiento;
            if(direccionDeMovimiento > 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }
    
    
    
    protected IEnumerator IntentarAtacar()
    {
        yield return new WaitForSeconds(3f);
        int random = Random.Range(0, 100);
        if(random < 50)
            StartCoroutine(IntentarAtacar());
        else
            StartCoroutine(Atacar());
        
    }

    protected IEnumerator Atacar()
    {
        atacando = true;
        animator.SetBool("Atacando", true);
        velocidadDeMovimiento = 0;
        zonaDano.SetActive(true);
        reboteEspada.reboteHotizontal = true;
        yield return new WaitForSeconds(3f);
        DejarDeAtacar();
    }
    
    protected void DejarDeAtacar()
    {
        atacando = false;
        animator.SetBool("Atacando", false);
        zonaDano.SetActive(false);
        reboteEspada.reboteHotizontal = false;
        velocidadDeMovimiento = velocidadDeMovimientoInicial;
        StartCoroutine(IntentarAtacar());
    }
    
    protected override void Morir()
    {
        base.Morir();
        colliderDano.enabled = false;
        StartCoroutine(ActivarAnimacionDeMuerte(0f, 0.25f));
    }

    protected IEnumerator ActivarAnimacionDeMuerte(float t, float t2)
    {
        yield return new WaitForSeconds(t);
        animator.SetTrigger("Muerto");
        spMuerte.PlayEffect();
        yield return new WaitForSeconds(t2);
        Destroy(gameObject);
    }
}
