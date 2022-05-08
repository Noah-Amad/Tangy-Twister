using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSaveControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] TextMeshProUGUI volumeTextUI = null;

    private void Start()
    {
        LoadValues();
    }


    public void VolumeSlider(float volume)
    {
        volume = volume * 100;
        volumeTextUI.text = volume.ToString("0");
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
