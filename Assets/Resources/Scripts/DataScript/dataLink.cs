using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;
using JsonBridge;
using Newtonsoft.Json;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
// Test
public class dataLink : MonoBehaviour
{

  //To connect to the server
  private const string url = "http://127.0.0.1";
  private const string api = "simulatte";

  //Data objects
  public JsonBridge.DataOutSerialized dataSer;
  public Data dataObj;
  public DataMap dataMap;

  //Object for convertion of data
  public JsonBridge.DataConvert converter;

  //Object to serialize and deserialize
  private JsonBridge.JsonSerDes des;

  //Server response object
  private List<JsonBridge.DataOutSerialized> payloads = new List<DataOutSerialized>();

  //The name of the displayed map
  private string currentMap;

  //The progress bar component
  private Slider progression;

  //Empty parent objects of gameObject
  private GameObject gameBoard;
  private GameObject tiles;

  //All GameObject on the screen
  GameObject[,] gridObject;
  GameObject playerObject;
  List<GameObject> gemObjects         = new List<GameObject>();
  List<GameObject> switchObjects      = new List<GameObject>();

  //Prefabricated tiles
  private string open                 = "Prefabs/PLAIN_OPEN_LEVEL";
  private string home                 = "Prefabs/PLAIN_HOME_LEVEL";
  private string mountain             = "Prefabs/PLAIN_MOUNTAIN_LEVEL";
  private string desert               = "Prefabs/PLAIN_DESERT_LEVEL";
  private string tree                 = "Prefabs/PLAIN_TREE_LEVEL";
  private string water                = "Prefabs/PLAIN_WATER_LEVEL";
  private string hill                 = "Prefabs/PLAIN_HILL_LEVEL";

  // Stairs
  private string  stairLeft           = "Prefabs/PLAIN_STAIRS_LEFT";
  private string  stairRight          = "Prefabs/PLAIN_STAIRS_RIGHT";
  private string  stairBack           = "Prefabs/PLAIN_STAIRS_BACK";
  private string  stairFront          = "Prefabs/PLAIN_STAIRS_FRON";

  //Prefabricated players
  private string player;
  private string police               = "Prefabs/ITEM/CHARACTER";
  private string frog                 = "Prefabs/ITEM/FROG";
  private string playerFront          = "_FRONT";
  private string playerBack           = "_BACK";
  private string playerLeft           = "_LEFT";
  private string playerRight          = "_RIGHT";

  //Prefabricated items
  private string gem                  = "Prefabs/ITEM/GEM";
  private string switchOn             = "Prefabs/ITEM/SWITCH_ON";
  private string switchOff            = "Prefabs/ITEM/SWITCH_OFF";

  //Maps folder
  private const string pathStarterMap = "Assets/Resources/MapJson/StarterMap/";

  //Inventory, with all the gems collected
  private Inventory pocket;

  /*
  * * * * Data Flow for DataLink Class and Compile() Method * * * *
  *
  *                                       User Input
  *                                         ↓
  *                                         ↓       JsonSerDes.convertDataSerializedToOutgoingData
  * ┌──────────────┐                     ┌───────────────────┐             ┌───────────────────┐                       ╔═════════════════╗
  * │              │  serializeToObject  │                   │             │                   │ webDeserialization    ║                 ║
  * │ this.dataObj │ ←────────────────── │   this.dataSer    │───────────→ │                   │ →→→→→→→→→→→→→→→→→→→→→ ║ Back-end Server ║
  * │ ──────────── │    (initialize)     │ ───────────────── │             │ RealDataOutSerial.│ (send request to srv) ║                 ║
  * │     Data     │                     │ DataOutSerialized │             │                   │                       ╚═════════════════╝
  * │              │ ←─┐                 │                   │             │                   │
  * └──────────────┘   │                 └───────────────────┘             └───────────────────┘                                ↓
  *                    │                                                                                                        ↓
  *  serializeToObject │                                                                             Receive response as rspAns ↓
  *                    │                                                                                 ←←←←←←←←←←←←←←←←←←←←←←←←
  *   (for each frame) │                                                                                 ↓
  *                    │                                                                                 ↓
  *
  *       ┌───────────────────┐           ┌─────────────────────┐                              ┌─────────────────────────┐
  *       │                   │           │                     │                              │                         │
  *       │      payload      │  forEach  │      payloads       │ appendPayloadToDataOutLayout │     rspAns.payload      │
  *       │ ───────────────── │ ←──────── │ ─────────────────── │ ←─────────────────────────── │ ─────────────────────── │
  *       │ DataOutSerialized │           │ DataOutSerialized[] │  Array.Select() conversion   │ DataPayloadSerialized[] │
  *       │                   │           │                     │                              │                         │
  *       └───────────────────┘           └─────────────────────┘                              └─────────────────────────┘
  *
  * - this.dataObj is the object that connected to the display of playground, it will mutate at each frame
  * - this.dataSer contains the initial map info, the only change applied to it is at each time the user enters
  *   a code. In the future we should have the ability to modify this.dataSer in order to change the map.
  *   But at the moment it shouldn't be modified unless when the user update his code.
  *
  * - the appendPayloadToDataOutLayout() method receives a frame of rspAns.payload, but also the this.dataSer object,
  *   in order to concatenate a full playground info using both info received from the server and that of this.dataSer
  *   We use an Array.Select(Callback) method (equiv. Array.stream().map(callback) in Java) to apply this method to
  *   every frame of rspAns.
  * - As you can see, payloads has type DataOutSerialized[] and payload has therefore DataOutSerialized, which correspond
  *   to this.dataSer. The SerializeToObject() method is therefore applicable to both side of this.dataSer and payload.
  * - We just need to call SerializeToObject() method in a loop for each payload of the payloads to update the dataObj.
  *
  */

  /**
  * This method returns the user's input
  */
  private string getUserCode()
  {
    return GameObject.Find("UserCode")
    .GetComponent<InputField>()
    .text;
  }

  /**
  * This method converts DataPayloadSerialized object to DataOutSerialized object, by appending info from the original
  * map (dataSer: DataOutSerialized)
  */
  private static DataOutSerialized AppendPayloadInfoToDataOutLayout(DataOutSerialized dos, DataPayloadSerialized dps)
  => new DataOutSerialized
  {
    type = dos.type, code = dos.code,
    grid = dps.grid.Select((l, j) =>
    l.Select((e, i) =>
    new GridObject(dos.grid[j][i].block, dos.grid[j][i].biome, e.level)).ToArray()).ToArray(),
    gems = dps.gems, beepers = dps.beepers,
    switches = dps.switches, portals = dps.portals, locks = dps.locks, platforms = dps.platforms,
    stairs = dos.stairs,
    players = dps.players,
    special = dps.special,
    consoleLog = dps.consoleLog
  };

  /**
  * Method called when user click on Reset.
  */
  public void OnResetClick()
  {
    payloads.Clear();
    this.dataSer.code = "";
    GameObject.Find("UserCode").GetComponent<InputField>().text = "";
    progression.value = 0;
    // Resetting the map means reinitialize it.
    converter.dataSer = dataSer;
    converter.serializedToObject();

    foreach (Transform child in gameBoard.transform)
    {
      Destroy(child.gameObject); // Destroy last frame
    }

    instantiation(false);
  }


  public void getGem(Gem[] gems){

  }
  /*
  **  Method called when the user compiles his code
  */
  public async void compile()
  {

    progression.value = 0;

    /*if( the frame[] is empty )
    */

    // Get the user code in the InputField
    dataSer.code = getUserCode(); // Since this.dataSer doesn't change i.e the map doesn't change, we could use it.
    // Should we remove the following lines of code before "Data conversion done"?
    // dataObj.code = getUserCode();

    //Convert the data object to data serializable
    // converter.dataObj = this.dataObj;
    // converter.objectToSerialized();

    Debug.Log("Data conversion done");

    //Convert the data to json format
    //Send the json file to the server
    //Get the response from the server
    string resp = await Task.Run(() => des.serialization(dataSer));
    // use of the async-await pattern so that the UI won't be blocked
    // in the future could we add some more features during the awaiting time, e.g. a banner, or some notification or some animation???


    //Deserialization of the response
    var answers = des.webDeserialization(resp);

    //Print the status of the compilation
    Debug.Log("Status: " + answers.status);

    // Switch with the type of response
    switch (answers)
    {

      // In case of error, dump error info and return
      case ErrorMessageModel errAns:
      {
        Debug.LogError("Encountered an error in the back-end\n" + errAns.msg);
        return;
      }

      // All is okay, process conversion
      case ResponseModel rspAns:
      {
        //Get the frame array for the animation, convert from DataPayloadSerialized[] to DataOutSerialized[]
        payloads = rspAns.payload.Select(e => AppendPayloadInfoToDataOutLayout(dataSer, e)).ToList();
        Debug.Log("Number frame " + payloads.Count);

        progression.maxValue = payloads.Count;

        int gemsNumber = payloads[0].gems.Length;
        int switchesNumber = payloads[0].switches.Length;
        int monsterNumber = 0;

        var gems = 0;
        var switchOn = 0;



        // Loop into each payload to extract data and send to dataObj
        while (payloads.Count > 0)
        {
          var payload = payloads[0];
          gems = gemsNumber - payload.gems.Length;
          payloads.RemoveAt(0);

          //Debug.Log(payload.switches.Aggregate("", (str, sw) => $"{str}:{sw.on}:"));
          switchOn = payload.switches.Count(sw => sw.on == true);
          Debug.Log("switchOn: " + switchOn);
          //Debug.Log("Switch on: " + payload.switches.Aggregate(0, (cnt, sw) => cnt + (sw.on ? 1 : 0)));


          converter.dataSer = payload;
          var json = JsonConvert.SerializeObject(converter.dataSer, Formatting.Indented);
          converter.serializedToObject();

          foreach (Transform child in gameBoard.transform)
          {
            Destroy(child.gameObject); // Destroy last frame
          }

          instantiation(false); // call method on dataObj to update it

          progression.value += 1;

          string gemsScore = gems + "/" + gemsNumber;
          string switchesScore = switchOn + "/" + switchesNumber;
          string monsterScore = 0 + "/" + monsterNumber;

          GameObject ScoreBoard = gameObject.transform.Find("GameBoard").gameObject.transform.Find("ScoreBoard").gameObject as GameObject;
          ScoreBoard.transform.Find("GemScore").gameObject.GetComponent<Text>().text = gemsScore;
          ScoreBoard.transform.Find("SwitchScore").gameObject.GetComponent<Text>().text = switchesScore;
          ScoreBoard.transform.Find("KillScore").gameObject.GetComponent<Text>().text = monsterScore;

          //Sleep for 1 seconds
          await Task.Delay(250);
          //Debug.Log("Frame " + i);

        }

        Debug.Log("All frames executed successfully.");

        if(gems > dataMap.maxGem)
        {
          dataMap.maxGem = gems;
        }
        if(switchOn > dataMap.maxSwitchOn)
        {
          Debug.Log("Update switch");
          Debug.Log("switchOn: " + switchOn);
          Debug.Log("dataMap.maxSwitchOn: " + dataMap.maxSwitchOn);
          dataMap.maxSwitchOn = switchOn;
        }

        // Use this structure to handle the end of game procedure, e.g. you can add some layout and effects for different status
        var gameStatusMessage = rspAns.game switch
        {
          "WIN" => "You win! The game has completed successfully.",
          "LOST" => "You lost! The game has finished.",
          "PENDING" => "The game was not finished..",
          _ => throw new Exception("Unsupported game status")
        };

        if(true/*rspAns.game.Equals("WIN")*/){//TODO change when we will have the win system
          dataMap.win = true;
        }

        Debug.Log(gameStatusMessage);

        saveMap();

        return;
      }
      default: throw new Exception("This is impossible");
    }
  }

  private Vector2 setCoordinates(Vector2 vector){
    Vector2 i = new Vector2(-0.5f, -0.25f);
    Vector2 j = new Vector2(0.5f, -0.25f);
    return vector.x*i + vector.y*j;
  }

  private string stairDirection(Direction dir)
  => dir switch
  {
    Direction.UP => stairFront,
    Direction.DOWN => stairBack,
    Direction.LEFT => stairLeft,
    Direction.RIGHT => stairRight,
    _ => throw new Exception("This shouldn't be possible")
  };

  private string playerDirection(Direction dir)
  => dir switch
  {
    Direction.UP => player+playerFront,
    Direction.DOWN => player+playerBack,
    Direction.LEFT => player+playerLeft,
    Direction.RIGHT => player+playerRight,
    _ => throw new Exception("This shouldn't be possible")
  };

  private string tileLevel(Block block, int i, int j)
  => block switch
  {
    Block.OPEN => open + dataObj.grid[j][i].Level,
    Block.HOME => home + dataObj.grid[j][i].Level,
    Block.MOUNTAIN => mountain + dataObj.grid[j][i].Level,
    Block.DESERT => desert + dataObj.grid[j][i].Level,
    Block.TREE => tree + dataObj.grid[j][i].Level,
    Block.WATER => water + dataObj.grid[j][i].Level,
    Block.HILL => hill + dataObj.grid[j][i].Level,
    Block.STAIR => stairDirection(dataObj.stairs.First(e => e.X == j && e.Y == i).Dir),
    Block.VOID => throw new NotImplementedException(),
    _ => throw new Exception("This shouldn't be possible")
  };

  private GameObject mapInstantiation(GameObject obj, Block block, int level, int x, int y, int i, int j)
  {
    string tile = tileLevel(block, i, j);
    Vector2 coo = setCoordinates(new Vector2(j-x, i-y));
    obj  = Instantiate(UnityEngine.Resources.Load(tile), new Vector3(coo.x,coo.y, 0), Quaternion.identity) as GameObject;
    obj.transform.parent = tiles.transform;
    return obj;
  }

  private void GemInstantiation(GameObject tile, Gem gemObj)
  {
    var gemLevel = -0.35f;
    var gemPrefab = gem;
    var level = dataObj.grid[gemObj.Y][gemObj.X].Level;
    gemLevel += (level - 1) * 0.4f;
    var tilePos = tile.transform.position;
    var coo = new Vector3(tilePos.x, tilePos.y + gemLevel, 0);

    var gemObject = Instantiate(UnityEngine.Resources.Load(gemPrefab), coo, Quaternion.identity) as GameObject;
    gemObject.transform.parent = gameBoard.transform;
    gemObject.GetComponent<SpriteRenderer>().sortingOrder = level;
    gemObjects.Add(gemObject);
  }

  private void playerInstantiation(GameObject tile, Player player)
  {
    float playerLevel = 0.25f;
    string playerPrefab = playerDirection(player.Dir);
    int level = dataObj.grid[player.Y][player.X].Level;
    playerLevel += (level-1)*0.4f;
    Vector3 coo = new Vector3(tile.transform.position.x, tile.transform.position.y+playerLevel, 0);

    playerObject = Instantiate(UnityEngine.Resources.Load(playerPrefab), coo, Quaternion.identity) as GameObject;
    playerObject.transform.parent = gameBoard.transform;
    playerObject.GetComponent<SpriteRenderer>().sortingOrder = level+1;

  }


  private void switchInstantiation(GameObject tile, Switch switchObj)
  {
    var switchLevel = 0.00f;
    string switchPrefab = switchObj.On ? switchOn : switchOff;
    var level = dataObj.grid[switchObj.Y][switchObj.X].Level;
    switchLevel += (level-1) * 0.4f;
    var tilePos = tile.transform.position;
    var coo = new Vector3(tilePos.x, tilePos.y + switchLevel, 0);

    var switchObject = Instantiate(UnityEngine.Resources.Load(switchPrefab), coo, Quaternion.identity) as GameObject;
    switchObject.transform.parent = gameBoard.transform;
    switchObject.GetComponent<SpriteRenderer>().sortingOrder = level;
    switchObjects.Add(switchObject);
  }


  private void instantiation(bool tileInstantiation)
  {
    // Create Game Object
    // Note. Can only use array of GameObject[,], if we use GameObject[][] Unity will create lots of GameObject
    if(tileInstantiation)
    {
      gridObject = new GameObject[dataObj.grid.Length, dataObj.grid[0].Length];
    }

    var x = dataObj.grid[0].Length / 2;
    var y = dataObj.grid.Length / 2;

    // Since we use GameObject[,], sorry no Linq but only imperative loop
    if(tileInstantiation)
    {
      for (var i = 0; i < gridObject.GetLength(1); i++)
      {
        for (var j = 0; j < gridObject.GetLength(0); j++)
        {
          var tile = dataObj.grid[j][i];
          gridObject[j, i] = mapInstantiation(gridObject[j, i], tile.Block, tile.Level, x, y, i, j);
          //gridObject[j, i] = mapInstantiation(gridObject[j, i], dataIn.grid[j][i].block, tile.Level, x, y, i, j);

        }
      }
    }

    // In the future add here the instantiation procedures for beeper, switch, platform, portal, etc.
    // Normally they should follow the same pattern like gems instantiation.

    foreach (var gemCoo in dataObj.gems)
    {
      GemInstantiation(gridObject[gemCoo.y, gemCoo.x], new Gem(gemCoo.x, gemCoo.y));
    }

    // Debug.Log(dataObj.switches.Aggregate("", (str, sw) => $"{str}:{sw.On}:"));

    foreach (var switchCoo in dataObj.switches)
    {
      switchInstantiation(gridObject[switchCoo.Y, switchCoo.X], new Switch(switchCoo.X, switchCoo.Y, switchCoo.On));
    }

    foreach (var playerCoo in dataObj.players)
    {
      playerInstantiation(gridObject[playerCoo.Y, playerCoo.X], playerCoo);
    }

  }

  private void Awake()
  {

  }

  public void saveMap()
  {
    if(dataMap == null){
      dataMap = new DataMap(currentMap);
    }
    dataMap.code = GameObject.Find("UserCode")
    .GetComponent<InputField>()
    .text;
    SaveMapManager.saveData(dataMap);
  }

  public void loadMap()
  {
    var load = SaveMapManager.loadData(dataMap);
    if(load != null){
      dataMap.code = load.code;
      dataMap.maxGem = load.maxGem;
      dataMap.maxSwitchOn = load.maxSwitchOn;
      dataMap.win = load.win;
      GameObject.Find("UserCode")
      .GetComponent<InputField>()
      .text = dataMap.code;
    }

  }


  // Start is called before the first frame update
  private void Start()
  {

    gameBoard = gameObject.transform.Find("GameBoard").gameObject.transform.Find("Elements").gameObject as GameObject;
    tiles = gameObject.transform.Find("GameBoard").gameObject.transform.Find("Tiles").gameObject as GameObject;

    progression = gameObject.transform.Find("Progress_Bar").gameObject.GetComponent<Slider>();
    progression.value = 0;

    //TODO Get the name of the map from the maps interface
    currentMap = StatData.getCurrent();
    dataMap = new DataMap(currentMap);
    loadMap();
    //currentMap = "map5.json";

    player = frog;

    // Awake() will be called before Start() therefore we can use `port` initialized in Awake()
    des = new JsonSerDes(url, Global.port, api);

    //pocket = des.inventoryDeserialization();
    //Debug.Log(pocket.gems);


    dataSer = des.deserialization(pathStarterMap + currentMap);

    dataObj = new Data();

    converter = new JsonBridge.DataConvert(dataSer, dataObj);
    //converter.stringToSerialized();
    converter.serializedToObject();

    instantiation(true);

  }

  // TODO copy this method to each scene
  private void OnApplicationQuit()
  {
    var shutdownApi = "simulatte/shutdown";
    new ShutDown(shutdownApi, Global.port).ShutDownOldServer();

    foreach (Transform child in gameBoard.transform)
    {
      Destroy(child.gameObject); // Destroy last frame
    }
    foreach (Transform child in tiles.transform)
    {
      Destroy(child.gameObject); // Destroy last frame
    }
  }
}
