using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JsonBridge {

  public class DataConvert
  {

    public DataSerialized dataSer;
    public Data dataObj;

    public DataConvert(DataSerialized dataSer, Data dataObj){
      this.dataSer = dataSer;
      this.dataObj = dataObj;
    }
    /*
    ** dataSer contains all data serializable
    ** dataObj contains all data for the constructions of the playground
    ** Update the dataObj with the dataSer's values
    */
    public void objectToSerialized(){

      if(dataObj.type != null){
        dataSer.type = dataObj.type;
      }
      if(dataObj.code != null){
        dataSer.code = dataObj.code;
      }

      objToSer2DArrays();

      if(dataObj.players != null){
        objToSerPlayers();
      }

      if(dataObj.portals != null){
        objToSerPortals();
      }

      if(dataObj.stairs != null){
        objToSerStairs();
      }

      if(dataObj.locks != null){
        objToSerLocks();
      }
    }
    public void objectToSerialized0(){
      //Length and depth of the map
      int length = dataObj.grid.GetLength(0);
      int depth =  dataObj.grid.GetLength(1);

      //Initialisation of the attributes of the DataSerialized
      dataSer.grid = new string[length,depth];
      //dataSer.layout = new string[length,depth];
      //dataSer.colors = new string[length,depth];

      dataSer.levels = new int[length,depth];

      //dataSer.portals = new Portal[dataObj.portals.Length];

      dataSer.players = new PlayerSerialized[dataObj.players.Length];

      dataSer.type = dataObj.type;
      dataSer.code = dataObj.code;

      //dataSer.locks = new Lock[dataObj.locks.Length];
      dataSer.stairs = new StairSerialized[dataObj.stairs.Length];


      //Start conversion
      for(int i = 0; i<length; i++){
        for(int j = 0; j<depth; j++){
          dataSer.grid[i,j] = blockToString(dataObj.grid[i,j]);
          //dataSer.layout[i,j] = itemToString(dataObj.layout[i,j]);
          //dataSer.colors[i,j] = colorToString(dataObj.colors[i,j]);
          dataSer.levels[i,j] = dataObj.levels[i,j];
        }
      }
      /*for(int i = 0; i<dataSer.portals.Length; i++){
      dataSer.portals[i] = dataObj.portals[i];
      }*/
      for(int i = 0; i<dataSer.players.Length; i++){
        int id = dataObj.players[i].id;
        int x = dataObj.players[i].x;
        int y = dataObj.players[i].y;
        string dir = directionToString(dataObj.players[i].dir);
        string role = roleToString(dataObj.players[i].role);
        int stamina = dataObj.players[i].stamina;
        dataSer.players[i] = new PlayerSerialized(id, x, y, dir, role, stamina);
      }
      /*for(int i = 0; i<dataSer.locks.Length; i++){
      dataSer.locks[i] = dataObj.locks[i];
      }*/
      for(int i = 0; i<dataSer.stairs.Length; i++){
        int x = dataObj.stairs[i].coo.x;
        int y = dataObj.stairs[i].coo.y;
        string dir = directionToString(dataObj.stairs[i].dir);
        dataSer.stairs[i] = new StairSerialized(x, y, dir);
      }

    }

    private void objToSer2DArrays(){

      //Length and depth of the map
      int length = dataObj.grid.GetLength(0);
      int depth =  dataObj.grid.GetLength(1);

      //Initialisation of the attributes of the DataSerialized
      dataSer.grid = new string[length,depth];
      //dataSer.layout = new string[length,depth];
      //dataSer.colors = new string[length,depth];

      dataSer.levels = new int[length,depth];


      //Start conversion
      for(int i = 0; i<length; i++){
        for(int j = 0; j<depth; j++){
          dataSer.grid[i,j] = blockToString(dataObj.grid[i,j]);
          //dataSer.layout[i,j] = itemToString(dataObj.layout[i,j]);
          //dataSer.colors[i,j] = colorToString(dataObj.colors[i,j]);

          dataSer.levels[i,j] = dataObj.levels[i,j];

        }
      }
    }

    private void objToSerPlayers(){
      dataSer.players = new PlayerSerialized[dataObj.players.Length];
      for(int i = 0; i<dataSer.players.Length; i++){
        int id = dataObj.players[i].id;
        int x = dataObj.players[i].x;
        int y = dataObj.players[i].y;
        string dir = directionToString(dataObj.players[i].dir);
        string role = roleToString(dataObj.players[i].role);
        int stamina = dataObj.players[i].stamina;
        dataSer.players[i] = new PlayerSerialized(id, x, y, dir, role, stamina);
      }
    }

    private void objToSerStairs(){
      dataSer.stairs = new StairSerialized[dataObj.stairs.Length];
      for(int i = 0; i<dataSer.stairs.Length; i++){
        int x = dataObj.stairs[i].coo.x;
        int y = dataObj.stairs[i].coo.y;
        string dir = directionToString(dataObj.stairs[i].dir);
        dataSer.stairs[i] = new StairSerialized(x, y, dir);
      }
    }

    private void objToSerPortals(){
      dataSer.portals = new Portal[dataObj.portals.Length];
      for(int i = 0; i<dataSer.portals.Length; i++){
        dataSer.portals[i] = dataObj.portals[i];
      }
    }

    private void objToSerLocks(){
      dataSer.locks = new Lock[dataObj.locks.Length];
      for(int i = 0; i<dataSer.locks.Length; i++){
        dataSer.locks[i] = dataObj.locks[i];
      }
    }

    /*
    ** dataObj contains all data for the constructions of the playground
    ** dataSer contains all data serializable
    ** Update the dataSer with the dataObj's values
    */
    public void serializedToObject1(){

      setToObj2DArrays();

      if(dataSer.players != null){
        setToObjPlayers();
      }

      if(dataSer.stairs != null){
        setToObjStairs();
      }

      if(dataSer.portals != null){
        setToObjPortals();
      }

      if(dataSer.locks != null){
        setToObjLocks();
      }
    }

    public void serializedToObject(){

      //Length and depth of the map
      int length = dataSer.grid.GetLength(0);
      int depth =  dataSer.grid.GetLength(1);

      //Initialisation of the attributes of the DataSerialized
      dataObj.grid = new Block[length,depth];
      //dataObj.layout = new Item[length,depth];
      //dataObj.colors = new Color[length,depth];

      dataObj.levels = new int[length,depth];

      //dataObj.portals = new Portal[dataSer.portals.Length];
      dataObj.players = new Player[dataSer.players.Length];

      dataObj.type = dataSer.type;
      dataObj.code = dataSer.code;
      /*if(dataSer.locks != null){
      dataObj.locks = new Lock[dataSer.locks.Length];
      }*/
      if(dataSer.stairs != null){
        dataObj.stairs = new Stair[dataSer.stairs.Length];
      }


      //Start conversion
      for(int i = 0; i<length; i++){

        for(int j = 0; j<depth; j++){
          dataObj.grid[i,j] = stringToBlock(dataSer.grid[i,j]);
          //dataObj.layout[i,j] = stringToItem(dataSer.layout[i,j]);
          //dataObj.colors[i,j] = stringToColor(dataSer.colors[i,j]);
          dataObj.levels[i,j] = dataSer.levels[i,j];
        }
      }
      /*for(int i = 0; i<dataObj.portals.Length; i++){
      dataObj.portals[i] = dataSer.portals[i];
      }*/
      for(int i = 0; i<dataObj.players.Length; i++){
        int id = dataSer.players[i].id;
        int x = dataSer.players[i].x;
        int y = dataSer.players[i].y;
        Direction dir = stringToDirection(dataSer.players[i].dir);
        Role role = stringToRole(dataSer.players[i].role);
        int stamina = dataSer.players[i].stamina;
        dataObj.players[i] = new Player(id, x, y, dir, role, stamina);
      }
      /*if(dataSer.locks != null){
      for(int i = 0; i<dataObj.locks.Length; i++){
      dataObj.locks[i] = dataSer.locks[i];
    }
    }*/
    if(dataSer.stairs != null){
      for(int i = 0; i<dataObj.stairs.Length; i++){
        int x = dataSer.stairs[i].coo.x;
        int y = dataSer.stairs[i].coo.y;
        Direction dir = stringToDirection(dataSer.stairs[i].dir);
        dataObj.stairs[i] = new Stair(x, y, dir);
      }
    }

  }

  private void setToObj2DArrays(){

    //Length and depth of the map
    int length = dataSer.grid.GetLength(0);
    int depth =  dataSer.grid.GetLength(1);

    //Initialisation of the attributes of the DataSerialized
    dataObj.grid = new Block[length,depth];
    //dataObj.layout = new Item[length,depth];
    //dataObj.colors = new Color[length,depth];

    dataObj.levels = new int[length,depth];

    //Start conversion
    for(int i = 0; i<length; i++){

      for(int j = 0; j<depth; j++){
        dataObj.grid[i,j] = stringToBlock(dataSer.grid[i,j]);
        //dataObj.layout[i,j] = stringToItem(dataSer.layout[i,j]);
        //dataObj.colors[i,j] = stringToColor(dataSer.colors[i,j]);
        dataObj.levels[i,j] = dataSer.levels[i,j];
      }
    }
  }

  private void setToObjPlayers(){
    dataObj.players = new Player[dataSer.players.Length];
    for(int i = 0; i<dataObj.players.Length; i++){
      int id = dataSer.players[i].id;
      int x = dataSer.players[i].x;
      int y = dataSer.players[i].y;
      Direction dir = stringToDirection(dataSer.players[i].dir);
      Role role = stringToRole(dataSer.players[i].role);
      int stamina = dataSer.players[i].stamina;
      dataObj.players[i] = new Player(id, x, y, dir, role, stamina);
    }
  }

  private void setToObjStairs(){
    dataObj.stairs = new Stair[dataSer.stairs.Length];
    for(int i = 0; i<dataObj.stairs.Length; i++){
      int x = dataSer.stairs[i].coo.x;
      int y = dataSer.stairs[i].coo.y;
      Direction dir = stringToDirection(dataSer.stairs[i].dir);
      dataObj.stairs[i] = new Stair(x, y, dir);
    }
  }

  private void setToObjPortals(){
    dataObj.portals = new Portal[dataSer.portals.Length];
    for(int i = 0; i<dataObj.portals.Length; i++){
      dataObj.portals[i] = dataSer.portals[i];
    }
  }

  private void setToObjLocks(){
    dataObj.locks = new Lock[dataSer.locks.Length];
    for(int i = 0; i<dataObj.locks.Length; i++){
      dataObj.locks[i] = dataSer.locks[i];
    }
  }

  public string directionToString(Direction dir){
    string res = "directionToString error";

    switch(dir){
      case Direction.UP: res = "UP";
      break;
      case Direction.DOWN: res = "DOWN";
      break;
      case Direction.LEFT: res = "LEFT";
      break;
      case Direction.RIGHT: res = "RIGHT";
      break;
      default:  Debug.Log("Unknown Direction");
      break;
    }
    return res;
  }

  public Direction stringToDirection(string dir){

    Direction res;

    switch(dir){
      case "UP": res = Direction.UP;
      break;
      case "DOWN": res = Direction.DOWN;
      break;
      case "LEFT": res = Direction.LEFT;
      break;
      default:  res = Direction.RIGHT;
      break;
    }
    return res;
  }

  public string roleToString(Role role){
    string res = "roleToString error";

    switch(role){
      case Role.PLAYER: res = "PLAYER";
      break;
      case Role.SPECIALIST: res = "SPECIALIST";
      break;
      default: Debug.Log("Unknown Role");
      break;
    }
    return res;
  }

  public Role stringToRole(string role){
    Role res;

    switch(role){
      case "PLAYER": res = Role.PLAYER;
      break;
      default: res = Role.SPECIALIST;
      break;
    }
    return res;
  }

  public string blockToString(Block block){
    string res = "blockToString error";

    switch(block){
      case Block.OPEN: res = "OPEN";
      break;
      case Block.BLOCKED: res = "BLOCKED";
      break;
      case Block.WATER: res = "WATER";
      break;
      case Block.TREE: res = "TREE";
      break;
      case Block.DESERT: res = "DESERT";
      break;
      case Block.HOME: res = "HOME";
      break;
      case Block.MOUNTAIN: res = "MOUNTAIN";
      break;
      case Block.STONE: res = "STONE";
      break;
      case Block.LOCK: res = "LOCK";
      break;
      case Block.STAIR: res = "STAIR";
      break;
      default: Debug.Log("Unknown Block");
      break;
    }
    return res;
  }

  public Block stringToBlock(string block){

    Block res;

    switch(block){
      case "OPEN": res = Block.OPEN;
      break;
      case "BLOCKED": res = Block.BLOCKED;
      break;
      case "WATER": res = Block.WATER;
      break;
      case "TREE": res = Block.TREE;
      break;
      case "DESERT": res = Block.DESERT;
      break;
      case "HOME": res = Block.HOME;
      break;
      case "MOUNTAIN": res = Block.MOUNTAIN;
      break;
      case "STONE": res = Block.STONE;
      break;
      case "LOCK": res = Block.LOCK;
      break;
      default: res = Block.STAIR;
      break;
    }
    return res;
  }

  public string itemToString(Item item){
    string res = "itemToString error";

    switch(item){
      case Item.NONE: res = "NONE";
      break;
      case Item.GEM: res = "GEM";
      break;
      case Item.CLOSEDSWITCH: res = "CLOSEDSWITCH";
      break;
      case Item.OPENEDSWITCH: res = "OPENEDSWITCH";
      break;
      case Item.BEEPER: res = "BEEPER";
      break;
      case Item.PORTAL: res = "PORTAL";
      break;
      case Item.PLATFORM: res = "PLATFORM";
      break;
      default: Debug.Log("Unknown Item");
      break;
    }
    return res;
  }

  public Item stringToItem(string item){
    Item res;

    switch(item){
      case "NONE": res = Item.NONE;
      break;
      case "GEM": res = Item.GEM;
      break;
      case "CLOSEDSWITCH": res = Item.CLOSEDSWITCH;
      break;
      case "OPENEDSWITCH": res = Item.OPENEDSWITCH;
      break;
      case "BEEPER": res = Item.BEEPER;
      break;
      case "PORTAL": res = Item.PORTAL;
      break;
      default: res = Item.PLATFORM;
      break;
    }
    return res;
  }

  public string colorToString(Color color){
    string res = "colorToString error";

    switch(color){
      case Color.BLACK: res = "BLACK";
      break;
      case Color.SILVER: res = "SILVER";
      break;
      case Color.GREY: res = "GREY";
      break;
      case Color.WHITE: res = "WHITE";
      break;
      case Color.RED: res = "RED";
      break;
      case Color.ORANGE: res = "ORANGE";
      break;
      case Color.GOLD: res = "GOLD";
      break;
      case Color.PINK: res = "PINK";
      break;
      case Color.YELLOW: res = "YELLOW";
      break;
      case Color.BEIGE: res = "BEIGE";
      break;
      case Color.BROWN: res = "BROWN";
      break;
      case Color.GREEN: res = "GREEN";
      break;
      case Color.AZURE: res = "AZURE";
      break;
      case Color.CYAN: res = "CYAN";
      break;
      case Color.ALICEBLUE: res = "ALICEBLUE";
      break;
      case Color.PURPLE: res = "PURPLE";
      break;
      default: Debug.Log("Unknown Color");
      break;
    }
    return res;
  }

  public Color stringToColor(string color){
    Color res;

    switch(color){
      case "BLACK": res = Color.BLACK;
      break;
      case "SILVER": res = Color.SILVER;
      break;
      case "GREY": res = Color.GREY;
      break;
      case "WHITE": res = Color.WHITE;
      break;
      case "RED": res = Color.RED;
      break;
      case "ORANGE": res = Color.ORANGE;
      break;
      case "GOLD": res = Color.GOLD;
      break;
      case "PINK": res = Color.PINK;
      break;
      case "YELLOW": res = Color.YELLOW;
      break;
      case "BEIGE": res = Color.BEIGE;
      break;
      case "BROWN": res = Color.BROWN;
      break;
      case "GREEN": res = Color.GREEN;
      break;
      case "AZURE": res = Color.AZURE;
      break;
      case "CYAN": res = Color.CYAN;
      break;
      case "ALICEBLUE": res = Color.ALICEBLUE;
      break;
      default: res = Color.PURPLE;
      break;
    }
    return res;
  }

}

}
