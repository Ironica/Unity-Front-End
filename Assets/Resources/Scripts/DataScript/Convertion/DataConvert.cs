using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace JsonBridge {

  // With the architecture proposed (see top of DataLink), only the direction of serialized class to object class
  // is necessary. Therefore I removed lots of unnecessary functions.
  public class DataConvert
  {

    public DataInSerialized dataIn;
    public DataOutSerialized dataSer; // Attention to the call-by-ref behavior of dataSer and dataObj
    public Data dataObj;

    public DataConvert(DataInSerialized dataIn, DataOutSerialized dataSer, Data dataObj)
    {
      this.dataIn = dataIn;
      this.dataSer = dataSer;
      this.dataObj = dataObj;
    }
    /* (REMOVED)
    ** dataSer contains all data serializable
    ** dataObj contains all data for the constructions of the playground
    ** Update the dataObj with the dataSer's values
    */


    /**
    * dataObj contains all data for the constructions of the playground
    * dataSer contains all data serializable
    * Update the dataSer with the dataObj's values
    */
    public void serializedToObject()
    {

      // rewritten with Linq methods, I think it's more declarative and you don't really need comments to understand them

      dataObj.grid = dataSer.grid.Select((l, y) =>
      l.Select((e, x) =>
      new Tile(e.block, e.biome, e.level)
      ).ToArray()).ToArray();

      dataObj.gems = dataSer.gems.Select(e => e).ToArray();

      dataObj.beepers = dataSer.beepers.Select(e => e).ToArray();

      dataObj.switches = dataSer.switches.Select(e => new Switch(e.coo.x, e.coo.y, e.on)).ToArray();

      dataObj.portals = dataSer.portals.Select(e => new Portal(e.coo.x, e.coo.y, e.energy)).ToArray();

      dataObj.locks = dataSer.locks.Select(e => new Lock(e.coo.x, e.coo.y, e.energy)).ToArray();

      dataObj.stairs = dataSer.stairs.Select(e => new Stair(e.coo.x, e.coo.y, e.dir)).ToArray();

      dataObj.platforms = dataSer.platforms.Select(e => new Platform(e.coo.x, e.coo.y, e.level)).ToArray();

      dataObj.players = dataSer.players.Select(e => new Player(e.id, e.x, e.y, e.dir, e.role, e.stamina)).ToArray();

      dataObj.consoleLog = dataSer.consoleLog;

      dataObj.special = dataSer.special;

    }

    /*public void stringToSerialized()
    {
      for(int i=0; i<dataIn.grid.GetLength(0); i++){
        for(int j =0; j<dataIn.grid.GetLength(1); j++){
          dataSer.grid[i][j] = tileNameToBlock(dataIn.grid[i][j]);
        }
      }
      dataSer.gems = dataIn.gems;
      dataSer.beepers = dataIn.beepers;
      dataSer.switches = dataIn.switches;
      dataSer.portals = dataIn.portals;
      dataSer.locks = dataIn.locks;
      dataSer.stairs = dataIn.stairs;
      dataSer.platforms = dataIn.platforms;
      dataSer.players = dataIn.players;
      dataSer.consoleLog = dataIn.consoleLog;
      dataSer.special = dataIn.special;
    }

    private GridObject tileNameToBlock(GridString tile)
    {
      Block block;
      if(tile.Equals("MOUNTAIN") || tile.Equals("TREE") || tile.Equals("WATER"))
      {
        block = Block.BLOCKED;
      }
      else
      {
        block = Block.OPEN;
      }
      return new GridObject(block, tile.biome, tile.level);
    }*/
  }
}
