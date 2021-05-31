using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Id { get; }

    public int X { get; }
    
    public int Y { get; }

    public Direction Dir { get; }

    public Role Role { get; }

    public int Stamina { get; }

    public Player(int id, int x, int y, Direction dir, Role role, int stamina){
      this.Id = id;
      this.X = x;
      this.Y = y;
      this.Dir = dir;
      this.Role = role;
      this.Stamina = stamina;
    }

}
