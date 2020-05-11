using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq; 

public class OptionsMenu : MonoBehaviour
{   
    Resolution[] resolutions; 
    public AudioMixer AudioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
       
        List<string> options  = new List<string>();
        
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option); 

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)

                {
                    currentResolutionIndex = i;
                }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution Resolution = resolutions[resolutionIndex];
        Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);
    }
    public void SetVolume (float Volume)
    {
      AudioMixer.SetFloat("Volume", Mathf.Log10(Volume)* 20);
    }
    public void SetQuality (int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;  
    }
}

