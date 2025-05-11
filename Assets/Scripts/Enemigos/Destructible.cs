using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected new Collider2D[] collider2D;
    [SerializeField] protected SoundPlayer sp;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Espada"))
        {
            StartCoroutine(destruir());
        }
    }
    
    protected virtual IEnumerator destruir()
    {
        if(explosionPrefab != null)
            explosionPrefab.SetActive(true);
        sp.PlayEffect();
        spriteRenderer.enabled = false;
        foreach (Collider2D col in collider2D)
        {
            col.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
