using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveMapManager
{
  public static void saveData(DataMap map)
  {
    Debug.Log("in SaveMapManager scripts: " + map.maxGem);
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/" + map.name + ".save";
    FileStream stream = new FileStream(path, FileMode.Create);

    formatter.Serialize(stream, map);
    stream.Close();
  }

  public static DataMap loadData(DataMap map)
  {
    string path = Application.persistentDataPath + "/" + map.name + ".save";
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
      }
    }

  }
