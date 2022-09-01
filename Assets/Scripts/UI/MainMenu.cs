using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject logo;
    public TextMeshProUGUI text;
    public GameObject panel;
    public GameObject mainMenu;

    public void HoverText()
    {
        text.fontSize = 75;
        logo.SetActive(true);
    }

    public void ExitHoverText()
    {
        text.fontSize = 64;
        logo.SetActive(false);
    }

    public void GoToPlayGames()
    {
        SceneManager.LoadScene("PlayGames");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PanelPopUp()
    {
        panel.SetActive(true);
        logo.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Login");
    }

    public void BackFromPanel()
    {
        logo.SetActive(false);
        panel.SetActive(false);
        mainMenu.SetActive(true);
    }

}
