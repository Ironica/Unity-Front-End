using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Runtime.Serialization.Formatters.Binary;

public static class SaveMapManager
{

  private const string path = "Assets/Resources/MapJson/DataMap/";

  public static void saveData(DataMap map)
  {
    /*BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(path + map.name + ".json", FileMode.Create);

    formatter.Serialize(stream, map);
    stream.Close();*/

    var json = JsonConvert.SerializeObject(map, Formatting.Indented);

    StreamWriter myStreamWriter = new StreamWriter(path + map.name + ".json");
    myStreamWriter.WriteLine(json);

    myStreamWriter.Close();
  }

  public static DataMap loadData(DataMap map)
  {
    /*string path = Application.persistentDataPath + "/" + map.name + ".save";
    if(File.Exists(path))
    {
    BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(path, FileMode.Open);

    map = formatter.Deserialize(stream) as DataMap;
    stream.Close();

    return map;
    } else
    {
    return new DataMap(map.name);
    }*/
    if(File.Exists(path + map.name + ".json")){
      var json = System.IO.File.ReadAllText(path + map.name + ".json");

      return JsonConvert.DeserializeObject<DataMap>(json);
    }
    else {
      saveData(map);
      return map;
    }
  }

}
