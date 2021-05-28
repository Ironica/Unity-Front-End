using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonBridge{

  public interface IModel
  {
    public string status { get; }
  }

  public class BareResponseModel: IModel
  {
    public string status { get; }
  }

  public class ResponseModel: IModel
  {
    public ResponseModel(string status, DataResponseSerialized[] payload)
    {
      this.status = status;
      this.payload = payload;
    }

    public string status { get; }
    public DataResponseSerialized[] payload { get; }
  }

  public class ErrorMessageModel: IModel
  {
    public string status { get; }
    public string msg { get; }

    public ErrorMessageModel(string status, string msg)
    {
      this.status = status;
      this.msg = msg;
    }
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

  [Serializable]
  public class DataResponseSerialized
  {
    public string[,] grid;
    public string[,] itemLayout;
    public string[,] colors;

    public int[,] levels;

    public Portal[] portals;
    public PlayerSerialized[] players;

    public string type;
    public string code;
    public Lock[] locks;
    public StairSerialized[] stairs;

    public DataResponseSerialized()
    {
    }
  }
}
