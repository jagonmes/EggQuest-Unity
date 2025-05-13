using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public GameObject cortina;
    public GameObject graciasPorJugar;
    void Start()
    {
        StartCoroutine(Fin());
    }

    protected IEnumerator Fin()
    {
        yield return new WaitForSeconds(10f);
        cortina.SetActive(true);
        yield return new WaitForSeconds(2f);
        graciasPorJugar.SetActive(true);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Menu Principal");
    }
}
