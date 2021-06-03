using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ChoiceMenu : MonoBehaviour
{
    public GameObject choiceWindow;
    private bool isActiveChoice = false;
    public AudioMixer audioMix;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  isActiveChoice)
        {
            CloseChoice();
           // mapMenu.SetActive(false);
          //  png.SetActive(false);
        }
    }

    public void Choice()
    {
        if (isActiveChoice)
        {
            CloseChoice();
        }
        else
        {
            choiceWindow.SetActive(true);
            isActiveChoice = true;
        }

    }

    public void CloseChoice()
    {
        choiceWindow.SetActive(false);
        isActiveChoice = false;
    }
    public void ChangeMap()
    {
        SceneManager.LoadScene("MapMenu");
    }
    public void SaveFile()
    {

    }
    public void LoadFile()
    {

    }

    public void SetVolume(float volume)
    {
        audioMix.SetFloat("Volume", volume);
    }

}
