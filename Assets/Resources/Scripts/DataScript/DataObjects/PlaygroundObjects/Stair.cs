using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair
{
  public int X { get; }
  public int Y { get; }
  public Direction Dir { get; }

  public Stair(int x, int y, Direction dir)
  {
    this.X = x;
    this.Y = y;
    this.Dir = dir;
  }
}
