using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair
{
  public Coordinates coo;
  public Direction dir;

  public Stair(int x, int y, Direction dir){
    this.coo = new Coordinates(x, y);
    this.dir = dir;
  }
}
