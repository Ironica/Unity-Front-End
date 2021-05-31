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
    private readonly string url;

    public JsonSerDes(string url){
      this.url = url;
    }

    /**
     * This method pack the DataOutSerialized data into serialized json format, and send it to the server,
     * then it returns the response from server
     */
    public string serialization(DataOutSerialized data, string map)
    {

      JsonConvert.DefaultSettings = () => // We triggered the JsonConvert's setting so that it takes enums' values into account
      {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new StringEnumConverter());
        return settings;
      };
      
      var json = JsonConvert.SerializeObject(data, // If a field of serialized object is null then it will be ignored
        Formatting.Indented,
        new JsonSerializerSettings
        {
          NullValueHandling = NullValueHandling.Ignore
        });
      File.WriteAllText(map,json); // Useless?
      Debug.Log("Serialization done"); // TODO remove redundant serialization

      return new JsonRequestHandler(url)
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

  }
}
