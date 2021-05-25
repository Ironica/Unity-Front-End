using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class StairSerialized
{
  public Coordinates coo;
  public string dir;

  public StairSerialized(int x, int y, string dir){
    this.coo = new Coordinates(x, y);
    this.dir = dir;
  }
}
