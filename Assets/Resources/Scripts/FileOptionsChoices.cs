using System;
using UnityEngine;
using UnityEngine.UI;

public class FileOptionsChoices : MonoBehaviour
{
  public Dropdown ChoiceDropdown;

  public void Start()
  {
    ChoiceDropdown.ClearOptions();
  }
}
