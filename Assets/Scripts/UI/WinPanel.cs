using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().winSoundPlay();
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
