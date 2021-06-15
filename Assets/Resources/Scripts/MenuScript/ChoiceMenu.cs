using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceMenu : MonoBehaviour
{
    public GameObject choiceWindow;
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
        SceneManager.LoadScene("Map_Menu");
    }




}
