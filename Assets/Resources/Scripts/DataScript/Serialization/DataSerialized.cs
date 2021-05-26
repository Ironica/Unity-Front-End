using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonBridge{

  public class ResponseModel
  {
    public string status { get; set; }
    public DataSerialized[] payload { get; set; }
  }

  [System.Serializable]
  public class DataSerialized
  {
    //Common Part of the Playground's Incoming / Outgoing Data
    public string[,] grid;
    public string[,] layout;
    public string[,] colors;

    public int[,] levels;

    public Portal[] portals;

    public PlayerSerialized[] players;

    //The Incoming (from Front-end to Compiler) Data Structure
    public string type;
    public string code;
    public Lock[] locks;
    public StairSerialized[] stairs;

    public DataSerialized(){}

  }
}
