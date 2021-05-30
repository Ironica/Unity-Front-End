using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace JsonBridge {

  public class DataConvert
  {

    public DataOutSerialized dataSer;
    public Data dataObj;

    public DataConvert(DataOutSerialized dataSer, Data dataObj)
    {
      this.dataSer = dataSer;
      this.dataObj = dataObj;
    }
    /*
    ** dataSer contains all data serializable
    ** dataObj contains all data for the constructions of the playground
    ** Update the dataObj with the dataSer's values
    */


    /*
    ** dataObj contains all data for the constructions of the playground
    ** dataSer contains all data serializable
    ** Update the dataSer with the dataObj's values
    */

    public void serializedToObject()
    {

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
  }
}
