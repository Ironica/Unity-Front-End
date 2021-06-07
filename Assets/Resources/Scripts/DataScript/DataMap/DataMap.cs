using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMap
{
    public string name {get; set;}
    public string code {get; set;}
    public int maxGem {get; set;}
    public bool win {get; set;}

    public DataMap(string name)
    {
      this.name = name;
      code = "";
      maxGem = 0;
      win = false;
    }
}
