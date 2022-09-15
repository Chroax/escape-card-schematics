using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().winSoundPlay();
    }
}
