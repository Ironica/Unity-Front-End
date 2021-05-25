using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Portal
{

  public Coordinates coo;
  public Coordinates dest;


  public bool isActive;

  public Portal(Coordinates coo, Coordinates dest){
    this.coo = coo;
    this.dest = dest;

    isActive = true;

  }
}
