using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    
    public Canvas screenCanvas;
    public GameObject pauseMenu, playerHUD;
    public PlayerInput controls;

    void Start()
    {
        pauseMenu.SetActive(false);
        controls.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;
    }

    public void TogglePauseMenu()
    {
        if(pauseMenu.activeSelf)
        {
            controls.SwitchCurrentActionMap("Player");
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            playerHUD.SetActive(true);
        }
        else
        {
            controls.SwitchCurrentActionMap("Pause");
            DisableCanvasChildren();
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void DisableCanvasChildren()
    {
        foreach (Transform child in screenCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}