using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id;

    public int x;
    public int y;

    public Direction dir;

    public Role role;

    public int stamina;

    public Player(int id, int x, int y, Direction dir, Role role, int stamina){
      this.id = id;
      this.x = x;
      this.y = y;
      this.dir = dir;
      this.role = role;
      this.stamina = stamina;
    }

}
