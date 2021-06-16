using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Resources.Scripts.SettingScripts
{
    public class AudioSlider: MonoBehaviour
    {
        [SerializeField]private Slider _slider;
        [SerializeField]private TextMeshProUGUI _sliderText;

        public AudioMixer audioMixer;
        
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", volume);
        }
        void Start()
        {
            _slider.onValueChanged.AddListener(v =>
            {
                _sliderText.text = ((v + 80f) * 1.25f).ToString("00");
            });
        }
    }
}