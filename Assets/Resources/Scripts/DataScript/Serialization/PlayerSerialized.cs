using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerSerialized
{
  public int id;

  public int x;
  public int y;

  public string dir;

  public string role;

  public int stamina;

  public PlayerSerialized(int id, int x, int y, string dir, string role, int stamina){
    this.id = id;

    this.x = x;
    this.y = y;

    /*DataConvert dc = new DataConvert();
    this.dir = dc.directionToString(player.dir);
    this.role = dc.roleToString(player.role);*/
    this.dir = dir;
    this.role = role;

    this.stamina = stamina;
  }
}
