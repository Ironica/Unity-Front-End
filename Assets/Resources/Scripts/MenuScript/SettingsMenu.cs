using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMix;
    public Dropdown resDropDown;
    
    Resolution[] reso;
    
    public void Start()
    {
        reso = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentIndexRes = 0;
        for (int i = 0; i < reso.Length; i++)
        {
            string opt = reso[i].width + " x " + reso[i].height;
            options.Add(opt);

            if (reso[i].width == Screen.width && reso[i].height == Screen.height)
            {
                currentIndexRes = i;
            }
        }
        
        resDropDown.AddOptions(options);
        resDropDown.value = currentIndexRes;
        resDropDown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMix.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int indexRes)
    {
        Resolution resTmp = reso[indexRes];
        Screen.SetResolution(resTmp.width,resTmp.height,Screen.fullScreen);
    }
}
