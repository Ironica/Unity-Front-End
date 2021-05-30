using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.IO;
using Newtonsoft.Json.Converters;

namespace JsonBridge{

  public class JsonSerDes
  {
    private string url;

    public JsonSerDes(string url){
      this.url = url;
    }

    public string serialization(DataOutSerialized data, string map)
    {

      JsonConvert.DefaultSettings = () =>
      {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new StringEnumConverter());
        return settings;
      };
      
      var json = JsonConvert.SerializeObject(data, 
        Formatting.Indented,
        new JsonSerializerSettings
        {
          NullValueHandling = NullValueHandling.Ignore
        });
      File.WriteAllText(map,json);
      Debug.Log("Serialization done"); // TODO remove redundant serialization

      string resp = new JsonRequestHandler(url)
      .Feed(json)
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

    public DataOutSerialized deserialization(string jsonFilePath){

      string mapJson = System.IO.File.ReadAllText(jsonFilePath);
      return JsonConvert.DeserializeObject<DataOutSerialized>(mapJson);
    }

  }
}
