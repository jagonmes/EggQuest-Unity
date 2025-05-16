using System.Collections;
using UnityEngine;

public class MenuPrincipal : GrupoDeOpciones
{
    protected override void Start()
    {
        StartCoroutine(activarMenu());
        optionSound = FindFirstObjectByType<OptionSound>();
        RecolocarFlecha();
    }

    public IEnumerator activarMenu()
    {
        yield return new WaitForSeconds(0.1f);
        playerInput.enabled = true;
    }
}
