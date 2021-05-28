using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

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
      dataObj.layout = new Item[length,depth];
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
          dataObj.layout[i, j] = stringToItem(dataSer.layout[i, j]);

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

  public string directionToString(Direction dir)
    => dir switch
    {
      Direction.UP => "UP",
      Direction.DOWN => "DOWN",
      Direction.LEFT => "LEFT",
      Direction.RIGHT => "RIGHT",
      _ => throw new Exception("directionToString error")
    };

  public Direction stringToDirection(string dir)
    => dir switch
    {
      "UP" => Direction.UP,
      "DOWN" => Direction.DOWN,
      "LEFT" => Direction.LEFT,
      "RIGHT" => Direction.RIGHT,
      _ => throw new Exception("Unknown direction")
    };

  public string roleToString(Role role)
    => role switch
    {
      Role.PLAYER => "PLAYER",
      Role.SPECIALIST => "SPECIALIST",
      _ => throw new Exception("Unknown role [1]")
    };

  public Role stringToRole(string role)
    => role switch
    {
      "PLAYER" => Role.PLAYER,
      "SPECIALIST" => Role.SPECIALIST,
      _ => throw new Exception("Unknown role [2]")
    };

  public string blockToString(Block block)
    => block switch
    {
      Block.OPEN => "OPEN",
      Block.BLOCKED => "BLOCKED",
      Block.WATER => "WATER",
      Block.TREE => "TREE",
      Block.DESERT => "DESERT",
      Block.HOME => "HOME",
      Block.MOUNTAIN => "MOUNTAIN",
      Block.STONE => "STONE",
      Block.LOCK => "LOCK",
      Block.STAIR => "STAIR",
      _ => throw new Exception("Unknown block [1]")
    };

  public Block stringToBlock(string block)
    => block switch
    {
      "OPEN" => Block.OPEN,
      "BLOCKED" => Block.BLOCKED,
      "WATER" => Block.WATER,
      "TREE" => Block.TREE,
      "DESERT" => Block.DESERT,
      "HOME" => Block.HOME,
      "MOUNTAIN" => Block.MOUNTAIN,
      "STONE" => Block.STONE,
      "LOCK" => Block.LOCK,
      "STAIR" => Block.STAIR,
      _ => throw new Exception("Unknown block [2]")
    };

  public string itemToString(Item item)
    => item switch
    {
      Item.NONE => "NONE",
      Item.GEM => "GEM",
      Item.CLOSEDSWITCH => "CLOSEDSWITCH",
      Item.OPENEDSWITCH => "OPENEDSWITCH",
      Item.BEEPER => "BEEPER",
      Item.PORTAL => "PORTAL",
      Item.PLATFORM => "PLATFORM",
      _ => throw new Exception("Unknown item [1]")
    };

  public Item stringToItem(string item)
    => item switch
    {
      "NONE" => Item.NONE,
      "GEM" => Item.GEM,
      "CLOSEDSWITCH" => Item.CLOSEDSWITCH,
      "OPENEDSWITCH" => Item.OPENEDSWITCH,
      "BEEPER" => Item.BEEPER,
      "PORTAL" => Item.PORTAL,
      "PLATFORM" => Item.PLATFORM,
      _ => throw new Exception("Unknown item [2]")
    };

  public string colorToString(Color color)
    => color switch
    {
      Color.BLACK => "BLACK",
      Color.SILVER => "SILVER",
      Color.GREY => "GREY",
      Color.WHITE => "WHITE",
      Color.RED => "RED",
      Color.ORANGE => "ORANGE",
      Color.GOLD => "GOLD",
      Color.PINK => "PINK",
      Color.YELLOW => "YELLOW",
      Color.BEIGE => "BEIGE",
      Color.BROWN => "BROWN",
      Color.GREEN => "GREEN",
      Color.AZURE => "AZURE",
      Color.CYAN => "CYAN",
      Color.ALICEBLUE => "ALICEBLUE",
      Color.PURPLE => "PURPLE",
      _ => throw new Exception("Unknown color [1]")
    };

  public Color stringToColor(string color)
    => color switch
    {
      "BLACK" => Color.BLACK,
      "SILVER" => Color.SILVER,
      "GREY" => Color.GREY,
      "WHITE" => Color.WHITE,
      "RED" => Color.RED,
      "ORANGE" => Color.ORANGE,
      "GOLD" => Color.GOLD,
      "PINK" => Color.PINK,
      "YELLOW" => Color.YELLOW,
      "BEIGE" => Color.BEIGE,
      "BROWN" => Color.BROWN,
      "GREEN" => Color.GREEN,
      "AZURE" => Color.AZURE,
      "CYAN" => Color.CYAN,
      "ALICEBLUE" => Color.ALICEBLUE,
      "PURPLE" => Color.PURPLE,
      _ => throw new Exception("Unknown color [2]")
    };

  }

}
