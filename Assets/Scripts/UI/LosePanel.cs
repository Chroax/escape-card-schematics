using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().loseSoundPlay();
    }
}
