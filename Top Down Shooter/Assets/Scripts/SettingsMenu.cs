﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Volume Settings"), SerializeField]
    private Slider masterVolumeSlider;

    [SerializeField] private Slider fXVolumeSlider;

    [SerializeField] private Slider musicVolumeSlider;


    [Header("Graphics Settings"), SerializeField]
    private Dropdown resolutionDropdown;

    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private Dropdown qualityDropdown;

    [Header("Apply button"), SerializeField]
    private Button applyButton;

    private void Awake()
    {
        //Build Resolutions
        resolutionDropdown.ClearOptions();
        var resolutions = new List<string>();

        foreach (var resolution in Screen.resolutions)
        {
            resolutions.Add($"{resolution.width} x {resolution.height}");
        }

        resolutionDropdown.AddOptions(resolutions);

        //Build Quality levels
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
    }

    private void OnEnable()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        fXVolumeSlider.value = PlayerPrefs.GetFloat("FX Volume", fXVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        applyButton.interactable = false;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("FX Volume", fXVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);

        // TODO: apply audio settings to mixers

        var value = resolutionDropdown.value;
        Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, fullscreenToggle);
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }
}