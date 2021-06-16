using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.SettingScripts
{
    public class Graphics: MonoBehaviour
    {
        public Resolution[] resolutions;
        public TMP_Dropdown resolutionDropdown;
        
        void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            var currentResolutionIndex = 0;
            resolutionDropdown.AddOptions(resolutions.Select((e, i) =>
            {
                if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
                return $"{e.width} x {e.height}";
            }).ToList());
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
        
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullScreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            var resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}