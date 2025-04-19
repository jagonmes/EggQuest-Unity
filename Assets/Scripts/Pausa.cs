using UnityEngine;
using UnityEngine.InputSystem;

public class Pausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private PlayerInput playerInput;
    
    void Start()
    {
        playerInput.enabled = true;
    }
    
    public void StartButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            bool menuActivo = false;
            foreach (GameObject menu in menus)
            {
                if (menu.activeSelf)
                {
                    menuActivo = true;
                    break;
                }
            }
            if (!menuPausa.activeSelf && !menuActivo)
            {
                Time.timeScale = 0;
                menuPausa.SetActive(true);
                FindFirstObjectByType<OptionSound>()?.PlayEffect();
            }
        }
    }
}
