using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Data{
  public Tile[][] grid;
  public Coordinates[] gems;
  public Coordinates[] beepers;
  public Switch[] switches;
  public Portal[] portals;
  public Lock[] locks;
  public Stair[] stairs;
  public Platform[] platforms;
  public Player[] players;

  public string type;
  public string code;

  [CanBeNull] public string consoleLog;
  [CanBeNull] public string special;

  public Data(){}
}
