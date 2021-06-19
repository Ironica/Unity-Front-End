using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public void SwitchLanguage(int index)
    {
        LocalizationSystem.language = index switch
        {
            0 => LocalizationSystem.Language.English,
            1 => LocalizationSystem.Language.French,
            2 => LocalizationSystem.Language.Chinese,
            3 => LocalizationSystem.Language.Japanese,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void Start()
    {
        dropdown.value = LocalizationSystem.language switch
        {
            LocalizationSystem.Language.English => 0,
            LocalizationSystem.Language.French => 1,
            LocalizationSystem.Language.Chinese => 2,
            LocalizationSystem.Language.Japanese => 3,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
