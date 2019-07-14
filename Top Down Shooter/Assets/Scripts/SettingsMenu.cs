using System;
using System.Collections.Generic;
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

    [SerializeField] private AudioMixer audioMixer;


    [Header("Graphics Settings"), SerializeField]
    private Dropdown resolutionDropdown;

    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private Dropdown qualityDropdown;

    [Header("Apply button"), SerializeField]
    private Button applyButton;

    private List<string> resolutions;

    private void Awake()
    {
        //Build Resolutions
        resolutionDropdown.ClearOptions();
        resolutions = new List<string>();

        foreach (var resolution in Screen.resolutions)
        {
            resolutions.Add($"{resolution.width} x {resolution.height}");
        }

        resolutionDropdown.AddOptions(resolutions);

        //Build Quality levels
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());

        //Set Audio levels
        audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume", 0f));
        audioMixer.SetFloat("FX Volume", PlayerPrefs.GetFloat("FX Volume", 0f));
        audioMixer.SetFloat("Music Volume", PlayerPrefs.GetFloat("Music Volume", 0f));
    }

    private void OnEnable()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        fXVolumeSlider.value = PlayerPrefs.GetFloat("FX Volume", fXVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        applyButton.interactable = false;

        var currentResolutionIndex =
            resolutions.IndexOf($"{Screen.currentResolution.width} x {Screen.currentResolution.height}");

        resolutionDropdown.value = currentResolutionIndex != 1 ? currentResolutionIndex : 0;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("FX Volume", fXVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);

        audioMixer.SetFloat("Master Volume", masterVolumeSlider.value);
        audioMixer.SetFloat("FX Volume", fXVolumeSlider.value);
        audioMixer.SetFloat("Music Volume", musicVolumeSlider.value);

        var value = resolutionDropdown.value;
        Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, fullscreenToggle);
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }
}