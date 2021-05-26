using System;
using UnityEngine;

public class ChoiceMenu : MonoBehaviour
{
    public GameObject ChoiceWindow;
    private bool isActiveChoice = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  isActiveChoice)
        {
            CloseChoice();
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
            ChoiceWindow.SetActive(true);
            isActiveChoice = true;
        }
       
    }

    public void CloseChoice()
    {
        ChoiceWindow.SetActive(false);
        isActiveChoice = false;
    }
    public void ChangeMap()
    {
        
    }
    public void SaveFile()
    {
        
    }
    public void LoadFile()
    {
        
    }

  
}
