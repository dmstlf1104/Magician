using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioContorller : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider BGMSlider;
    public Slider SFXSlider;
    // Start is called before the first frame update

    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    void Start()
    {

    }
    public void SetBGMVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
