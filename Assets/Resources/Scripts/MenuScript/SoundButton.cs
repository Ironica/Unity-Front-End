using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.MenuScript
{
    public class SoundButton : MonoBehaviour
    {
        public GameObject soundBar; 
        public Sprite[] soundSprites;
        private Image soundImage;
        private int soundState;

        private void Start()
        {
            soundState = 0;
            soundImage = GetComponent<Button>().image;
            soundImage.sprite = soundSprites[soundState];
            soundBar.SetActive(false);

            gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff);
        }

        private void TurnOnAndOff()
        {
            soundState = 1 - soundState;
            soundImage.sprite = soundSprites[soundState];
            soundBar.SetActive(soundState != 0);
            
        }
    }
}