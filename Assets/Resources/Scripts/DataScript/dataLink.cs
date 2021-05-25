using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataLink : MonoBehaviour
{

  public JsonBridge.DataSerialized dataSer;
  public Data dataObj;
  public JsonBridge.DataConvert converter;
  private JsonBridge.JsonSerDes des = new JsonBridge.JsonSerDes(url);

  public const string url = "http://127.0.0.1:8080/paidiki-xara";

  private const string pathStarterMap = "Assets/Resources/MapJson/StarterMap/";
  private const string pathCurrentMap = "Assets/Resources/MapJson/CurrentMap";

  public string mapName;

  private string getUserCode(){
    return GameObject.Find("UserCode")
    .GetComponent<InputField>()
    .text;
  }
  private void compile(){

    /*if( the frame[] is empty )
    */

    dataObj.code = getUserCode();

    Debug.Log(dataObj.code);

    converter.objectToSerialized();

    Debug.Log(dataSer.code);

  }

  // Start is called before the first frame update
  void Start()
  {

    mapName = "map1.json";

    dataSer = des.deserialization(pathStarterMap + mapName);
    dataObj = new Data();

    converter = new JsonBridge.DataConvert(dataSer, dataObj);
    converter.serializedToObject();

    string response  = des.serialization(dataSer);

  }

  // Update is called once per frame
  void Update()
  {

  }
}
