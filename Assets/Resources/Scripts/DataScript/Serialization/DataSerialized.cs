using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonBridge{

  public class ResponseModel
  {
    public string status { get; set; }
    public DataSerialized[] payload { get; set; }
  }

  [System.Serializable]
  public class DataSerialized
  {
    //Common Part of the Playground's Incoming / Outgoing Data
    public string[,] grid;
    public string[,] layout;
    public string[,] colors;

    public int[,] levels;

    public Portal[] portals;

    public PlayerSerialized[] players;

    //The Incoming (from Front-end to Compiler) Data Structure
    public string type;
    public string code;
    public Lock[] locks;
    public StairSerialized[] stairs;

    public DataSerialized(){/*

      //Non serializable data with non string type
      Data data = new Data();

      //Class convertion for data's types to string
      DataConvert dc = new DataConvert();

      //Length and depth of the map
      int length = data.grid.GetLength(0);
      int depth =  data.grid.GetLength(1);

      //Initialisation of the attributes of the DataSerialized
      grid = new string[length,depth];
      layout = new string[length,depth];
      colors = new string[length,depth];

      levels = new int[length,depth];

      portals = new Portal[data.portals.Length];

      players = new PlayerSerialized[data.players.Length];

      type = data.type;
      code = data.code;

      locks = new Lock[data.locks.Length];
      stairs = new StairSerialized[data.stairs.Length];


      //Start conversion
      for(int i = 0; i<length; i++){
        for(int j = 0; j<depth; j++){
          this.grid[i,j] = dc.blockToString(data.grid[i,j]);
          this.layout[i,j] = dc.itemToString(data.layout[i,j]);
          this.colors[i,j] = dc.colorToString(data.colors[i,j]);

          this.levels[i,j] = data.levels[i,j];

        }
      }
      for(int i = 0; i<portals.Length; i++){
        portals[i] = data.portals[i];
      }
      for(int i = 0; i<players.Length; i++){
        int id = data.players[i].id;
        int x = data.players[i].x;
        int y = data.players[i].y;
        string dir = dc.directionToString(data.players[i].dir);
        string role = dc.roleToString(data.players[i].role);
        int stamina = data.players[i].stamina;
        players[i] = new PlayerSerialized(id, x, y, dir, role, stamina);
      }
      for(int i = 0; i<locks.Length; i++){
        locks[i] = data.locks[i];
      }
      for(int i = 0; i<stairs.Length; i++){
        int x = data.stairs[i].coo.x;
        int y = data.stairs[i].coo.y;
        string dir = dc.directionToString(data.stairs[i].dir);
        stairs[i] = new StairSerialized(x, y, dir);
      }

    */}


    public void PrintFrame()
    {
      /*string block="";
      for (int y = 0; y < grid.GetLength(0); y++)
      {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
          //PrintObject(x, y);
          block+=grid[y,x] + " ";
        }
        Debug.Log(block);
        block = "";
      }*/
      Debug.Log("New frame");
      for(int i = 0; i<players.Length; i++){
        Debug.Log(players[i].id + ",  x: " + players[i].x + ", y: " + players[i].y);
      }
    }
  }
}
