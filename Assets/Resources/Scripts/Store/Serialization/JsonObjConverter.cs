using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonObjConverter
{

  private const string path = "Assets/Resources/Scripts/Store/JsonFile/storage.json";

  public static void toJson(ItemsInStore store){
    var json = JsonConvert.SerializeObject(store, Formatting.Indented);

    StreamWriter monStreamWriter = new StreamWriter(path);
    monStreamWriter.WriteLine(json);

    monStreamWriter.Close();
  }


  public static ItemsInStore toObj(){
    var json = System.IO.File.ReadAllText(path);

    return JsonConvert.DeserializeObject<ItemsInStore>(json);
  }

  public static void gemGain(int gems){
    ItemsInStore store = toObj();
    store.gems += gems;
    toJson(store);
  }

  public static void gemSpending(int gems){
    ItemsInStore store = toObj();
    store.gems -= gems;
    toJson(store);
  }
}
