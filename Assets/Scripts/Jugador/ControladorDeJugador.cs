using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ControladorDeJugador : MonoBehaviour
{
    public Rigidbody2D rb;
    
    
    public float velocidadDeMovimiento = 50f;
    public float potenciaDeSalto = 50f;
    public float potenciaDeKnockBack = 50f;
    public bool enKnockBack = false;
    
    [SerializeField]private GameObject zonaDeDanoEnemigos;
    [SerializeField]private EntradaDelJugador entradaDelJugador;
    [SerializeField]private Vector2 boxSize;
    [SerializeField]private float distanciaDelCentro = 0.0f;
    [SerializeField]private LayerMask capaPlataformas;
    public Animator animator;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private ControladorVida controladorVida;
    [SerializeField]private SoundPlayer spCaminar;
    [SerializeField]private SoundPlayer spSaltar;
    [SerializeField]private SoundPlayer spMorir;
    [SerializeField]private SoundPlayer spRebotar;
    [SerializeField] private bool puedeAtacar = false;
    [SerializeField] private bool atacando = false;
    [SerializeField] private GameObject espada;
    [SerializeField] private GameObject espadaAbajo;
    public Transform respawnPoint = null;
    
    private Vector3 ultimaPosicionEnElSuelo = new Vector3(0.0f, 0.0f, 0.0f);
    private bool _cayendo = false;
    private bool cayendo = false;
    private bool cayendoAlVacio = false;
    [SerializeField]private float _jumpTimer = 0.2f;
    [SerializeField]private float jumpTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        SoundPlayers();
        if (controladorVida.muerto)
            return;
        MoverAlJugador();
        SetAnimatorParams();
        FlipSprites();
        if(!JugadorEnPlataforma())
            jumpTimer -= Time.deltaTime;
        else
            jumpTimer = _jumpTimer;
    }

    void SoundPlayers()
    {
        if (controladorVida.muerto)
        {
            spCaminar.StopEffect();
            spSaltar.StopEffect();
            return;
        }
        if (entradaDelJugador.xAxis != 0 && JugadorEnPlataforma() && Time.timeScale != 0)
        {
            if (!spCaminar.playing)
            {
                spCaminar.PlayEffect();
                spCaminar.SetLoop(true);
            }
        }
        else
        {
            if (spCaminar.playing)
            {
                spCaminar.StopEffect();
                spCaminar.SetLoop(false);
            }
        }
        bool jugadorEnPlataforma = JugadorEnPlataforma();
        cayendo = !jugadorEnPlataforma && rb.linearVelocity.y < 0;
        if (cayendo != _cayendo)
        {
            if (_cayendo)
            {
                if(!cayendoAlVacio)
                    spSaltar.PlayEffect();
            }

            _cayendo = cayendo;
        }
        if(cayendo && !enKnockBack)
            zonaDeDanoEnemigos.SetActive(true);
        else
            zonaDeDanoEnemigos.SetActive(false);
    }

    void MoverAlJugador()
    {
        if (!enKnockBack)
        {
            rb.linearVelocityX = velocidadDeMovimiento * entradaDelJugador.xAxis;
        }
    }
    
    public virtual void Saltar(InputAction.CallbackContext context)
    {
        if (context.started && (JugadorEnPlataforma() || jumpTimer > 0) && Time.timeScale != 0 && !enKnockBack && !controladorVida.muerto)
        {
            rb.linearVelocityY = potenciaDeSalto;
            spSaltar.PlayEffect();
        }
    }

    private void SetAnimatorParams()
    {
        animator.SetFloat("VelocidadDeMovimiento", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("VelocidadVertical", rb.linearVelocityY);
        animator.SetBool("EnElSuelo", JugadorEnPlataforma());
        animator.SetBool("Invencible", controladorVida.invulnerableAnim);
    }

    private void FlipSprites()
    {
        if (atacando)
            return;
        if(rb.linearVelocityX < 0)
            spriteRenderer.flipX = true;
        else if(rb.linearVelocityX > 0)
            spriteRenderer.flipX = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - (this.transform.up) * distanciaDelCentro, boxSize);
    }

    public bool JugadorEnPlataforma()
    {
        RaycastHit2D other = Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, distanciaDelCentro, capaPlataformas);  
        if (other && Mathf.Abs(rb.linearVelocityY) < 0.3f)
        {

                if(!cayendoAlVacio)
                    ultimaPosicionEnElSuelo = transform.position;
                jumpTimer = _jumpTimer;
                return true;

        }
        else
            return false;
    }
    
    public void JugadorCaido()
    {
        cayendoAlVacio = true;
        StartCoroutine(devolverAlJugadorAlSuelo(1f));
    }
    
    public void MatarAlJugador()
    {
        rb.bodyType = RigidbodyType2D.Static;
        spMorir.PlayEffect();
        SoundManager.Instance.StopMusic();
        animator.SetBool("MuertoBool", true);
        if (espada != null && espadaAbajo != null)
        {
            espada.SetActive(false);
            espadaAbajo.SetActive(false);
        }

        if (respawnPoint != null)
            StartCoroutine(reiniciarEnPuntoDeRespawn(spMorir.source.length + 0.25f));
        else
            StartCoroutine(reiniciarEscena(spMorir.source.length + 0.25f));
    }

    public void RebotarEnEnemigo()
    {
        rb.linearVelocityY = potenciaDeSalto*2/3;
        spSaltar.PlayEffect();
    }

    public void KnockBack(float tiempo, Vector2 collisionPosition)
    {
        if (enKnockBack)
            return;
        enKnockBack = true;
        Vector2 direccion = new Vector2((this.transform.position.x - collisionPosition.x), (this.transform.position.y - collisionPosition.y)).normalized;
        rb.linearVelocity = direccion * potenciaDeKnockBack;
        StartCoroutine(desactivarKnockBack(tiempo));
        if (espada != null && espadaAbajo != null)
        {
            espada.SetActive(false);
            espadaAbajo.SetActive(false);
            animator.SetBool("Atacando", false);
        }
    }

    public IEnumerator desactivarKnockBack(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        enKnockBack = false;
    }
    
    public IEnumerator devolverAlJugadorAlSuelo(float tiempo)
    {
        var BaseCamera = GameObject.Find("Base Camera");
        BaseCamera.GetComponent<SeguirAlJugador>().jugadorEncontrado = false;
        controladorVida.DanoPorCaida();
        yield return new WaitForSeconds(tiempo);
        cayendoAlVacio = false;
        if (!controladorVida.muerto)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = ultimaPosicionEnElSuelo;
            BaseCamera.GetComponent<SeguirAlJugador>().jugadorEncontrado = true;
        }
    }
    
    public IEnumerator reiniciarEnPuntoDeRespawn(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, this.transform.position.z);
        animator.SetBool("MuertoBool", false);
        SoundManager.Instance.ResumeMusic();
        controladorVida.ReiniciarVida();
        var BaseCamera = GameObject.Find("Base Camera");
        BaseCamera.GetComponent<SeguirAlJugador>().jugadorEncontrado = true;
        enKnockBack = false;
        cayendoAlVacio = false;
        atacando = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("EnElSuelo", true);
        animator.SetBool("Muerto", false);
        animator.SetBool("RecibirDano", false);
    }
    
    public IEnumerator reiniciarEscena(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Atacar(InputAction.CallbackContext context)
    {
        if (!puedeAtacar)
            return;
        if (context.started && Time.timeScale != 0 && !enKnockBack && !controladorVida.muerto && !atacando)
        {
            animator.SetBool("Atacando", true);
            if(entradaDelJugador.yAxis < 0  && Mathf.Abs(rb.linearVelocity.y) > 0.1f)
            {
                animator.SetTrigger("AtacarAbajo");
                animator.Play("Atacar Abajo");
                espadaAbajo.SetActive(true);
            }
            else
            {
                animator.SetTrigger("Atacar");
                espada.SetActive(true);
                if (spriteRenderer.flipX)
                {
                    espada.transform.localPosition = new Vector3(-32,espada.transform.localPosition.y,espada.transform.localPosition.z);
                    espada.GetComponent<BoxCollider2D>().offset = new Vector2(10, 0);
                    espada.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    espada.transform.localPosition = new Vector3(32,espada.transform.localPosition.y,espada.transform.localPosition.z);
                    espada.GetComponent<BoxCollider2D>().offset = new Vector2(-10, 0);
                    espada.GetComponent<SpriteRenderer>().flipX = false;
                }

            }
            atacando = true;
            StartCoroutine(dejarDeAtacar(0.25f));
        }

    }
    
    public IEnumerator dejarDeAtacar(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        if (espada != null && espadaAbajo != null)
        {
            espada.SetActive(false);
            espadaAbajo.SetActive(false);
            animator.SetBool("Atacando", false);
        }
        yield return new WaitForSeconds(0.25f);
        atacando = false;
    }
    
    public void ReboteEspada(Vector2 collisionPosition)
    {
        Vector2 direccion = new Vector2((this.transform.position.x - collisionPosition.x), (this.transform.position.y - collisionPosition.y)).normalized;
        if(direccion.x >= 0)
            rb.linearVelocityX = potenciaDeKnockBack/2;
        else
            rb.linearVelocityX = -potenciaDeKnockBack/2;
        enKnockBack = true;
        StartCoroutine(desactivarKnockBack(0.125f));
        spRebotar.PlayEffect();
    }
    public void ReboteEspadaVertical()
    {
        rb.linearVelocityY = potenciaDeSalto;
        spRebotar.PlayEffect();
    }
    
    private bool aproximar(float a, float b, float rango)
    {
        if(b + rango > a && b - rango < a)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
