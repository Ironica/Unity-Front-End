using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.IO;

namespace JsonBridge{

  public class JsonSerDes
  {
    private string url;

    public JsonSerDes(string url){
      this.url = url;
    }

    public string serialization(DataSerialized data, string map){

      string json = JsonConvert.SerializeObject(data, Formatting.Indented);
      File.WriteAllText(map,json);
      Debug.Log("Serialization done");

      string resp = new JsonRequestHandler(url)
      .Feed(JsonConvert.SerializeObject(data))
      .Fetch()
      .get();

      return resp;
    }

    public ResponseModel webDeserialization(string resp){

      ResponseModel answers = JsonConvert.DeserializeObject<ResponseModel>(resp);

      Debug.Log("Deserialization done");

      return answers;

    }

    public DataSerialized deserialization(string jsonFilePath){

      string mapJson = System.IO.File.ReadAllText(jsonFilePath);

      return JsonConvert.DeserializeObject<DataSerialized>(mapJson);
    }

    /*
    // Start is called before the first frame update
    void Start()
    {

      //data = new DataSerialized();
      data.type = "colorfulmountainous";
      data.code =
      "cst a = Player()\n" +
      "cst b = Player()\n" +
      "cst c = Specialist()\n" +
      "for _ in 1 ... 3 {\n" +
      "    a.changeColor(\"RED\")\n" +
      "    a.moveForward()\n" +
      "    b.changeColor(\"BLACK\")\n" +
      "    b.moveForward()\n" +
      "    c.changeColor(\"GREEN\")\n" +
      "    c.moveForward()\n" +
      "}\n";
      data.grid = new[,]
      {
        {"OPEN", "OPEN", "OPEN"},
        {"OPEN", "STAIR", "OPEN"},
        {"OPEN", "STAIR", "STAIR"},
        {"STAIR", "OPEN", "STAIR"},
      };
      data.layout = new[,]
      {
        {"NONE", "NONE", "GEM"},
        {"NONE", "NONE", "NONE"},
        {"NONE", "NONE", "NONE"},
        {"NONE", "NONE", "NONE"},
      };
      data.colors = new[,]
      {
        {"WHITE", "WHITE", "WHITE",},
        {"WHITE", "WHITE", "WHITE",},
        {"WHITE", "WHITE", "WHITE",},
        {"WHITE", "WHITE", "WHITE",},
      };
      data.levels = new[,]
      {
        {2, 1, 1},
        {1, 2, 1},
        {1, 2, 2},
        {2, 1, 3}
      };
      data.portals = Array.Empty<Portal>();
      data.locks = Array.Empty<Lock>();
      data.stairs = new []
      {
        new StairSerialized(0, 3, "UP"),
        new StairSerialized(1, 1, "UP"),
        new StairSerialized(1, 2, "DOWN"),
        new StairSerialized(2, 2, "UP"),
        new StairSerialized(2, 3, "UP")
      };
      data.players = new[]
      {
        new PlayerSerialized (1, 1, 1, "DOWN", "PLAYER", 90),
        new PlayerSerialized (2, 1, 0, "DOWN", "SPECIALIST", 120),
        new PlayerSerialized (3, 2, 0, "DOWN", "PLAYER", 100),
      };
    }

    // Update is called once per frame
    void Update()
    {

    }*/
  }
}
