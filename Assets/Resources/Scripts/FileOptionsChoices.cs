using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileOptionsChoices : MonoBehaviour
{
    public Dropdown ChoiceDD;
    void Start()
    {
        ChoiceDD.ClearOptions();

        List<String> choices = new List<string>();
        
        choices.Add("Change map");
        choices.Add("Save file");
        choices.Add("Load file");
        
        int currentIndexRes = 0;
        ChoiceDD.AddOptions(choices);
        ChoiceDD.value = currentIndexRes;
        ChoiceDD.RefreshShownValue();
    }
}
