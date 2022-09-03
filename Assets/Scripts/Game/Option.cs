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
    public Slider brightness;

    void Start()
    {

        audioMixerMusic.GetFloat("musicVolume", out float music);
        audioMixerSFX.GetFloat("effectVolume", out float effect);

        brightness.value = Screen.brightness;

        musicSlider.value = music;
        sfxSlider.value = effect;
    }
    public void setVolume(float volume)
    {
        audioMixerMusic.SetFloat("volume", volume);
    }

    public void setSFX(float volume)
    {
        audioMixerSFX.SetFloat("volume", volume);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setBrigthness(float brightness)
    {
        Screen.brightness = brightness;
        Debug.Log("Changed");
    }
}