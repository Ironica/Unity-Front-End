using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMap
{
  public string name;
  public bool[] goals;
  public string code;

  public DataMap(string name, string code)
  {
    this.name = name;
    this.code = code;
  }

}
