using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
  
public class Lock
{
  public int X { get; }
  
  public int Y { get; }

  public int Energy { get; }

  public bool IsActive => Energy > 0;

  public Lock(int x, int y, int? energy)
  {
    this.X = x;
    this.Y = y;
    this.Energy = energy ?? 0;
  }
};


