using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSerialized
{
  public Coordinates coo;
  public Direction dir;

  public StairSerialized(int x, int y, Direction dir){
    this.coo = new Coordinates(x, y);
    this.dir = dir;
  }
}
