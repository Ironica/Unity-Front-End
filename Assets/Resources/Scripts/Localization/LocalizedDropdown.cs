using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
[AddComponentMenu("Localization/Localized Dropdown")]
public class LocalizedDropdown : MonoBehaviour
{
    // Fields
    // =======
    public List<LocalizedDropDownOption> options;
    public int selectedOptionIndex = 0;
    
    // Properties
    // ===========
    private TMP_Dropdown Dropdown => GetComponent<TMP_Dropdown>();
    
    // Methods
    // ========
    private IEnumerator Start()
    {
        yield return PopulateDropdown();
    }

    private void OnEnable()
    {
        UpdateDropdownOptions(LocalizationSystem.language);
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private List<LocalizedDropDownOption> InitializeOptions()
        => Dropdown.options.Select(e => new LocalizedDropDownOption
        {
            text = e.text, sprite = e.image
        }).ToList();

    private IEnumerator PopulateDropdown()
    {
        options = InitializeOptions();
        selectedOptionIndex = Dropdown.value;
        Dropdown.ClearOptions();
        Dropdown.onValueChanged.RemoveListener(UpdateSelectedOptionIndex);
        
        for (var i = 0; i < options.Count; i++)
        {
            var option = options[i];
            var localizedText = string.Empty;
            Sprite localizedSprite = null;
            if (option.text.key != string.Empty)
            {
                localizedText = option.text.value;
                yield return localizedText;
                if (i == selectedOptionIndex)
                {
                    UpdateSelectedText(localizedText);
                }
            }

            if (option.sprite != null)
            {
                localizedSprite = option.sprite;
                yield return localizedSprite;
                if (i == selectedOptionIndex)
                {
                    UpdateSelectedSprite(localizedSprite);
                }
            }
            Dropdown.options.Add(new TMP_Dropdown.OptionData(localizedText, localizedSprite));
        }

        Dropdown.value = selectedOptionIndex;
        Dropdown.onValueChanged.AddListener(UpdateSelectedOptionIndex);
    }

    private void UpdateDropdownOptions(LocalizationSystem.Language language)
    {
        for (var i = 0; i < Dropdown.options.Count; i++)
        {
            var optionI = i;
            var option = options[i];

            if (option.text.key != string.Empty)
            {
                Dropdown.options[optionI].text = option.text.value;
                if (optionI == selectedOptionIndex)
                {
                    UpdateSelectedText(option.text.value);
                }
            }

            if (option.sprite != null)
            {
                Dropdown.options[optionI].image = option.sprite;
                if (optionI == selectedOptionIndex)
                {
                    UpdateSelectedSprite(option.sprite);
                }
            }
        }
    }

    private void UpdateSelectedOptionIndex(int index) => selectedOptionIndex = index;

    private void UpdateSelectedText(string text)
    {
        if (Dropdown.captionText != null)
        {
            Dropdown.captionText.text = text;
        }
    }

    private void UpdateSelectedSprite(Sprite sprite)
        {
            if (Dropdown.captionImage != null)
            {
                Dropdown.captionImage.sprite = sprite;
            }
        }
}

public class LocalizedDropDownOption
{
    public LocalizedString text;
    public Sprite sprite;
}