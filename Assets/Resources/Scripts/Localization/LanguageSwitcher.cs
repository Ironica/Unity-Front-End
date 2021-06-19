using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public void SwitchLanguage(int index)
    {
        LocalizationSystem.language = index switch
        {
            0 => LocalizationSystem.Language.English,
            1 => LocalizationSystem.Language.French,
            2 => LocalizationSystem.Language.Chinese,
            3 => LocalizationSystem.Language.Japanese,
        };
    }
}
