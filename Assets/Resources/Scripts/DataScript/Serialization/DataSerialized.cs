using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace JsonBridge{

  /**
  * Abstract Model Interface
  * Minimum requirement: implement `status` field which is a string
  */
  public interface IModel
  {
    public string status { get; } // TODO change this to enum
  }

  /**
  * The first Model that implements interface
  * It represents the layout of a response when everything is okay
  * It contains an array of payload (payloads: DataPayloadSerialized[])
  */
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

  /**
  * The second Model that implements interface
  * It contains only a message info, use it to display to user or for debug purpose
  */
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

  /**
  * The data structure representing every request format, it contains complete info of a playground including Game Type
  * and user's code.
  * See DataLink.dataSer in DataLink class for more information.
  */
  public class DataOutSerialized
  {
    public string type; // TODO change this to enum
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

    public DataOutSerialized()
    {

    }

  }

  public class DataInSerialized
  {
    public string type; // TODO change this to enum
    public string code;

    public GridString[][] grid;

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

    public DataInSerialized(){}

  }

  /**
  * This data structure represents a frame that you will receive from the server. It should contain only what could be
  * changed during the animation, evaluated from the server.
  * You concatenate each frame with your initial map (see DataOutSerialized for more info).
  */
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
