using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadPreferences : MonoBehaviour
{
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private PlayMenuController playMenuController;
    [SerializeField] private GraphicsMenuController graphicsMenuController;
    [SerializeField] private SoundSettingsController soundSettingsController;

    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessTextValue;

    [SerializeField] private TMP_Dropdown _qualityDropDown;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("masterVoluime"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                soundSettingsController.ResetVolumeButton("Audio");
            }
            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                _qualityDropDown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
                //TODO change game brightness with post processing
            }
        }
    }
}
