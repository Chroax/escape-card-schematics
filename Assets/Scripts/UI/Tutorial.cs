using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject nextPanel;
    public GameObject currentPanel;
    public GameObject beforePanel;

    public void next(){
        nextPanel.SetActive(true);
        currentPanel.SetActive(false);
        beforePanel.SetActive(false);
    }
    public void before(){
        nextPanel.SetActive(false);
        currentPanel.SetActive(false);
        beforePanel.SetActive(true);
    }

    public void SkipTutotrial(){
        SceneManager.LoadScene("Tutorial");
    }
}
