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

    public IModel webDeserialization(string resp)
    {

      var respObj = JObject.Parse(resp);

      if (string.Equals(respObj["status"].ToString(), "OK"))
      {
        Debug.Log("Response OK, processing deserialization...");
        return respObj.ToObject<ResponseModel>();
      }

      Debug.Log("Response Error, processing deserialization...");
      return respObj.ToObject<ErrorMessageModel>();

    }

    public DataSerialized deserialization(string jsonFilePath){

      string mapJson = System.IO.File.ReadAllText(jsonFilePath);
      return JsonConvert.DeserializeObject<DataSerialized>(mapJson);
    }

  }
}
