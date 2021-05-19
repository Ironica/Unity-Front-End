using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Data{

  //Common Part of the Playground's Incoming / Outgoing Data
  public Block[,] grid;
  public Item[,] layout;
  public Color[,] colors;
  public int[,] levels;
  public Portal[] portals;
  public Player[] players;

  //The Incoming (from Front-end to Compiler) Data Structure
  public string type;
  public string code;
  public Lock[] locks;
  public Stair[] stairs;

  public Data(){

    grid = new Block[,]{{Block.OPEN, Block.BLOCKED},{Block.OPEN, Block.WATER}};

    layout = new Item[,]{{Item.GEM, Item.BEEPER},{Item.PORTAL, Item.PLATFORM}};

    colors = new Color[,]{{Color.BLACK, Color.GREY},{Color.WHITE, Color.RED}};

    levels = new int[,]{{1,2}, {3,4}};

    portals = new Portal[1];
    portals[0] = new Portal(new Coordinates(1, 4), new Coordinates(3,6));

    players = new Player[1];
    players[0] = new Player();

    type = "colorfulmountainous";

    code = "UserCode";

    locks = new Lock[]{new Lock(new Coordinates(3,3), new Coordinates[2]{new Coordinates(2,90), new Coordinates(1,6)})};

    stairs = new Stair[]{new Stair(new Coordinates(9,76), Direction.LEFT), new Stair(new Coordinates(12,26), Direction.DOWN)};

    Debug.Log(blockConvert(0));
  }

  public string blockConvert(int block){
    switch ((Block)block)
    {
      case Block.OPEN: return "OPEN";
      break;
      case Block.BLOCKED: return "BLOCKED";
      break;
      case Block.WATER: return "WATER";
      break;
      case Block.TREE: return "TREE";
      break;
      case Block.DESERT: return "DESERT";
      break;
      case Block.HOME: return "HOME";
      break;
      case Block.MOUNTAIN: return "MOUNTAIN";
      break;
      case Block.STONE: return "STONE";
      break;
      case Block.LOCK: return "LOCK";
      break;
      case Block.STAIR: return "STAIR";
      break;
      default: Debug.Log("Unknown Block");
      break;

    }
    return "Error";
  }
}
