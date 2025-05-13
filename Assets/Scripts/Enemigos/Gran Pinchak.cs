using System.Collections;
using UnityEngine;

public class GranPinchak : Enemigo
{

    [SerializeField]private bool aturdido = false;
    [SerializeField]private bool atacando = false;
    [SerializeField]private bool enfadado = false;
    [SerializeField]private bool muerto = false;
    [SerializeField]private Collider2D colliderDano;
    [SerializeField] private SoundPlayer spMuerte;
    [SerializeField] private SoundPlayer spReboteEnemigo;
    [SerializeField]private GameObject player;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject cinematica;
    [SerializeField]private Transform spriteTransform;
    public ReboteEspada reboteEspada;
    [SerializeField]private Transform jugador;
    public float distanciaAJugador = 0.0f;
    
    
    protected override void Move()
    {
        if (rb != null && !aturdido && !enfadado && !muerto)
        {
            if (atacando)
            {
                rb.linearVelocityX = velocidadDeMovimiento * direccionDeMovimiento * 1.5f;
                Quaternion currentRotation = spriteTransform.transform.rotation;
                Quaternion rotationToApply = Quaternion.AngleAxis(-360*Time.deltaTime * direccionDeMovimiento, new Vector3(0,0,1));
                currentRotation *= rotationToApply;
                spriteTransform.transform.rotation = currentRotation;;
            }
            else
            {
                rb.linearVelocityX = velocidadDeMovimiento * direccionDeMovimiento;
                spriteTransform.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }

        
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(IntentarAtacar());
    }

    protected override void Morir()
    {
        StopAllCoroutines();
        base.Morir();
        colliderDano.enabled = false;
        StartCoroutine(ActivarAnimacionDeMuerte(0f, 1.80f));
    }

    protected IEnumerator ActivarAnimacionDeMuerte(float t, float t2)
    {
        yield return new WaitForSeconds(t);
        animator.SetTrigger("Morir");
        spMuerte.PlayEffect();
        spMuerte.SetLoop(true);
        yield return new WaitForSeconds(t2);
        if(cinematica != null)
            cinematica.SetActive(true);
        Destroy(gameObject);
        spMuerte.StopEffect();
        spMuerte.SetLoop(false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControladorVida>().RecibirDano(other.ClosestPoint(new Vector2(transform.position.x, transform.position.y)));
        }
        if (other.CompareTag("Espada"))
        {
            if (aturdido)
            {
                DanarEnemigo();
                if (vida <= 0)
                {
                    Morir();
                }
            }
            else if(!enfadado && !atacando && !muerto)
            {
                StopAllCoroutines();
                StartCoroutine(Enfadarse());
            }
        }
        if (other.CompareTag("Plataformas") || other.CompareTag("Enemy")  || other.CompareTag("EnemieStopper"))
        {
            direccionDeMovimiento = -direccionDeMovimiento;
            if(direccionDeMovimiento > 0)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        if(other.CompareTag("Plataformas") && atacando)
            StartCoroutine(Aturdido());
    }
    
    protected IEnumerator IntentarAtacar()
    {
        yield return new WaitForSeconds(3f);
        if (!enfadado && !atacando && !aturdido)
        {
            int random = Random.Range(0, 100);
            distanciaAJugador = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y),
                new Vector2(this.transform.position.x, this.transform.position.y));
            if (random < 50 || distanciaAJugador  > 200f)
                StartCoroutine(IntentarAtacar());
            else
                StartCoroutine(Enfadarse());
        }
    }

    protected IEnumerator Enfadarse()
    {
        if (!enfadado)
        {
            enfadado = true;
            animator.SetTrigger("Enfadado");
            yield return new WaitForSeconds(1f);
            Atacar();
        }
}

    protected void Atacar()
    {
        enfadado = false;
        atacando = true;
        if(player.transform.position.x > this.transform.position.x)
        direccionDeMovimiento = 1;
        else
        {
            direccionDeMovimiento = -1;
        }
        animator.SetTrigger("Atacar");
    }
    
    protected IEnumerator Aturdido()
    {
        spReboteEnemigo.PlayEffect();
        aturdido = true;
        atacando = false;
        animator.SetTrigger("Aturdirse");
        animator.ResetTrigger("Atacar");
        animator.ResetTrigger("Enfadado");
        animator.ResetTrigger("IdleT");
        spriteTransform.transform.rotation = Quaternion.Euler(0,0,0);
        KnockBack();
        yield return new WaitForSeconds(2f);
        aturdido = false;
        animator.SetTrigger("IdleT");
        animator.ResetTrigger("Aturdirse");
        StartCoroutine(IntentarAtacar());
    }
    
    public void KnockBack()
    {
        rb.linearVelocity = 150 * new Vector2(0.5f * direccionDeMovimiento,0.5f) ;
        StartCoroutine(desactivarKnockBack());
    }
    
    public IEnumerator desactivarKnockBack()
    {
        yield return new WaitForSeconds(2f);
        if(!muerto)
            if(rb != null)
                rb.linearVelocityX = 0;
    }
    
    protected override void DanarEnemigo()
    {
        spMuerte.StopEffect();
        spMuerte.PlayEffect();
        vida--;
    }



}
