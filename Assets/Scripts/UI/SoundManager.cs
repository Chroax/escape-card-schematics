using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public void hoverSoundPlay()
    {
        hoverSound.Play();
    }

    public void clickSoundPlay()
    {
        clickSound.Play();
    }
}
