using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject logo;
    public TextMeshProUGUI text;
    public GameObject panel;
    public GameObject mainMenu;
    public LogoutScript logoutScript;

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
        if(!DBManager.isTutorial)
            SceneManager.LoadScene("Intro Scene");
    }

    public void GoToTutorial()
    {
        if (DBManager.isTutorial)
            SceneManager.LoadScene("Tutorial Study");
    }

    public void PanelPopUp()
    {
        panel.SetActive(true);
    }

    public void QuitGame()
    {
        //handle logout
        logoutScript.CallLogout();
    }

    public void BackFromPanel()
    {
        logo.SetActive(false);
        panel.SetActive(false);
        mainMenu.SetActive(true);
    }

}
