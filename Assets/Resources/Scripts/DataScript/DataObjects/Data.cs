using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data{

  //Common Part of the Playground's Incoming / Outgoing Data
  public Block[,] grid;
  public Item[,] layout;
  public Color[,] colors;
  public int[,] levels;
  public Portal[] portals;
  public Player[] players;

  //The Incoming (from Front-end to Compiler) Data Structure
  public string type;
  public string code;
  public Lock[] locks;
  public Stair[] stairs;

  public Data(){}
}
