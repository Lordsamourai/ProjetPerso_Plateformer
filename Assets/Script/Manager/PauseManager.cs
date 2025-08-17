using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public GameObject optionsPanel;
    public GameObject pausePanel;

    public bool isPaused { get; private set; } = false; // Ajout pour savoir si le jeu est en pause

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        HidePause();
        HideOptions();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void HideOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ShowPause()
    {
        pausePanel.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HidePause()
    {
        pausePanel.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
