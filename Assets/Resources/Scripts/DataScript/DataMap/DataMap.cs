using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMap
{
    public string name {get; set;}

    public string storyTilte {get; set;}
    public string story {get; set;}

    public string goalsTitle {get; set;}
    public Goals goal {get; set;}

    public string code {get; set;}

    public int maxGem {get; set;}
    public int maxSwitchOn {get; set;}
    public int maxMonster {get; set;}

    public bool win {get; set;}

    public DataMap(string name)
    {
      this.name = name;

      storyTilte = "";
      story = "";

      goalsTitle = "";
      goal = Goals.SWITCHON;

      code = "";

      maxGem = 0;
      maxSwitchOn = 0;
      maxMonster = 0;

      win = false;
    }
}
