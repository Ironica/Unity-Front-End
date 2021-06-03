using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum Block
{
  [EnumMember(Value = "OPEN")]
  OPEN,

  [EnumMember(Value = "OPEN")]
  HOME,

  [EnumMember(Value = "BLOCKED")]
  MOUNTAIN,

  [EnumMember(Value = "OPEN")]
  DESERT,

  [EnumMember(Value = "BLOCKED")]
  TREE,

  [EnumMember(Value = "BLOCKED")]
  WATER,

  [EnumMember(Value = "OPEN")]
  HILL,

  [EnumMember(Value = "STAIR")]
  STAIR,

  [EnumMember(Value = "VOID")]
  VOID,
}
