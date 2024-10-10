using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider voiceSlider;
    public void Start() {
        this.gameObject.SetActive(false);
        musicSlider.value = 0.4f;
        OnSliderMusicChanged(0.4f);

        musicSlider.onValueChanged.AddListener(OnSliderMusicChanged);
        soundSlider.onValueChanged.AddListener(OnSliderSoundChanged);
        voiceSlider.onValueChanged.AddListener(OnSliderVoiceChanged);
    }
    public void toggleSettingsWindow() {
        if (gameObject.activeInHierarchy) {
            closeSettingsWindow();
        } else {
            openSettingsWindow();
        }
    }
    public void openSettingsWindow() {
        this.gameObject.SetActive(true);
    }
    public void closeSettingsWindow() {
        this.gameObject.SetActive(false);
    }
    public void OnSliderMusicChanged(float value) {
        AudioManager.instance.masterVolume = value;
        AudioManager.instance.changeVolume();
    }

    public void OnSliderSoundChanged(float value) {
        SoundManager.instance.audioSource.volume = value;
    }
    public void OnSliderVoiceChanged(float value) {
        MonologueManager.instance.masterVolume = value;
        MonologueManager.instance.changeVolume();
    }
}
