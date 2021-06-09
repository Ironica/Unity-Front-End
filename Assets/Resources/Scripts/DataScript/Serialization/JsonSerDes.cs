using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Converters;
using Resources.Scripts.DataScript.DataObjects;

namespace JsonBridge{

  public class JsonSerDes
  {
    private readonly string url;
    private readonly int port;
    private readonly string api;

    public JsonSerDes(string url, int port, string api){
      this.url = url;
      this.port = port;
      this.api = api;
    }

    // TODO move these two helper functions to appropriate place

    /**
     * Helper method for conversion between Block and BlockData
     */
    private BlockData convertFrontEndBlockDataToSerializableData(Block block)
      => block switch
      {
        Block.OPEN => BlockData.OPEN,
        Block.HOME => BlockData.OPEN,
        Block.MOUNTAIN => BlockData.BLOCKED,
        Block.DESERT => BlockData.OPEN,
        Block.TREE => BlockData.BLOCKED,
        Block.WATER => BlockData.BLOCKED,
        Block.HILL => BlockData.OPEN,
        Block.STAIR => BlockData.STAIR,
        Block.VOID => BlockData.VOID,
        _ => throw new Exception("JsonSerDes:: Unknown block data")
      };

    /**
     * This method convert dataSer (DataOutSerialized) to RealDataOutSerialized that will be dispatched to the server
     */
    private RealDataOutSerialized convertDataSerializedToOutgoingData(DataOutSerialized data)
      => new RealDataOutSerialized(
        data.type, data.code,
        data.grid.Select(l =>
          l.Select(e => new OutgoingGridObject(
            convertFrontEndBlockDataToSerializableData(e.block),
            e.biome, e.level
          )).ToArray()).ToArray(),
        data.gems, data.beepers, data.switches, data.portals,
        data.monsters,
        data.locks, data.stairs, data.platforms,
        data.players,
        data.gamingCondition, data.userCollision
      );

    /**
     * This method pack the DataOutSerialized data into serialized json format, and send it to the server,
     * then it returns the response from server
     */
    public string serialization(DataOutSerialized data)
    {

      JsonConvert.DefaultSettings = () => // We triggered the JsonConvert's setting so that it takes enums' values into account
      {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new StringEnumConverter());
        return settings;
      };

      var json = JsonConvert.SerializeObject(convertDataSerializedToOutgoingData(data), // If a field of serialized object is null then it will be ignored
        Formatting.Indented,
        new JsonSerializerSettings
        {
          NullValueHandling = NullValueHandling.Ignore
        });

      Debug.Log("Serialization done");

      return new JsonRequestHandler($"{url}:{port}/{api}")
      .Feed(json)
      .Fetch()
      .Get();
    }

    /**
     * This method encapsulates the deserialization side from the server
     */
    public IModel webDeserialization(string resp)
    {

      var respObj = JObject.Parse(resp); // We don't deserialize directly but parse the object

      // It's a partial deserialization, we check first if the status is OK or ERROR, and we choose the deserialization
      // model by looking at different cases
      if (string.Equals(respObj["status"].ToString(), "OK"))
      {
        Debug.Log("Response OK, processing deserialization...");
        return respObj.ToObject<ResponseModel>();
      }

      Debug.Log("Response Error, processing deserialization...");
      return respObj.ToObject<ErrorMessageModel>();

    }

    public DataOutSerialized deserialization(string jsonFilePath){

      string mapJson = System.IO.File.ReadAllText(jsonFilePath);
      return JsonConvert.DeserializeObject<DataOutSerialized>(mapJson);
    }

    public Inventory inventoryDeserialization(){
      string inventory = System.IO.File.ReadAllText("Assets/Resources/Inventory/inventory.json");
      return JsonConvert.DeserializeObject<Inventory>(inventory);
    }

    public void inventorySerialization(Inventory i){
      string texte =  JsonConvert.SerializeObject(i);
      File.WriteAllText("Assets/Resources/Inventory/inventory.json", texte);
    }

  }
}
