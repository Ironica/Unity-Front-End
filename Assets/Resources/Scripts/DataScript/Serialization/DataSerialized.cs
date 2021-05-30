using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public ResponseModel(string status, DataPayloadSerialized[] payload)
    {
      this.status = status;
      this.payload = payload;
    }

    public string status { get; }
    public DataPayloadSerialized[] payload { get; }
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
  
  public class DataOutSerialized
  {
    public string type;
    public string code;
    
    public GridObject[][] grid;
    
    public Coordinates[] gems;
    public Coordinates[] beepers;
    public SwitchSerialized[] switches;
    public PortalSerialized[] portals;
    
    public LockSerialized[] locks;
    public StairSerialized[] stairs;
    public PlatformSerialized[] platforms;

    public PlayerSerialized[] players;

    public string consoleLog;
    public string special;
    
    public DataOutSerialized(){}

  }

  [Serializable]
  public class DataPayloadSerialized
  {
    public PayloadGridObject[][] grid;

    public Coordinates[] gems;
    public Coordinates[] beepers;

    public PortalSerialized[] portals;
    public PlatformSerialized[] platforms;
    public LockSerialized[] locks;
    public PlayerSerialized[] players;
    public string consoleLog;
    public string special;

    public DataPayloadSerialized()
    {
    }
  }
}
