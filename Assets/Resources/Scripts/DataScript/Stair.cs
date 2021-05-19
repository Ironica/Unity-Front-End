using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Stair
{
  public Coordinates coo;
  public Direction dir;

  public Stair(Coordinates coo, Direction dir){
    this.coo = coo;
    this.dir = dir;
  }
}
