using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject levelsPanel;
    void Start()
    {
        HideMainMenu();
        HideOptions();
        HideLevels();
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }
    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void HideOptions()
    {
        optionsPanel.SetActive(false);
    }
    public void ShowLevels()
    {
        levelsPanel.SetActive(true);
    }
    public void HideLevels()
    {
        levelsPanel.SetActive(false);
    }
    public void BackButton()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void PlayButton()
    {
        ShowLevels();
        //SceneManager.LoadScene("Niveau1");
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}

