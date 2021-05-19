using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Player
{
    public int id;

    public int x;
    public int y;

    public Direction dir;

    public Role role;

    public int stamina;

    public Player(){
      id = 012873;
      x = 0;
      y = 0;
      dir = Direction.UP;
      role = Role.SPECIALIST;
      stamina = 50;
    }

}
