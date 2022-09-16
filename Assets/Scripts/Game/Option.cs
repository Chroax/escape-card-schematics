using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public AudioMixer audioMixerMusic;
    public AudioMixer audioMixerSFX;
    private float value;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        audioMixerMusic.GetFloat("volume", out float music);
        audioMixerSFX.GetFloat("volume", out float effect);

        musicSlider.value = music;
        sfxSlider.value = effect;
    }
    public void setVolume(float volume)
    {
        audioMixerMusic.SetFloat("volume", volume);
        if(musicSlider.value == -20)
        {
            audioMixerMusic.SetFloat("volume", -80);
        }
    }

    public void setSFX(float volume)
    {
        audioMixerSFX.SetFloat("volume", volume);
        if (sfxSlider.value == -20)
        {
            audioMixerSFX.SetFloat("volume", -80);
        }
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}