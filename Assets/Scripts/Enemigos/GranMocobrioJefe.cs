using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class GranMocobrioJefe : Enemigo
{

    [SerializeField]private bool aplastado = false;
    [SerializeField]private bool atacando = false;
    [SerializeField]private bool enfadado = false;
    [SerializeField]private bool enElSuelo = true;
    [SerializeField]private bool muerto = false;
    [SerializeField]private Collider2D colliderDano;
    [SerializeField] private SoundPlayer spMuerte;
    [SerializeField] private SoundPlayer spSalto;
    [SerializeField]private Vector2 boxSize;
    [SerializeField]private float distanciaDelCentro = 0.0f;
    [SerializeField]private LayerMask capaPlataformas;
    [SerializeField]private GameObject player;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private float tiempoEnElAire = 5.0f;
    [SerializeField] private GameObject barrera;
    
    
    protected override void Move()
    {
        if (atacando && EnPlataforma())
        {
            animator.SetBool("EnElSuelo", true);
            atacando = false;
            rb.linearVelocityX = velocidadDeMovimiento * direccionDeMovimiento;
        }
        else
        {
            animator.SetBool("EnElSuelo", false);
        }

        if (rb != null)
        {
            if(!aplastado && !atacando && !enfadado && enElSuelo)
                rb.linearVelocityX = velocidadDeMovimiento * direccionDeMovimiento;
            else if(aplastado)
                rb.linearVelocityX = 0;
        }

        if (enfadado || atacando)
        {
            if (player != null)
            {
                if(player.transform.position.x > this.transform.position.x)
                    spriteRenderer.flipX = false;
                else
                    spriteRenderer.flipX = true;
            }
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
    protected override void Morir()
    {
        base.Morir();
        if (aplastado)
        {
            StartCoroutine(ActivarAnimacionDeMuerte(0.25f, 1.80f));
        }
        else
        {
            StartCoroutine(ActivarAnimacionDeMuerte(0f, 1.80f));
        }
    }

    protected IEnumerator ActivarAnimacionDeMuerte(float t, float t2)
    {
        yield return new WaitForSeconds(t);
        animator.SetTrigger("Muerto");
        spMuerte.PlayEffect();
        spMuerte.SetLoop(true);
        yield return new WaitForSeconds(t2);
        if(barrera != null)
            Destroy(barrera);
        Destroy(gameObject);
        spMuerte.StopEffect();
        spMuerte.SetLoop(false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DanoAEnemigos"))
        {
            if (aplastable && !muerto)
            {
                if (!enfadado && !atacando)
                {
                    DanarEnemigo();
                    aplastado = true;
                    animator.SetTrigger("Aplastado");
                    animator.SetBool("Aplastado 0", true);
                }
                if (vida <= 0)
                {
                    Morir();
                    muerto = true;
                    colliderDano.enabled = false;
                }
                else
                {
                    StartCoroutine(QuitarAplastado()); 
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
            if (!enfadado)
            {
                DanarEnemigo();
                spMuerte.PlayEffect();
            }
            if (vida <= 0)
            {
                Morir();
                muerto = true;
                colliderDano.enabled = false;
            }
            else if(!enfadado && !atacando)
            {
                StartCoroutine(Enfadado());
            }
            
        }
        if (other.CompareTag("Plataformas") || other.CompareTag("Enemy")  || other.CompareTag("EnemieStopper"))
        {
            direccionDeMovimiento = -direccionDeMovimiento;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - (this.transform.up) * distanciaDelCentro, boxSize);
    }

    public bool EnPlataforma()
    {
        
        if (Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, distanciaDelCentro, capaPlataformas))
        {
            return true;
        }
        else
            return false;
    }
    
    protected IEnumerator QuitarAplastado()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Aplastado 0", false);
        aplastado = false;
        StartCoroutine(Enfadado());
    }
    
    protected IEnumerator Enfadado()
    {
        if (!enfadado && !atacando)
        {
            animator.SetTrigger("Enfadado");
            enfadado = true;
            yield return new WaitForSeconds(0.75f);
            StartCoroutine(Atacar());
        }
    }
    
    protected IEnumerator Atacar()
    {
        if (!atacando && enfadado)
        {
            animator.SetTrigger("Atacando");
            SaltarHaciaElJugador();
            yield return new WaitForSeconds(0.1f);
            atacando = true;
            yield return new WaitForSeconds(0.75f);
            enfadado = false;
        }
    }
    
    private void SaltarHaciaElJugador()
    {
        if (player != null)
        {
            if(player.transform.position.x > this.transform.position.x)
                direccionDeMovimiento = 1;
            else
                direccionDeMovimiento = -1;
            Vector2 velocidad = new Vector2(Mathf.Clamp(Mathf.Abs(this.transform.position.x - player.transform.position.x), 80, 999) * direccionDeMovimiento, rb.gravityScale * tiempoEnElAire);
            rb.linearVelocity = velocidad;
        }
    }
}
