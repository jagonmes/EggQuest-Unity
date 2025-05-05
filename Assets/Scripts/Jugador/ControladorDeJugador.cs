using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControladorDeJugador : MonoBehaviour
{
    public Rigidbody2D rb;
    
    
    public float velocidadDeMovimiento = 50f;
    public float potenciaDeSalto = 50f;
    public float potenciaDeKnockBack = 50f;
    public bool enKnockBack = false;
    
    [SerializeField]private EntradaDelJugador entradaDelJugador;
    [SerializeField]private Vector2 boxSize;
    [SerializeField]private float distanciaDelCentro = 0.0f;
    [SerializeField]private LayerMask capaSuelo;
    public Animator animator;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private ControladorVida controladorVida;
    [SerializeField]private SoundPlayer spCaminar;
    [SerializeField]private SoundPlayer spSaltar;
    [SerializeField]private SoundPlayer spMorir;
    
    private Vector3 ultimaPosicionEnElSuelo = new Vector3(0.0f, 0.0f, 0.0f);
    private bool _cayendo = false;
    private bool cayendo = false;
    private bool cayendoAlVacio = false;


    // Update is called once per frame
    void Update()
    {
        SoundPlayers();
        if (controladorVida.muerto)
            return;
        MoverAlJugador();
        SetAnimatorParams();
        FlipSprites();
    }

    void SoundPlayers()
    {
        if (controladorVida.muerto)
        {
            spCaminar.StopEffect();
            spSaltar.StopEffect();
            return;
        }
        if (entradaDelJugador.xAxis != 0 && JugadorEnPlataforma())
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
            if(_cayendo && !cayendoAlVacio)
                spSaltar.PlayEffect();
            _cayendo = cayendo;
        }

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
        if (context.started && JugadorEnPlataforma() && Time.timeScale != 0 && !enKnockBack && !controladorVida.muerto)
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
        
        if (Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, distanciaDelCentro, capaSuelo))
        {
            if(!cayendoAlVacio)
                ultimaPosicionEnElSuelo = transform.position;
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
        StartCoroutine(reiniciarEscena(spMorir.source.length));
    }

    public void KnockBack(float tiempo, Vector2 collisionPosition)
    {
        if (enKnockBack)
            return;
        enKnockBack = true;
        Vector2 direccion = new Vector2((this.transform.position.x - collisionPosition.x), (this.transform.position.y - collisionPosition.y)).normalized;
        rb.linearVelocity = direccion * potenciaDeKnockBack;
        StartCoroutine(desactivarKnockBack(tiempo));
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
    
    public IEnumerator reiniciarEscena(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
