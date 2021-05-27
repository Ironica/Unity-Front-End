using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using JsonBridge;
using Newtonsoft.Json;

public class dataLink : MonoBehaviour
{

  public JsonBridge.DataSerialized dataSer;
  public Data dataObj;
  public JsonBridge.DataConvert converter;
  private JsonBridge.JsonSerDes des = new JsonBridge.JsonSerDes(url);
  private JsonBridge.DataSerialized[] payload;
  private string currentMap;
  GameObject[,] gridObject;
  List<GameObject> gemObjects = new List<GameObject>();
  GameObject playerObject;

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
  private string playerFront = "Prefabs/ITEM/CHARACTER_FRONT";
  private string playerBack = "Prefabs/ITEM/CHARACTER_BACK";
  private string playerLeft = "Prefabs/ITEM/CHARACTER_LEFT";
  private string playerRight = "Prefabs/ITEM/CHARACTER_RIGHT";
  
  // Items
  private string gem = "Prefabs/ITEM/GEM";


  public const string url = "http://127.0.0.1:8080/paidiki-xara";

  private const string pathStarterMap = "Assets/Resources/MapJson/StarterMap/";
  private const string pathCurrentMap = "Assets/Resources/MapJson/CurrentMap/";

  public string mapName;

  private string getUserCode(){
    return GameObject.Find("UserCode")
    .GetComponent<InputField>()
    .text;
  }

  private DataSerialized[] convertToOriginalArray(DataResponseSerialized[] drs)
    => drs.Select(e => new DataSerialized()
    {
      grid = e.grid,
      layout = e.itemLayout,
      colors = e.colors,
      levels = e.levels,
      portals = e.portals,
      players = e.players,
      type = e.type,
      code = e.code,
      locks = e.locks,
      stairs = e.stairs
    }).ToArray();

  /*
  **  Method called when the user compiles his code
  */
  public async void compile(){

    /*if( the frame[] is empty )
    */

    // Get the user code in the InputField
    dataObj.code = getUserCode();
    Debug.Log(dataObj.code);

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
    payload = convertToOriginalArray(answers.payload);
    Debug.Log("Number frame " + payload.Length);
    for(int i = 0; i< payload.Length; i++){
      converter.dataSer = payload[i];
      string json = JsonConvert.SerializeObject(converter.dataSer, Formatting.Indented);
      File.WriteAllText(pathCurrentMap + currentMap ,json);
      converter.serializedToObject();

      foreach (Transform child in this.gameObject.transform.GetChild(3).GetChild(0)) {
        Destroy(child.gameObject);
      }
      instantiation();
      //Sleep for 1 seconds
      await Task.Delay(1000);
      //Debug.Log("Frame " + i);
    }

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

  private string playerDirection(Direction dir){
    switch(dir){
      case Direction.UP: return playerFront;
      case Direction.DOWN: return playerBack;
      case Direction.LEFT: return playerLeft;
      default: return playerRight;
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

  private GameObject mapInstantiation(GameObject obj, Block block, int level, int x, int y, int i, int j){
    string tile = tileLevel(block, i, j);
    Vector2 coo = setCoordinates(new Vector2(j-x, i-y));
    obj  = Instantiate(UnityEngine.Resources.Load(tile), new Vector3(coo.x,coo.y, 0), Quaternion.identity) as GameObject;
    obj.transform.parent = this.gameObject.transform.GetChild(3).GetChild(0);
    return obj;
  }


  private void playerInstantiation(GameObject tile, Player player){
    float playerLevel = 0.25f;
    string playerPrefab = playerDirection(player.dir);
    int level = dataObj.levels[player.y,player.x];
    playerLevel += (level-1)*0.4f;
    Vector3 coo = new Vector3(tile.transform.position.x, tile.transform.position.y+playerLevel, 0);

    playerObject = Instantiate(UnityEngine.Resources.Load(playerPrefab), coo, Quaternion.identity) as GameObject;
    playerObject.transform.parent = this.gameObject.transform.GetChild(3).GetChild(0);
    playerObject.GetComponent<SpriteRenderer>().sortingOrder = level;

  }

  private void GemInstantiation(GameObject tile, Gem gemObj)
  {
    var gemLevel = 0.25f;
    var gemPrefab = gem;
    var level = dataObj.levels[gemObj.X, gemObj.Y];
    gemLevel += (level - 1) * 0.4f;
    var tilePos = tile.transform.position;
    var coo = new Vector3(tilePos.x, tilePos.y + gemLevel, 0);

    var gemObject = Instantiate(UnityEngine.Resources.Load(gemPrefab), coo, Quaternion.identity) as GameObject;
    gemObject.transform.parent = this.gameObject.transform.GetChild(3).GetChild(0);
    gemObject.GetComponent<SpriteRenderer>().sortingOrder = level;
    gemObjects.Add(gemObject);
  }

  private void instantiation(){
    gridObject = new GameObject[dataObj.grid.GetLength(0), dataObj.grid.GetLength(1)];

    int x = dataObj.grid.GetLength(1)/2;
    int y =dataObj.grid.GetLength(0)/2;

    for(int i =0; i<dataObj.grid.GetLength(1); i++){
      for(int j = 0; j<dataObj.grid.GetLength(0); j++){
        gridObject[j,i] = mapInstantiation(gridObject[j,i], dataObj.grid[j,i], dataObj.levels[j,i], x, y, i, j);
        if (dataObj.layout[j, i] == Item.GEM) { GemInstantiation(gridObject[j, i], new Gem(j, i));}
      }
    }
    for(int i=0; i<dataObj.players.Length; i++){
      Player player = dataObj.players[i];

      playerInstantiation(gridObject[player.y, player.x], player);
    }
  }

  // Start is called before the first frame update
  void Start()
  {

    currentMap = "map4.json";

    dataSer = des.deserialization(pathStarterMap + currentMap);

    dataObj = new Data();

    converter = new JsonBridge.DataConvert(dataSer, dataObj);
    converter.serializedToObject();

    //string response  = des.serialization(dataSer, pathStarterMap + currentMap);

    instantiation();

  }

  // Update is called once per frame
  void Update()
  {

  }
}
