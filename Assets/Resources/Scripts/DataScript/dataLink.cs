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
  private JsonBridge.DataSerialized[] payload;
  private string currentMap;


  public const string url = "http://127.0.0.1:8080/paidiki-xara";

  private const string pathStarterMap = "Assets/Resources/MapJson/StarterMap/";
  private const string pathCurrentMap = "Assets/Resources/MapJson/CurrentMap";

  public string mapName;

  private string getUserCode(){
    return GameObject.Find("UserCode")
    .GetComponent<InputField>()
    .text;
  }

  /*
  **  Method called when the user compiles his code
  */
  private void compile(){

    /*if( the frame[] is empty )
    */

    // Get the user code in the InputField
    dataObj.code = getUserCode();

    //Convert the data object to data serializable
    converter.objectToSerialized();

    Debug.Log("Data conversion done");

    //Convert the data to json format
    //Send the json file to the servor
    //Get the response from the servor
    string  resp = des.serialization(dataSer, pathCurrentMap + currentMap);

    //Deserialization of the response
    JsonBridge.ResponseModel answers = des.webDeserialization(resp);

    //Get the frame array for the animation
    payload = answers.payload;

    //Print the status of the compilation
    Debug.Log("Status: " + answers.status);

  }

  // Start is called before the first frame update
  void Start()
  {

    currentMap = "map1.json";

    dataSer = des.deserialization(pathStarterMap + currentMap);
    dataObj = new Data();

    converter = new JsonBridge.DataConvert(dataSer, dataObj);
    converter.serializedToObject();

    string response  = des.serialization(dataSer, pathStarterMap + currentMap);



  }

  // Update is called once per frame
  void Update()
  {

  }
}
