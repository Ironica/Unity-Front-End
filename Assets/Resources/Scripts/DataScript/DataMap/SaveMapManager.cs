using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveMapManager
{
    public static void saveData(DataMap map)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + map.name + "txt";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, map);
        stream.Close();
    }

    public static DataMap loadData(string mapName)
    {
        string path = Application.persistentDataPath + "/" + mapName + "txt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataMap map = formatter.Deserialize(stream) as DataMap;
            stream.Close();

            return map;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
