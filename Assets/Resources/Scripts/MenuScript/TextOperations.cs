using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class TextOperations : MonoBehaviour
{
  public InputField textArea;
  private String textToUse;
  private String path = "Assets/test.txt";
  
  public void Save()
  {
      textToUse = textArea.text;
      File.WriteAllText(path,textToUse);
      
  }

    public void Load()
  {
      textToUse = File.ReadAllText(path);
      textArea.text = textToUse;
  }
}
