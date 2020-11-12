using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider soundSlider;

    private void OnEnable()
    {
        float val;
        mixer.GetFloat("BGM", out val);

        musicSlider.value = val;

        float val2;
        mixer.GetFloat("SFX", out val2);

        soundSlider.value = val2;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetValue(float sliderValue) 
    {

    }

    public void SetMusicVolume(Slider vol)
    {
        mixer.SetFloat("BGM", vol.value);
    }

    public void SetSoundVolume(Slider vol)
    {
        mixer.SetFloat("SFX", vol.value);
    }
}
