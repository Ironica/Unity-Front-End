using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using System.IO;

public class JsonSerDes : MonoBehaviour
{
    public Data data;

    public void serialization(){

      string json= JsonConvert.SerializeObject(data, Formatting.Indented);
      File.WriteAllText("data.json",json);

      Debug.Log("Serialization done");
    }

    public void deserialization(){

      string json = File.ReadAllText("data.json");
      data = JsonConvert.DeserializeObject<Data>(json);

      Debug.Log("Deserialization done");
    }

    // Start is called before the first frame update
    void Start()
    {
      data = new Data();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
