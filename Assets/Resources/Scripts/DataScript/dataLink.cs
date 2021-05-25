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

  // Level 0
  private string open0 = "Prefabs/PLAIN_OPEN_LEVEL0";
  private string blocked0 = "Prefabs/PLAIN_HILL_LEVEL0";
  private string water0 = "Prefabs/PLAIN_WATER";
  private string tree0 = "Prefabs/PLAIN_TREE_LEVEL0";
  private string desert0 = "Prefabs/PLAIN_DESERT_LEVEL0";
  private string home0 = "Prefabs/PLAIN_HOME 1";
  private string mountain0 = "Prefabs/PLAIN_MOUNTAIN";


  // Level 1
  private string open1 = "Prefabs/PLAIN_OPEN";
  private string blocked1 = "Prefabs/PLAIN_HILL";
  private string water1 = "Prefabs/PLAIN_WATER 1";
  private string tree1 = "Prefabs/PLAIN_TREE";
  private string desert1 = "Prefabs/PLAIN_DESERT";
  private string home1 = "Prefabs/PLAIN_HOME";

  // Stairs
  private string stairLeft = "Prefabs/PLAIN_STAIRS_LEFT";
  private string stairRight = "Prefabs/PLAIN_STAIRS_RIGHT";
  private string stairBack = "Prefabs/PLAIN_STAIRS_BACK";
  private string stairFront = "Prefabs/PLAIN_STAIRS_FRON";
  private int currentStair = -1;

  //Players skin
  private string player1 = "Prefabs/ITEM/CHARACTER";


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

  public void mapInstantiation(Block block, int level, float x, float y, int i, int j){
    string tile;
    if(dataObj.levels[j, i] == 1){
      tile = tileLevel0(block);
    }
    else {
      tile = tileLevel1(block);
    }
    GameObject tileObject  = Instantiate(Resources.Load(tile), new Vector3(x,y, 0), Quaternion.identity) as GameObject;
    tileObject.transform.parent = this.transform;
  }

  public string tileLevel0(Block block){
    switch(block){
      case Block.OPEN: return open0;
      break;
      case Block.BLOCKED: return blocked0;
      break;
      case Block.WATER: return water0;
      break;
      case Block.TREE: return tree0;
      break;
      case Block.DESERT: return desert0;
      break;
      case Block.HOME: return home0;
      break;
      case Block.MOUNTAIN: return mountain0;
      break;
      case Block.STONE: return mountain0;
      break;
      case Block.LOCK: return home0;
      break;
      default:
      currentStair++;
      return stairDirection(dataObj.stairs[currentStair].dir);
      break;
    }
  }

  public string tileLevel1(Block block){
    switch(block){
      case Block.OPEN: return open1;
      break;
      case Block.BLOCKED: return blocked1;
      break;
      case Block.WATER: return water1;
      break;
      case Block.TREE: return tree1;
      break;
      case Block.DESERT: return desert1;
      break;
      case Block.HOME: return home1;
      break;
      case Block.MOUNTAIN: return mountain0;
      break;
      case Block.STONE: return mountain0;
      break;
      case Block.LOCK: return home1;
      break;
      default:
      currentStair++;
      return stairDirection(dataObj.stairs[currentStair].dir);
      break;
    }
  }

  private string stairDirection(Direction dir){
    switch(dir){
      case Direction.UP: return stairFront;
      break;
      case Direction.DOWN: return stairBack;
      break;
      case Direction.LEFT: return stairLeft;
      break;
      default: return stairRight;
      break;
    }
  }
  private void instantiation(){
    float length = dataObj.grid.GetLength(1)*0.5f;
    float heigth = dataObj.grid.GetLength(0)*0.25f;
    GameObject tileObject1  = Instantiate(Resources.Load(player1), new Vector3(0-length,0-heigth, 0), Quaternion.identity) as GameObject;
    tileObject1.transform.parent = this.transform;
    GameObject tileObject2  = Instantiate(Resources.Load(player1), new Vector3(0+length,0-heigth, 0), Quaternion.identity) as GameObject;
    tileObject2.transform.parent = this.transform;
    GameObject tileObject3  = Instantiate(Resources.Load(player1), new Vector3(0-length,0+heigth, 0), Quaternion.identity) as GameObject;
    tileObject3.transform.parent = this.transform;
    GameObject tileObject4  = Instantiate(Resources.Load(player1), new Vector3(0+length,0+heigth, 0), Quaternion.identity) as GameObject;
    tileObject4.transform.parent = this.transform;
    float x=0f;
    float y =0f;
    for(int i =0; i<dataObj.grid.GetLength(1); i++){
      x = i*0.5f;
      y = i*0.25f*-1f;
      for(int j = 0; j<dataObj.grid.GetLength(0); j++){
        mapInstantiation(dataObj.grid[j,i], dataObj.levels[j,i], x, y, i, j);
        x -=0.5f;
        y -= 0.25f;
      }
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
