using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().loseSoundPlay();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
