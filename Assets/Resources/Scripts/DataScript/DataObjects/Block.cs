using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum Block
{
  [EnumMember(Value = "OPEN")]
  OPEN,

  [EnumMember(Value = "HOME")]
  HOME,

  [EnumMember(Value = "MOUNTAIN")]
  MOUNTAIN,

  [EnumMember(Value = "DESERT")]
  DESERT,

  [EnumMember(Value = "TREE")]
  TREE,

  [EnumMember(Value = "WATER")]
  WATER,

  [EnumMember(Value = "HILL")]
  HILL,

  [EnumMember(Value = "STAIR")]
  STAIR,

  [EnumMember(Value = "VOID")]
  VOID,
}
