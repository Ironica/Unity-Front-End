using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Lock
{
  public Coordinates coo;
  public Coordinates[] controlled;

  public Lock(Coordinates coo, Coordinates[] controlled){
    this.coo = coo;
    this.controlled = controlled;
  }
}
