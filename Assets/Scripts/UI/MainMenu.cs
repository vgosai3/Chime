using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        // Initialize controls with current settings
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
        float f = 0;        
        if (audioMixer.GetFloat("volume", out f)) {
            volumeSlider.value = f;
        }
        volumeSlider.value = f;
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    //Play button functionality take to level 1
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level");
        // Reset timescale if returned from pause menu
        Time.timeScale = 1f;
    }

    //Play button functionality take to level 1
    public void PlayWithSave()
    { 
        Globals.LoadSave();
        PlayGame();
    }

    //Quit button functionality
    public void QuitGame()
    {
        Application.Quit();
    }

    // Volume slider 
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    // Graphics dropdown
    public void SetQuality(int index)
    {
        Debug.Log(QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(index);
    }

    // Toggle fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Update resolution from dropdown
    public void SetResolution(int index) {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }
}