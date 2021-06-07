using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/* About DataObjects
 *
 * - package PlaygroundObjects contains all classes that constitutes the Data class
 *   They are used for communicate with Unity rendering engine
 *
 * - package SerializedObjects contains all classes that will be used in serialization/deserialization procedure
 *   They are used by DataOutSerialized and DataPayloadSerialize classes in DataSerialized.cs
 *
 * - Biome, Block, Direction, Role are enums. Pay attention to "EnumMember" annotation which allows Json Serializer
 *   to recognize their string values.
 *
 * - notice that in Data class some items are recorded with Coordinates class. However, you will need to instantialize
 *   the proper classes in last procedure to deal with dataObj.
 */

/**
 * This class represent the layout of object that acts as a communicator with the Unity display
 * It should only carry info that's useful to the rendering of the playground
 */
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

// could we replace all arrays by lists?
