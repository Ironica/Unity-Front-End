using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class dataLink : MonoBehaviour
{

  public JsonBridge.DataSerialized dataSer;
  public Data dataObj;
  public JsonBridge.DataConvert converter;
  private JsonBridge.JsonSerDes des = new JsonBridge.JsonSerDes(url);
  private JsonBridge.DataSerialized[] payload;
  private string currentMap;

  //Ground
  private string open       = "Prefabs/PLAIN_OPEN_LEVEL";
  private string blocked    = "Prefabs/PLAIN_HILL_LEVEL";
  private string water      = "Prefabs/PLAIN_WATER_LEVEL";
  private string tree       = "Prefabs/PLAIN_TREE_LEVEL";
  private string desert     = "Prefabs/PLAIN_DESERT_LEVEL";
  private string home       = "Prefabs/PLAIN_HOME_LEVEL";
  private string mountain   = "Prefabs/PLAIN_MOUNTAIN_LEVEL";

  // Stairs
  private string  stairLeft     = "Prefabs/PLAIN_STAIRS_LEFT";
  private string  stairRight    = "Prefabs/PLAIN_STAIRS_RIGHT";
  private string  stairBack     = "Prefabs/PLAIN_STAIRS_BACK";
  private string  stairFront    = "Prefabs/PLAIN_STAIRS_FRON";
  private int     currentStair  = -1;

  //Players skin
  private string playerPrefab = "Prefabs/ITEM/CHARACTER_FRONT";


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

  private Vector2 setCoordinates(Vector2 vector){
    Vector2 i = new Vector2(-0.5f, -0.25f);
    Vector2 j = new Vector2(0.5f, -0.25f);
    return vector.x*i + vector.y*j;
  }

  private string stairDirection(Direction dir){
    switch(dir){
      case Direction.UP: return stairFront;
      case Direction.DOWN: return stairBack;
      case Direction.LEFT: return stairLeft;
      default: return stairRight;
    }
  }

  public string tileLevel(Block block, int i, int j){
    switch(block){
      case Block.OPEN:return open + dataObj.levels[j, i];
      case Block.BLOCKED: return blocked + dataObj.levels[j, i];
      case Block.WATER: return water + dataObj.levels[j, i];
      case Block.TREE: return tree + dataObj.levels[j, i];
      case Block.DESERT: return desert + dataObj.levels[j, i];
      case Block.HOME: return home + dataObj.levels[j, i];
      case Block.MOUNTAIN: return mountain+1;
      case Block.STONE: return mountain+1;
      case Block.LOCK: return home + dataObj.levels[j, i];
      default:
      currentStair++;
      return stairDirection(dataObj.stairs[currentStair].dir);
    }
  }

  private void mapInstantiation(Block block, int level, int x, int y, int i, int j){
    string tile = tileLevel(block, i, j);
    Vector2 coo = setCoordinates(new Vector2(j-x, i-y));
    GameObject tileObject  = Instantiate(Resources.Load(tile), new Vector3(coo.x,coo.y, 0), Quaternion.identity) as GameObject;
    tileObject.transform.parent = this.transform;
    Debug.Log("x: " + coo.x + " y: " + coo.y);
  }

  private void playerInstantiation(Player player, int x, int y){
    Vector2 coo = setCoordinates(new Vector2(player.x-x, player.y-y));
    GameObject tileObject  = Instantiate(Resources.Load(playerPrefab), new Vector3(coo.x,coo.y, 0), Quaternion.identity) as GameObject;
  }

  private void instantiation(){
    int x = dataObj.grid.GetLength(1)/2;
    int y =dataObj.grid.GetLength(0)/2;
    Debug.Log("x: " + x + "; y: " + y);
    for(int i =0; i<dataObj.grid.GetLength(1); i++){
      for(int j = 0; j<dataObj.grid.GetLength(0); j++){
        mapInstantiation(dataObj.grid[j,i], dataObj.levels[j,i], x, y, i, j);
        if(dataObj.grid[j, i] == Block.HOME){
          Debug.Log("j: " + j + ", i: " + i);
        }
      }
    }
    for(int i=0; i<dataObj.players.Length; i++){
      playerInstantiation(dataObj.players[i], x, y);
    }
  }

  // Start is called before the first frame update
  void Start()
  {

    currentMap = "map2.json";

    dataSer = des.deserialization(pathStarterMap + currentMap);

    dataObj = new Data();

    converter = new JsonBridge.DataConvert(dataSer, dataObj);
    converter.serializedToObject();

    string response  = des.serialization(dataSer, pathStarterMap + currentMap);

    instantiation();

  }

  // Update is called once per frame
  void Update()
  {

  }
}
